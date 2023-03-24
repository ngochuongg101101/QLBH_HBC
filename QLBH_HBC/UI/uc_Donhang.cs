using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
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
    public partial class uc_Donhang : DevExpress.XtraEditors.XtraUserControl
    {
        float tongTien = 0;
        Boolean addnewflag = false;

        public uc_Donhang()
        {
            InitializeComponent();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT TRANGTHAI, MADH , NGAYTAO , TENDL , NGUOITAO, TONGTIEN  FROM DONHANG " +
                        "JOIN DAILY ON MADL = MA_DL";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.Refresh();
            gridView1.RowClick += gridView1_RowClick;
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            //1. Có nên thêm 1 cột MADL khum, hoặc lms để lưu được thông tin đó -> hiện ra cbDaily "MADL - TENDL"
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            NapCT();
        }

        private void NapCT()
        {
            var row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            // Lấy các giá trị MADH, NGAYTAO, HOTEN, TENDL, TRANGTHAI
            txtMadh.Text = row["MADH"].ToString();
            dtNgaytao.Text = row["NGAYTAO"].ToString();
            txtNguoitao.Text = row["NGUOITAO"].ToString();
            cbDaily.Text = row["TENDL"].ToString();
            txtTongtien.Text = row["TONGTIEN"].ToString();
            //string trangthai = row["TRANGTHAI"].ToString();   
            txtMadh.Enabled = false;
            dtNgaytao.Enabled = false;
            txtNguoitao.Enabled = false;
            cbDaily.Enabled = false;
            txtGhichu.Enabled = false;
            txtTongtien.Enabled = false;

            string sql1 = "SELECT MAHH, TENHH, CT_DONHANG.SL, DVT, CT_DONHANG.DONGIA, THANHTIEN FROM CT_DONHANG JOIN HANGHOA ON MAHH = MA_HH " +
                "WHERE MA_DH = '" + txtMadh.Text + "'";
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
            txtMadh.Enabled = true;
            dtNgaytao.Enabled = true;
            txtNguoitao.Enabled = true;
            cbDaily.Enabled = true;
            txtGhichu.Enabled = true;
            txtTongtien.Enabled = true;

            gridView1.AddNewRow();
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;

            cbDaily.Focus();
            btnSave.Enabled = true;
            txtMadh.Enabled = false;
            dtNgaytao.EditValue = DateTime.Today;
            //2. txtNguoitao = username

            for (int i = 1; i <= 20; i++)
            {
                gridView2.AddNewRow();
            }
            gridView2.Appearance.Row.BackColor = Color.Empty;
            addnewflag = true;

            //3.Thêm danh sách Mã DL - Tên DL vào cbDaily

        }

        private void gridView2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Lấy thông tin ô đang được chọn
                int rowHandle = gridView2.FocusedRowHandle;
                string fieldName = gridView2.FocusedColumn.FieldName;

                // Kiểm tra nếu cột đang chọn là cột MAHH hay cột SL
                if (fieldName == "MAHH" || fieldName == "SL")
                {
                    DataRow row = gridView2.GetDataRow(rowHandle);

                    // Lấy mã hàng hóa được thay đổi
                    string maHH = row["MAHH"].ToString();

                    // Kiểm tra nếu cột MAHH đã được cập nhật
                    if (fieldName == "MAHH" && !row.IsNull("MAHH"))
                    {
                        // Lấy thông tin hàng hóa từ cơ sở dữ liệu
                        string sql = "SELECT TENHH, DVT, DONGIA FROM HANGHOA WHERE MAHH = '" + maHH + "'";
                        DataTable dt = Config.DataProvider.Instance.ExecuteQuery(sql);

                        // Hiển thị thông tin hàng hóa lên gridView
                        if (dt.Rows.Count > 0)
                        {
                            gridView2.SetRowCellValue(rowHandle, "TENHH", dt.Rows[0]["TENHH"].ToString());
                            gridView2.SetRowCellValue(rowHandle, "DVT", dt.Rows[0]["DVT"].ToString());
                            gridView2.SetRowCellValue(rowHandle, "DONGIA", dt.Rows[0]["DONGIA"].ToString());
                        }
                    }

                    // Kiểm tra nếu cột SL đã được cập nhật
                    if (fieldName == "SL" && !row.IsNull("SL"))
                    {
                        int sl = int.Parse(row["SL"].ToString());
                        float donGia = float.Parse(gridView2.GetRowCellValue(rowHandle, "DONGIA").ToString());

                        float thanhTien = sl * donGia;
                        gridView2.SetRowCellValue(rowHandle, "THANHTIEN", thanhTien);

                        tongTien = tongTien + thanhTien;

                        // Gán giá trị tính được cho thuộc tính Text của đối tượng TextEdit
                        txtTongtien.Text = tongTien.ToString();

                        //Lấy lên VCK đi kèm
                        //Kiểm tra Hàng hóa đó có VCK không
                        string sql2 = "SELECT CO_VCK FROM HANGHOA WHERE MAHH = '" + maHH + "'";
                        DataTable dt1 = Config.DataProvider.Instance.ExecuteQuery(sql2);
                        string coVck = dt1.Rows[0][0].ToString();
                        //Nếu hàng hóa có VCK thì lấy lên VCK đi kèm
                        if (coVck == "True")
                        {
                            // Lấy thông tin VCK từ cơ sở dữ liệu
                            string sql = "SELECT MAHH,TENHH,BOOM.SL,DVT FROM HANGHOA,BOOM WHERE MAHH = MA_VO AND MA_BIA = '" + maHH + "'";
                            DataTable dt2 = Config.DataProvider.Instance.ExecuteQuery(sql);
                            // Hiển thị thông tin VCK lên gridView
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                gridView2.SetRowCellValue(rowHandle + i + 1, "MAHH", dt2.Rows[i]["MAHH"].ToString());
                                gridView2.SetRowCellValue(rowHandle + i + 1, "TENHH", dt2.Rows[i]["TENHH"].ToString());
                                gridView2.SetRowCellValue(rowHandle + i + 1, "SL", Convert.ToInt32(dt2.Rows[i]["SL"].ToString()) * sl);
                                gridView2.SetRowCellValue(rowHandle + i + 1, "DVT", dt2.Rows[i]["DVT"].ToString());
                            }

                        }


                    }
                }

            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMadh.Enabled = true;
            dtNgaytao.Enabled = true;
            txtNguoitao.Enabled = true;
            cbDaily.Enabled = true;
            txtGhichu.Enabled = true;
            txtTongtien.Enabled = true;

            gridView2.OptionsBehavior.ReadOnly = false;
            gridView2.OptionsBehavior.Editable = true;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (addnewflag == true)
            {
                //cập nhật thêm mới
                if (CheckControl())
                {
                    //3. TH1: Insert vào DONHANG (TRANGTHAI='Chờ xuất kho', CT_DONHANG)
                    MessageBox.Show("Thêm mới thành công!");
                    addnewflag = false;
                    NapLai();
                }
            }
            else
            {
                //cập nhật sửa chữa
                //4. TH2: Update thông tin đã sửa vào DONHANG, CT_DONHANG
                btnSave.Enabled = false;
                NapLai();
                MessageBox.Show("Đã lưu thay đổi!");
            }

        }
        public void NapLai()
        {
            string sql = "SELECT TRANGTHAI, MADH , NGAYTAO , TENDL , NGUOITAO, TONGTIEN  FROM DONHANG " +
                        "JOIN DAILY ON MADL = MA_DL";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.Refresh();
        }
        public bool CheckControl()
        {
            if (string.IsNullOrWhiteSpace(cbDaily.Text))
            {
                MessageBox.Show("Bạn chưa chọn Đại lý", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbDaily.Focus();
                return false;
            }
            //5. 
            //Check trong gridView2 nữa ạ (MAHH, SL)
            //if (string.IsNullOrWhiteSpace())
            //{
            //    MessageBox.Show("Bạn chưa nhập...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    .Focus();
            //    return false;
            //}
            return true;
        }
    }
}
