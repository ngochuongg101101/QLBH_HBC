using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH_HBC
{
    public partial class frmNhapcuoc : DevExpress.XtraEditors.XtraForm
    {
        public frmNhapcuoc()
        {
            InitializeComponent();
        }
        private DataTable dt1;
        float tongTien = 0;

        private void frmNhapcuoc_Load(object sender, EventArgs e)
        {
            //Ngày tạo mặc định là ngày hiện tại
            dtNgaytao.EditValue = DateTime.Today;

            //Hiển thị mã + tên đại lý trong combobox
            string sql = "Select MADL,TENDL from DAILY";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            cbDaily.DataSource = dt;
            dt.Columns.Add("FULL",typeof(string),"MADL + ' - ' + TENDL");
            cbDaily.DisplayMember = "FULL";
            cbDaily.Text = "";
            cbDaily.ValueMember = "MADL";

            //Truyền vào giá trị txtNguoitao = USERNAME đăng nhập, hiển thị là USERNAME

        }

        private void gridControl_Load(object sender, EventArgs e)
        {
            //Insert vào bảng các dòng trống có dữ liệu ở cột STT (1->20)
            // Create a DataTable to hold the data
            dt1 = new DataTable();
            dt1.Columns.Add("STT", typeof(int));
            dt1.Columns.Add("MAHH", typeof(string));
            dt1.Columns.Add("TENHH", typeof(string));
            dt1.Columns.Add("DVT", typeof(string));
            dt1.Columns.Add("SL", typeof(string));
            dt1.Columns.Add("GIACUOC", typeof(string));
            dt1.Columns.Add("THANHTIEN", typeof(string));

            // Add some data to the DataTable
            for (int i=1; i<=20; i++)
            {
                dt1.Rows.Add(i);
            }

            // Set the DataTable as the DataSource of the grid control
            gridControl.DataSource = dt1;

            //Click để thêm dòng
            gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void gridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Lấy thông tin ô đang được chọn
                int rowHandle = gridView.FocusedRowHandle;
                string fieldName = gridView.FocusedColumn.FieldName;

                // Kiểm tra nếu cột đang chọn là cột MAHH hoặc SL
                if (fieldName == "MAHH" || fieldName == "SL")
                {
                    DataRow row = gridView.GetDataRow(rowHandle);

                    // Kiểm tra nếu cột MAHH đã được cập nhật
                    if (fieldName == "MAHH" && !row.IsNull("MAHH"))
                    {
                        // Lấy mã hàng hóa được thay đổi
                        string maHH = row["MAHH"].ToString();

                        // Lấy thông tin hàng hóa từ cơ sở dữ liệu
                        string sql = "SELECT TENHH, DVT, GIACUOC FROM HANGHOA WHERE MAHH = '" + maHH + "'";
                        DataTable dt = DataProvider.Instance.ExecuteQuery(sql);

                        // Hiển thị thông tin hàng hóa lên gridView
                        if (dt.Rows.Count > 0)
                        {
                            gridView.SetRowCellValue(rowHandle, "TENHH", dt.Rows[0]["TENHH"].ToString());
                            gridView.SetRowCellValue(rowHandle, "DVT", dt.Rows[0]["DVT"].ToString());
                            gridView.SetRowCellValue(rowHandle, "GIACUOC", dt.Rows[0]["GIACUOC"].ToString());
                        }
                    }

                    // Kiểm tra nếu cột SL đã được cập nhật
                    if (fieldName == "SL" && !row.IsNull("SL"))
                    {
                        int sl = int.Parse(row["SL"].ToString());
                        float giaCuoc = float.Parse(gridView.GetRowCellValue(rowHandle, "GIACUOC").ToString());

                        float thanhTien = sl * giaCuoc;
                        gridView.SetRowCellValue(rowHandle, "THANHTIEN", thanhTien);

                        tongTien = tongTien + thanhTien;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //1. TẠO PHIẾU NHẬP CƯỢC
            //Insert into PHIEUCUOC(NGAYTAO,NGUOITAO,MADL,LOAI = N'Nhập')
            //-> Tạo xong về SQL sinh được MA_PC
            //-> Lấy lên MAPC = MA_PC của phiếu vừa tạo
            SqlConnection connection = new SqlConnection();
            string sql = "Data Source=DESKTOP-33G4CSH;Initial Catalog=QLBH_HBC;Integrated Security=True";
            connection.ConnectionString = sql;
            connection.Open();
            {
                //connection.Open();
                MessageBox.Show(connection.ConnectionString.Trim());

                // Tạo command và truyền vào câu lệnh SQL
                using (SqlCommand command = new SqlCommand("INSERT INTO PHIEUCUOC(NGAYTAO, NGUOITAO,LOAI) VALUES (@Ngaytao,@Nguoitao,N'Nhập')", connection))
                {
                    // Truyền các tham số vào command
                    //MessageBox.Show(dtNgaytao.EditValue.ToString());
                    MessageBox.Show(cbNguoitao.Text);
                    MessageBox.Show(cbDaily.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@NgayTao", dtNgaytao.DateTime);
                    command.Parameters.AddWithValue("@NguoiTao", cbNguoitao.Text);
                    command.Parameters.AddWithValue("@MaDL", cbDaily.SelectedValue.ToString());
                    command.ExecuteNonQuery();
                    MessageBox.Show(command.ToString());

                    // Thực hiện execute scalar để lấy ra giá trị của MAPC
                    //int mapc = (int)command.ExecuteScalar();

                    // Hiển thị thông báo thành công và gán giá trị MAPC vào một control trên form
                    XtraMessageBox.Show("Tạo phiếu thành công! Mã phiếu: " );
                    //string maPC = mapc.ToString();
                }
            }
            //Chạy lần lượt các row trong gridView
            //(
            //    1.1 TẠO CHI TIẾT PHIẾU CƯỢC
            //    Insert into CT_PHIEUCUOC(MA_PC,MA_VO,SL)

            //    1.2 UPDATE THÊM SỐ LƯỢNG NHẬP CƯỢC
            //    Lấy lên SL_old = VCKCUOC.SL_CUOC where VCKCUOC.MA_DL = MADL and VCKCUOC.MA_VO = MAHH
            //    Update VCKCUOC
            //    Set SLCUOC = SL_old + SL cược thêm
            //    where VCKCUOC.MA_DL = MADL and VCKCUOC.MA_VO = MAHH
            //)

            //2. Tạo phiếu thu chi
            //Insert into  PHIEUTHUCHI(NGAYTAO,NGUOITAO,PTTT,TONGTIEN,MA_VV='VV002', MA_PC= MAPC vừa tạo)
        }

        private void cbDaily_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNoidung.Text = "Thu tiền cược VCK của " + cbDaily.Text;
        }
    }
}
