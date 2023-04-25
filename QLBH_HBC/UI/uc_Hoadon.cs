using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH_HBC.UI
{
    public partial class uc_Hoadon : DevExpress.XtraEditors.XtraUserControl
    {
        Boolean addnewflag = false;
        private string username;
        private uc_Hoadon ucHoadon;
        public string maDH { get; set; }
        public uc_Hoadon(string userName, uc_Hoadon ucHoadon)
        {
            InitializeComponent();
            username = userName;
            this.ucHoadon = ucHoadon;
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "select MAHD,NGAYTAO,NGUOITAO,VAT,THANHTOAN,TONGTIEN,MA_DH,TENLOAI from HOADON,LOAIHD WHERE MALHD=MA_LHD";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.Refresh();
            gridView1.RowClick += gridView1_RowClick;
            gridView1.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            NapCT();
        }
        private void NapCT()
        {
            var row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            cbLoaiHD.Text = row["TENLOAI"].ToString();
            btnMadh.Text = row["MA_DH"].ToString();
            txtMahd.Text = row["MAHD"].ToString();
            dtNgaytao.Text = row["NGAYTAO"].ToString();
            txtNguoitao.Text = row["NGUOITAO"].ToString();
            if (addnewflag == true)
            {
                chkThanhtoan.Checked = false;
            }
            else if (addnewflag == false)
            {
                chkThanhtoan.Checked = Convert.ToBoolean(row["THANHTOAN"].ToString());
            }

            string sql1 = "SELECT MAHH, TENHH, CT_DONHANG.SL, DVT, CT_DONHANG.DONGIA, (CT_DONHANG.SL*CT_DONHANG.DONGIA) AS THANHTIEN FROM CT_DONHANG JOIN HANGHOA ON MAHH = MA_HH " +
                "WHERE MA_DH = '" + btnMadh.Text + "'";
            gridControl2.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql1);
            gridControl2.Refresh();
            gridView2.OptionsBehavior.ReadOnly = true;
            gridView2.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }

        private void gridControl1_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = gridControl1.MainView as GridView;
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));
                if (info.InRow)
                {
                    view.FocusedRowHandle = info.RowHandle;
                    view.SelectRow(info.RowHandle);
                    gridView1_RowClick(sender, null);
                }
            }
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addnewflag = true;
            gridView1.AddNewRow();
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            NapCT();
            btnSave.Enabled = true;
            txtMahd.Enabled = false;
            dtNgaytao.EditValue = DateTime.Today;
            txtNguoitao.Text = username;
            chkThanhtoan.Enabled = false;
            for (int i = 1; i <= 20; i++)
            {
                gridView2.AddNewRow();
            }
            gridView2.OptionsBehavior.Editable = false;
            gridView2.OptionsBehavior.ReadOnly = true;
            gridView2.Appearance.Row.BackColor = Color.Empty;
            List<DTO.LoaiHD> data_loaiHD = DAO.LoaiHDDAO.Instance.GetAll();
            //Lấy lên từ table LOAIPK -> cbLoaiPK, value = MALPK, displaymember = TENLOAI,
            //hiển thị sẵn value (LPK0001) - displaymember (Xuất bán hàng) (loại thường xuyên nhất -> sau đó ng dùng có thể đổi)
            cbLoaiHD.DataSource = data_loaiHD;
            cbLoaiHD.DisplayMember = "tenLHD";
            //DONE
            //for (int i = gridView2.DataRowCount - 1; i >= 0; i--)
            //{
            //    bool rowIsEmpty = true;
            //    for (int j = 0; j < gridView2.VisibleColumns.Count; j++)
            //    {
            //        if (!string.IsNullOrEmpty(gridView2.GetRowCellValue(i, gridView2.VisibleColumns[j]).ToString()))
            //        {
            //            rowIsEmpty = false;
            //            break;
            //        }
            //    }

            //    if (rowIsEmpty)
            //    {
            //        gridView2.DeleteRow(i);
            //    }
            //}
        }

        private void btnMadh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmDonhang f = new frmDonhang(null,this);
            f.ShowDialog();
            btnMadh.Text = maDH;
            int check = gridView2.DataRowCount - 1;
            if (check > 0)
            {
                for (int i = gridView2.DataRowCount - 1; i >= 0; i--)
                {
                    check = check - 1;
                    gridView2.DeleteRow(i);
                    break;
                }
            }
            if (maDH.Trim().Length > 0 && check == 0)
            {
                int rowHandle = gridView2.FocusedRowHandle;
                DataRow row = gridView2.GetDataRow(rowHandle);

                List<DTO.CTDonhang> data_ctdonhang = DAO.CTDonhangDAO.Instance.GetMaHHByMaDH(maDH.Trim().ToUpper());
                if (data_ctdonhang != null)
                {
                    foreach (DTO.CTDonhang ctDonhang in data_ctdonhang)
                    {
                        if (ctDonhang.MaHH.Trim().ToUpper().Length > 0)
                        {
                            List<DTO.Hanghoa> hanghoa = DAO.HanghoaDAO.Instance.GetList(ctDonhang.MaHH.Trim().ToUpper());
                            if (hanghoa.Count > 0)
                            {

                                gridView2.BeginUpdate();
                                foreach (DTO.Hanghoa item in hanghoa)
                                {
                                    gridView2.AddNewRow();
                                    int newRowHandle = gridView2.RowCount - 1;

                                    gridView2.SetRowCellValue(rowHandle, "MAHH", item.MaHH);
                                    gridView2.SetRowCellValue(rowHandle, "TENHH", item.TenHH);
                                    gridView2.SetRowCellValue(rowHandle, "SL", ctDonhang.Sl);
                                    gridView2.SetRowCellValue(rowHandle, "DVT", item.Dvt);
                                    rowHandle = rowHandle + 1;
                                }
                                gridView2.EndUpdate();

                            }
                        }

                    }
                }
            }
        }

        //Add -> Chọn loại HD -> Click vào kính lúp sẽ mở ra form frmDonhang (tương tự uc_Kho) 
        //    * Sửa lại như sao cho khi mở form từ:
        //                                       + uc_Kho -> lấy Đơn hàng trạng thái "Chờ xuất kho"
        //                                       + uc_Hoadon -> Lấy Đơn hàng trạng thái "Đã xuất kho"
        //    * Sau khi chọn được số đơn hàng từ frmDonhang hoặc nhập số đơn hàng rồi nhấn Enter
        //      -> Lấy lên gridView2 thông tin bảng CT_DONHANG where MA_DH = btnMadh.Text
        //    * Nhập thuế vào txtVAT -> tự động tính txtTongtien
        //Save 
        //    * Lưu vào HOADON (MAHD, NGAYTAO, NGUOITAO, VAT, THANHTOAN, TONGTIEN, MA_DH, MA_LHD)
        //              Values (tự sinh,dtNgaytao,txtNguoitao,txtVAT,false,SUM(THANHTIEN),btnMadh,cbLoaiHD.SelectedValue)
        //    * 

    }
}
