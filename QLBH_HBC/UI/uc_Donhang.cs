using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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
        public double tongTien = 0;
        private int slcuoc = 0;
        Boolean addnewflag = false;
        private string username;
        public uc_Donhang()
        {
            InitializeComponent();
        }

        public uc_Donhang(string userName)
        {
            InitializeComponent();
            username = userName;
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT TRANGTHAI, MADH , NGAYTAO , TENDL , NGUOITAO, TONGTIEN  FROM DONHANG " +
                        "JOIN DAILY ON MADL = MA_DL";

            DataTable dt = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.DataSource = dt;

            dt.Columns.Add("STATUS", typeof(int));
            for (int i=0; i<dt.Rows.Count; i++)
            {
                string trangThai = dt.Rows[i]["TRANGTHAI"].ToString();
                if (trangThai == "Chờ xuất kho")
                {
                    dt.Rows[i]["STATUS"] = 0;
                }
                if (trangThai == "Đã xuất kho")
                {
                    dt.Rows[i]["STATUS"] = 1;
                }
                if (trangThai == "Đã tạo hóa đơn")
                {
                    dt.Rows[i]["STATUS"] = 2;
                }
            }

            gridControl1.DataSource = dt;
            gridControl1.Refresh();

            gridView1.RowClick += gridView1_RowClick;
            //gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            //1. Có nên thêm 1 cột MADL khum, hoặc lms để lưu được thông tin đó -> hiện ra cbDaily "MADL - TENDL" Không cần thiết làm thế chỉ cho bên nhà phát triền dễ dùng. còn nguòi dùng sẽ biết chọn đại lý nào rồi mà
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            NapCT();
            txtMadh.Enabled = false;
            dtNgaytao.Enabled = false;
            txtNguoitao.Enabled = false;
            cbDaily.Enabled = false;
            txtGhichu.Enabled = false;
            txtTongtien.Enabled = false;
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


            string sql1 = "SELECT MAHH, TENHH, CT_DONHANG.SL, DVT, CT_DONHANG.DONGIA, THANHTIEN FROM CT_DONHANG JOIN HANGHOA ON MAHH = MA_HH " +
                "WHERE MA_DH = '" + txtMadh.Text + "'";
            gridControl2.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql1);
            gridControl2.Refresh();
            //gridView2.OptionsBehavior.ReadOnly = true;
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
            NapCT();
            cbDaily.Focus();
            btnSave.Enabled = true;
            txtMadh.Enabled = false;
            dtNgaytao.EditValue = DateTime.Today;
            txtNguoitao.Text = username;

            for (int i = 1; i <= 20; i++)
            {
                gridView2.AddNewRow();
            }
            gridView2.OptionsBehavior.Editable = true;
            gridView2.OptionsBehavior.ReadOnly = false;
            gridView2.Appearance.Row.BackColor = Color.Empty;
            addnewflag = true;

            //3.Thêm danh sách Mã DL - Tên DL vào cbDaily
            string sql = "Select MADL,TENDL from DAILY";
            DataTable dt = Config.DataProvider.Instance.ExecuteQuery(sql);
            cbDaily.DataSource = dt;
            dt.Columns.Add("FULL", typeof(string), "TRIM(MADL) + ' - ' + TENDL");
            cbDaily.DisplayMember = "FULL";
            cbDaily.Text = "";
            cbDaily.ValueMember = "MADL";
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
                        string sql = "SELECT TENHH, DVT, DONGIA FROM HANGHOA WHERE MAHH = '" + maHH.Trim().ToUpper() + "'";
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
                        double donGia = double.Parse(gridView2.GetRowCellValue(rowHandle, "DONGIA").ToString());

                        double thanhTien = sl * donGia;
                        gridView2.SetRowCellValue(rowHandle, "THANHTIEN", thanhTien);


                        // Gán giá trị tính được cho thuộc tính Text của đối tượng TextEdit
                        txtTongtien.Text = tongTien.ToString();

                            tongTien = tongTien + Convert.ToDouble(cellValueThanhtien);
                        }

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
                        GridColumn summaryColumn = gridView2.Columns["THANHTIEN"];
                        double summaryValue = Convert.ToDouble(gridView2.Columns["THANHTIEN"].SummaryItem.SummaryValue);
                        MessageBox.Show(summaryValue.ToString());
                        txtTongtien.Text = tongTien.ToString();

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


            for (int i = 1; i <= 20; i++)
            {
                gridView2.AddNewRow();
            }
            gridView2.OptionsBehavior.Editable = true;
            gridView2.OptionsBehavior.ReadOnly = false;
            gridView2.Appearance.Row.BackColor = Color.Empty;
            btnSave.Enabled = true;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(cbDaily.Text))
            {
                MessageBox.Show("Bạn chưa chọn Đại lý", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Insert
                if (addnewflag == true)
                {
                    bool checkHHVo = false;
                    DTO.Vckcuoc dataVCK;
                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        object cellValueMaHH = gridView2.GetRowCellValue(i, "MAHH");
                        object cellValueMaSL = gridView2.GetRowCellValue(i, "SL");
                        if (cellValueMaHH.ToString().Trim().Length > 0)
                        {
                            bool HHVo = DAO.HanghoaDAO.Instance.GetByDataOtherByBark(cellValueMaHH.ToString().Trim());
                            if (HHVo)
                            {
                                dataVCK = DAO.VCKDAO.Instance.Get(cbDaily.SelectedValue.ToString().Trim(), cellValueMaHH.ToString().Trim().ToUpper());
                                if(dataVCK == null)
                                {
                                    checkHHVo = true;
                                    XtraMessageBox.Show("Mã võ không có trong danh sách cược vỏ chai két của Đại lý. Yêu cầu đại lý cược võ có mã hàng hóa là " + cellValueMaHH.ToString().Trim(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                                else
                                {
                                    int sl_khadung = Convert.ToInt32(dataVCK.SlCuoc) - Convert.ToInt32(dataVCK.SlGiu);
                                    if (Convert.ToInt32(cellValueMaSL) - sl_khadung> 0)
                                    {
                                        checkHHVo = true;
                                        XtraMessageBox.Show("Yêu cầu đại lý đặt cược thêm võ có mã hàng hóa là " + cellValueMaHH.ToString().Trim(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    }
                                
                                }
                            }
                            else
                            {
                                bool HHBia = DAO.HanghoaDAO.Instance.GetByDataOtherByBear(cellValueMaHH.ToString().Trim().ToUpper());
                                if (HHBia)
                                {
                                    DTO.Hanghoa hanghoa = DAO.HanghoaDAO.Instance.Get(cellValueMaHH.ToString().Trim().ToUpper());
                                    if(Convert.ToInt32(hanghoa.Sl) - Convert.ToInt32(cellValueMaSL) < 0)
                                    {
                                        checkHHVo = true;
                                        XtraMessageBox.Show("Hiện tại số lượng sản phẩm bạn đặt vượt quá số lượng hàng hóa kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    
                    if (CheckControl() && !checkHHVo && txtTongtien.Text.Trim().Length > 0 && cbDaily.SelectedValue.ToString().Trim().Length > 0)
                    {
                        //3. TH1: Insert vào DONHANG (TRANGTHAI='Chờ xuất kho', CT_DONHANG)
                        string resultDH = null;
                        if (txtGhichu.Text.Length>0)
                        {
                            resultDH = DAO.DonhangDAO.Instance.Insert(dtNgaytao.DateTime.ToString("MM/dd/yyyy HH:mm:ss"), username, "Chờ xuất kho", cbDaily.SelectedValue.ToString().Trim(), txtGhichu.Text.Trim(), Convert.ToDouble(txtTongtien.Text.Trim()));
                        }
                        else
                        {
                            resultDH = DAO.DonhangDAO.Instance.Insert(dtNgaytao.DateTime.ToString("MM/dd/yyyy HH:mm:ss"), username, "Chờ xuất kho", cbDaily.SelectedValue.ToString().Trim(), " ", Convert.ToDouble(txtTongtien.Text.Trim()));
                        }
                        bool resultCT = false;
                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            object cellValueMaHH = gridView2.GetRowCellValue(i, "MAHH");
                            object cellValueSL = gridView2.GetRowCellValue(i, "SL");
                            object cellValueDonGia = gridView2.GetRowCellValue(i, "DONGIA");
                            object cellValueThanhTien = gridView2.GetRowCellValue(i, "THANHTIEN");
                            if (resultDH != null)
                            {
                                    if (cellValueMaHH.ToString().Trim().Length > 0 && cellValueSL.ToString().Trim().Length > 0)
                                    {
                                        if (cellValueDonGia.ToString().Trim().Length > 0 && cellValueThanhTien.ToString().Trim().Length > 0)
                                        {
                                            resultCT = DAO.CTDonhangDAO.Instance.Insert(resultDH, cellValueMaHH.ToString().Trim().ToUpper(), Convert.ToInt32(cellValueSL), Convert.ToDouble(cellValueDonGia.ToString().Trim()), Convert.ToDouble(cellValueThanhTien.ToString().Trim()));
                                        }
                                    else
                                        {
                                            resultCT = DAO.CTDonhangDAO.Instance.Insert(resultDH, cellValueMaHH.ToString().Trim().ToUpper(), Convert.ToInt32(cellValueSL), 0,0);
                                            if (resultCT)
                                            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
                                                DTO.Vckcuoc dataVCKSL = DAO.VCKDAO.Instance.Get(cbDaily.SelectedValue.ToString().Trim(), cellValueMaHH.ToString().Trim().ToUpper());
                                            }
                                    }
                                }
                                    else
                                    {
                                        break;
                                    }
                            }

                        }
                        if (resultCT == true)
                        {

                            MessageBox.Show("Thêm mới thành công!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            NapLai();
                            txtMadh.Enabled = false;
                            dtNgaytao.Enabled = false;
                            txtNguoitao.Enabled = false;
                            cbDaily.Enabled = false;
                            txtGhichu.Enabled = false;
                            txtTongtien.Enabled = false;

                            txtMadh.Text = "";
                            dtNgaytao.Text = "";
                            txtNguoitao.Text = "";
                            cbDaily.Text = "";
                            txtGhichu.Text = "";
                            txtTongtien.Text = "";

                        }
                        else
                        {
                            MessageBox.Show("Thêm mới không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        addnewflag = false;
                    }
                }
                // Update
                else
                {
                    btnSave.Enabled = false;
                    //NapLai();
                    MessageBox.Show("Đã lưu thay đổi! Sửa");
                }

            }

        }
        public void NapLai()
        {
            string sql1 = "SELECT TRANGTHAI, MADH , NGAYTAO , TENDL , NGUOITAO, TONGTIEN  FROM DONHANG " +
                        "JOIN DAILY ON MADL = MA_DL";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql1);
            gridControl1.Refresh();
        }
        public bool CheckControl()
        {
            if (string.IsNullOrWhiteSpace(cbDaily.Text))
            {
                //MessageBox.Show("Bạn chưa chọn Đại lý", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //cbDaily.Focus();
                //return false;
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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
