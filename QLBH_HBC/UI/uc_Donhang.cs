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
        private DataTable dt;
        float tongTien = 0;
        public uc_Donhang()
        {
            InitializeComponent();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT TRANGTHAI, MADH , NGAYTAO , TENDL , NGUOITAO, TONGTIEN  FROM DONHANG " +
                        "JOIN DAILY ON MADL = MA_DL";
            gridControl1.DataSource = DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.Refresh();
            gridView1.RowClick += gridView1_RowClick;
            gridView1.OptionsBehavior.Editable = true;
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

            string sql1 = "SELECT MAHH, TENHH, CT_DONHANG.SL, DVT, CT_DONHANG.DONGIA, THANHTIEN FROM CT_DONHANG JOIN HANGHOA ON MAHH = MA_HH " +
                "WHERE MA_DH = '" + txtMadh.Text + "'";
            gridControl2.DataSource = DataProvider.Instance.ExecuteQuery(sql1);
            gridControl2.Refresh();
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
            cbDaily.Focus();
            btnSave.Enabled = true;
            txtMadh.Enabled = false;
            dtNgaytao.EditValue = DateTime.Today;

            //Insert vào bảng các dòng trống có dữ liệu ở cột STT(1->20)
            // Create a DataTable to hold the data
            //dt = new DataTable();
            //dt.Columns.Add("STT", typeof(int));
            //dt.Columns.Add("MAHH", typeof(string));
            //dt.Columns.Add("TENHH", typeof(string));
            //dt.Columns.Add("DVT", typeof(string));
            //dt.Columns.Add("SL", typeof(string));
            //dt.Columns.Add("DONGIA", typeof(string));
            //dt.Columns.Add("THANHTIEN", typeof(string));

            // Add some data to the DataTable
            for (int i = 1; i <= 20; i++)
            {
                //dt.Rows.Add(i);
                gridView2.AddNewRow();
                //gridView2.SetRowCellValue(i-1, "STT", i.ToString());
                //gridView2.SetRowCellValue(rowHandle + i + 1, "MAHH", dt2.Rows[i]["MAHH"].ToString());
            }

            // Set the DataTable as the DataSource of the grid control
            //gridControl2.DataSource = dt;

            //Click để thêm dòng
            //gridView2.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
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
                        DataTable dt = DataProvider.Instance.ExecuteQuery(sql);

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
                        DataTable dt1 = DataProvider.Instance.ExecuteQuery(sql2);
                        string coVck = dt1.Rows[0][0].ToString();
                        //Nếu hàng hóa có VCK thì lấy lên VCK đi kèm
                        if (coVck == "True")
                        {
                            // Lấy thông tin VCK từ cơ sở dữ liệu
                            string sql = "SELECT MAHH,TENHH,BOOM.SL,DVT FROM HANGHOA,BOOM WHERE MAHH = MA_VO AND MA_BIA = '" + maHH + "'";
                            DataTable dt2 = DataProvider.Instance.ExecuteQuery(sql);
                            // Hiển thị thông tin VCK lên gridView
                            for (int i=0; i<dt2.Rows.Count; i++)
                            {
                                gridView2.SetRowCellValue(rowHandle+i+1, "MAHH", dt2.Rows[i]["MAHH"].ToString());
                                gridView2.SetRowCellValue(rowHandle+i+1, "TENHH", dt2.Rows[i]["TENHH"].ToString());
                                gridView2.SetRowCellValue(rowHandle+i+1, "SL", Convert.ToInt32(dt2.Rows[i]["SL"].ToString())*sl);
                                gridView2.SetRowCellValue(rowHandle+i+1, "DVT", dt2.Rows[i]["DVT"].ToString());
                            }

                        }


                    }
                }

            }
        }

    }
}
