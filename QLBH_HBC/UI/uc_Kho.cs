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
    public partial class uc_Kho : DevExpress.XtraEditors.XtraUserControl
    {

        private string userName;
        private uc_Kho ucKho;
        public string maDH { get; set; }
        public uc_Kho(string username, uc_Kho ucKho)
        {
            InitializeComponent();
            this.userName = username;
            this.ucKho = ucKho;
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT MAPK,NGAYTAO,NGUOITAO,NOIDUNG,PTVC,BIENSO,TENLOAI,MA_DH FROM PHIEUKHO " +
                "JOIN LOAIPK ON MALPK=MA_LPK";
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
            cbLoaiPK.Text = row["TENLOAI"].ToString();
            txtMapk.Text = row["MAPK"].ToString();
            dtNgaytao.Text = row["NGAYTAO"].ToString();
            txtNguoitao.Text = row["NGUOITAO"].ToString();
            txtPTVC.Text = row["PTVC"].ToString();
            txtBienso.Text = row["BIENSO"].ToString();
            txtNoidung.Text = row["NOIDUNG"].ToString();
            btnMadh.Text = row["MA_DH"].ToString();

            string sql1 = "SELECT MAHH, TENHH, CT_DONHANG.SL, DVT FROM CT_DONHANG JOIN HANGHOA ON MAHH = MA_HH " +
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
            gridView1.AddNewRow();
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;

            NapCT();
            btnSave.Enabled = true;
            txtMapk.Enabled = false;
            dtNgaytao.EditValue = DateTime.Today;
            txtNguoitao.Text = userName;

            for (int i = 1; i <= 20; i++)
            {
                gridView2.AddNewRow();
            }
            // Thiết lập FocusedRowHandle là hàng đầu tiên
            gridView2.FocusedRowHandle = 0;

            // Thiết lập TopRowIndex để đảm bảo hàng đầu tiên là hàng đang được focus
            gridView2.TopRowIndex = gridView2.FocusedRowHandle;

            gridView2.OptionsBehavior.Editable = true;
            gridView2.OptionsBehavior.ReadOnly = false;
            gridView2.Appearance.Row.BackColor = Color.Empty;
            List<DTO.LoaiPK> data_loaiPK = DAO.LoaiPKDAO.Instance.GetAll();
            //Lấy lên từ table LOAIPK -> cbLoaiPK, value = MALPK, displaymember = TENLOAI,
            //hiển thị sẵn value (LPK0001) - displaymember (Xuất bán hàng) (loại thường xuyên nhất -> sau đó ng dùng có thể đổi)
            cbLoaiPK.DataSource = data_loaiPK;
            cbLoaiPK.DisplayMember = "tenLPK";
            //DONE
            
        }

        private void cbLoaiPK_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nếu = LPK0001 thì txtMadh.Visible = true;
            //còn lại -> txtMadh.Visible = false; (do chỉ có trường hợp xuất bán hàng là có đi kèm mã đơn hàng)
            if(cbLoaiPK.SelectedIndex != null)
            {
                DTO.LoaiPK loaiPK = (DTO.LoaiPK)cbLoaiPK.SelectedValue;
                if(loaiPK.MaLPK.Trim() == "LPK0001")
                {
                    btnMadh.Visible = true;
                }
                else
                {
                    btnMadh.Visible = false;
                }
            }
            //done
        }

        private void btnMadh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmDonhang f = new frmDonhang(this);
            f.ShowDialog();
            btnMadh.Text = maDH;
            if (maDH.Trim().Length > 0)
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
                            DTO.Hanghoa hanghoa = DAO.HanghoaDAO.Instance.Get(ctDonhang.MaHH.Trim().ToUpper());
                            if (hanghoa != null)
                            {
                                gridView2.SetRowCellValue(rowHandle, "MAHH", hanghoa.MaHH);
                                gridView2.SetRowCellValue(rowHandle, "TENHH", hanghoa.TenHH);
                                gridView2.SetRowCellValue(rowHandle, "SL", ctDonhang.Sl);
                                gridView2.SetRowCellValue(rowHandle, "DVT", hanghoa.Dvt);
                            }
                        }

                    }
                }
            }
            //Người dùng bấm vào ô tìm kiếm sẽ chạy ra frmDonhang -> gửi đi 1 tham số vào frmDonhang
            //Khi kích đúp vào 1 dòng -> sẽ truyền lại thông tin MADH vào tham số đã gửi đến
            //Hiển thị tham số đó lên btnMadh.Text
            //Done

        }
        private void frmDonhang_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show(ucKho.maDH);
            //Lấy thông tin mã đơn hàng từ form frmDonhang
            frmDonhang f = sender as frmDonhang;
            MessageBox.Show(ucKho.maDH);
            if (f != null && f.DialogResult == DialogResult.OK)
            {
                // Thực hiện gán giá trị mã đơn hàng cho button btnMadh
                btnMadh.Text = ucKho.maDH;
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //1. Insert PHIEUKHO (NGAYTAO, NGUOITAO, NOIDUNG, PTVC, BIENSO, MA_LPK, MA_DH)
            //
            //2. Check thiếu thông tin
            //- Check txtNgaytao, txtNguoitao, MaHH, SL
            //- Nếu MALPK = 'LPK0001' -> Check thêm btnMadh
            if (cbLoaiPK.Text.Trim().Length > 0)
            {
                if (btnMadh.Text.Trim().Length > 0)
                {
                    if (txtNguoitao.Text.Trim().Length > 0)
                    {
                        if(txtNoidung.Text.Trim().Length > 0)
                        {
                            if(txtPTVC.Text.Trim().Length > 0)
                            {
                                if(txtBienso.Text.Trim().Length > 0)
                                {

                                }
                                else
                                {
                                    XtraMessageBox.Show("Bạn nhập thiếu biển số xe vận chuyển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show("Bạn nhập thiếu phương tiện vận chuyển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("Bạn nhập thiếu nội dung diễn giải", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Bạn nhập thiếu người tạo", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Bạn nhập thiếu trường loai phiếu kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                XtraMessageBox.Show("Bạn nhập thiếu trường loai phiếu kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //2. Check tồn + VCK cược
            //- Từ MALPK -> vào bảng LOAIPK xem LOAI -> Nếu LOAI = "Xuất" 
            //  -> Kiểm tra xem có vượt tồn kho ở Hàng hóa không -> nếu vượt -> hiện thông báo chặn
            //- Kiểm tra nếu MALPK = "LPK0001"
            //  -> Kiểm tra SL có vượt SL khả dụng = SL_CUOC - SL_GIU -> nếu vượt -> hiện thông báo chặn
            //
            //3. Insert CT_PHIEUKHO (MAPK (vừa tạo), MAHH, SL)
            //
            //4. Update SL (tồn kho) ở bảng HANGHOA
            //      + Nếu LOAI = "Nhập" -> cộng thêm
            //      + Nếu LOAI = "Xuất" -> trừ đi

            //5. Update SL giữ trong VCKCUOC
            //- Nếu MALPK = "LPK0001" 
            //- Với mã vỏ -> Lấy lên SL_GIU ở bảng VCKCUOC
            //- SL_GIU mới = SL_GIU hiện tại + SL trong đơn hàng

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnMadh.Text = ucKho.maDH;
        }
    }
}
