using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using QLBH_HBC.Reports;
using QLBH_HBC.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.Sql;

namespace QLBH_HBC
{
    public partial class frmNhapcuoc : DevExpress.XtraEditors.XtraForm
    {
        private string userName;
        private string loai;
        private double tongtien = 0;
        private uc_Cuocvo uc_Cuocvo;
        // Declare an event that can be raised in the parent form
        public event EventHandler ChildFormEvent;
        private string mvv;
        public frmNhapcuoc(string username, string loai, uc_Cuocvo _Cuocvo)
        {
            InitializeComponent();
            this.userName = username;
            this.loai = loai;
            this.uc_Cuocvo = _Cuocvo;
        }
        private DataTable dt1;
        private void frmNhapcuoc_Load(object sender, EventArgs e)
        {
            if (loai == "Nhập")
            {
                label1.Text = "PHIẾU NHẬN CƯỢC VỎ CHAI KÉT";
                mvv = "MVV0002";

            }
            if (loai == "Trả")
            {
                label1.Text = "PHIẾU TRẢ CƯỢC VỎ CHAI KÉT";
                mvv = "MVV0003";
            }


            //Ngày tạo mặc định là ngày hiện tại
            dtNgaytao.EditValue = DateTime.Today;
            //Hiển thị mã + tên đại lý trong combobox
            string sql = "Select MADL,TENDL from DAILY";
            DataTable dt = Config.DataProvider.Instance.ExecuteQuery(sql);
            cbDaily.DataSource = dt;
            //dt.Columns.Add("FULL", typeof(string), "TRIM(MADL) + ' - ' + TENDL");
            cbDaily.DisplayMember = "TENDL";
            cbDaily.Text = "";
            cbDaily.ValueMember = "MADL";
            txtNguoitao.Text = userName;
            //DTO.Nguoidung Hoten = DAO.NguoidungDAO.Instance.GetFullNameByUsername(userName);
            //if (Hoten != null)
            //{
            //    txtNguoitao.Text = Hoten.Hoten.ToString();

            //}
            txtNguoitao.Enabled = false;
            txtNoidung.Text = "";

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
                        string sql = "SELECT TENHH, DVT, GIACUOC FROM HANGHOA WHERE MAHH = '" + maHH + "' AND LOAI = N'Vỏ'";
                        DataTable dt = Config.DataProvider.Instance.ExecuteQuery(sql);

                        // Hiển thị thông tin hàng hóa lên gridView
                        if (dt.Rows.Count > 0)
                        {
                            gridView.SetRowCellValue(rowHandle, "TENHH", dt.Rows[0]["TENHH"]);
                            gridView.SetRowCellValue(rowHandle, "DVT", dt.Rows[0]["DVT"]);
                            gridView.SetRowCellValue(rowHandle, "GIACUOC", Convert.ToDouble (dt.Rows[0]["GIACUOC"]));
                        }
                        else
                        {
                            XtraMessageBox.Show("Yêu cầu nhập mã hàng hóa là vỏ","Cảnh báo",MessageBoxButtons.OK);
                        }
                    }

                    // Kiểm tra nếu cột SL đã được cập nhật
                    if (fieldName == "SL" && !row.IsNull("SL"))
                    {
                        int sl = int.Parse(row["SL"].ToString());
                        double giaCuoc = float.Parse(gridView.GetRowCellValue(rowHandle, "GIACUOC").ToString());

                        double thanhTien = sl * giaCuoc;
                        gridView.SetRowCellValue(rowHandle, "THANHTIEN", thanhTien);
                        //for (int i = 0; i < gridView.RowCount; i++)
                        //{
                        //    object cellValueThanhTien = gridView.GetRowCellValue(i, "THANHTIEN");
                        //    if(cellValueThanhTien.ToString().Trim().Length > 0)
                        //    {
                        //        tongtien = (float)(Double)(tongtien + Convert.ToDouble(cellValueThanhTien));
                        //        txtTongtien.Text = tongtien.ToString();
                        //    }
                        //}
                        gridView.RefreshData();
                        GridColumn summaryColumn = gridView.Columns["THANHTIEN"];
                        tongtien = Convert.ToDouble(gridView.Columns["THANHTIEN"].SummaryItem.SummaryValue);

                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                    //DateTime parsedDate;
                    if (dtNgaytao.DateTime.ToString("MM/dd/yyyy HH:mm:ss").Length > 6 && cbDaily.SelectedValue.ToString().Trim().Length > 0)
                    {
                        string result = DAO.PhieuCuocDAO.Instance.Insert(dtNgaytao.DateTime.ToString("MM/dd/yyyy HH:mm:ss"), userName, loai, cbDaily.SelectedValue.ToString().Trim());

                        if (result != null)
                        {
                            bool resultCT = false;
                            for (int i = 0; i < gridView.RowCount; i++)
                            {
                                object cellValueMaHH = gridView.GetRowCellValue(i, "MAHH");
                                object cellValueSL = gridView.GetRowCellValue(i, "SL");
                                if (cellValueMaHH.ToString().Trim().Length > 0 && cellValueSL.ToString().Trim().Length > 0)
                                {
                                    if (cbPTTT.Text.Length > 0)
                                    {
                                        resultCT = DAO.CTPhieuCuocDAO.Instance.Insert(result, cellValueMaHH.ToString().Trim().ToUpper(), Convert.ToInt32(cellValueSL));
                                        DAO.PhieuThuChiDAO.Instance.Insert(dtNgaytao.DateTime.ToString("MM/dd/yyyy HH:mm:ss"), userName, cbPTTT.Text, Convert.ToInt32(tongtien), mvv, result.Trim());
                                        DTO.Vckcuoc resultVCK = DAO.VCKDAO.Instance.Get(cbDaily.SelectedValue.ToString().Trim(), cellValueMaHH.ToString().Trim());
                                        if (loai == "Nhập")
                                        {
                                            if (resultVCK != null)
                                            {
                                                DTO.Vckcuoc dataVCKSL = DAO.VCKDAO.Instance.Get(cbDaily.SelectedValue.ToString().Trim(), cellValueMaHH.ToString().Trim().ToUpper());
                                                DAO.VCKDAO.Instance.Update(cbDaily.SelectedValue.ToString().Trim(), cellValueMaHH.ToString().Trim(), Convert.ToInt32(cellValueSL) + Convert.ToInt32(resultVCK.SlCuoc), Convert.ToInt32(dataVCKSL.SlGiu));                           
                                            }
                                            else
                                            {
                                                DAO.VCKDAO.Instance.Insert(cbDaily.SelectedValue.ToString().Trim(), cellValueMaHH.ToString().Trim(), Convert.ToInt32(cellValueSL));
                                            }
                                        }
                                        if (loai == "Trả")
                                        {
                                            if (resultVCK != null)
                                            {
                                                DTO.Vckcuoc dataVCKSL = DAO.VCKDAO.Instance.Get(cbDaily.SelectedValue.ToString().Trim(), cellValueMaHH.ToString().Trim().ToUpper());
                                                DAO.VCKDAO.Instance.Update(cbDaily.SelectedValue.ToString().Trim(), cellValueMaHH.ToString().Trim(), Convert.ToInt32(resultVCK.SlCuoc) - Convert.ToInt32(cellValueSL), Convert.ToInt32(dataVCKSL.SlGiu));
                                            }
                                            else
                                            {
                                            MessageBox.Show("Khách hàng chưa cược loại vỏ " + cellValueMaHH);
                                            }
                                        }


                                }
                            }
                                else
                                {
                                    break;
                                }


                            }
                            if (resultCT)
                            {
                                ChildFormEvent?.Invoke(this, EventArgs.Empty);
                                XtraMessageBox.Show("Tạo phiếu thành công!");
                                this.Close();
                            }
                            else
                            {
                                XtraMessageBox.Show("Lỗi nhập số lượng và hàng hóa");
                            }

                        }
                        else
                        {
                            XtraMessageBox.Show("Tạo phiếu không thành công!");
                        }
                    }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi kết nối hệ thống");
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

        }

        private void cbDaily_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loai == "Nhập")
            {
               txtNoidung.Text = "Thu tiền cược VCK của " + cbDaily.Text;
            }
            if (loai == "Trả")
            {
                txtNoidung.Text = "Trả tiền cược VCK của " + cbDaily.Text;
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-33G4CSH;Initial Catalog=QLBH_HBC;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT MAPC, NGAYTAO, MADL, TENDL, HOTEN FROM PHIEUCUOC JOIN DAILY ON MADL = MA_DL JOIN NGUOIDUNG ON NGUOITAO = USERNAME WHERE MAPC = 'PC0003'",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            con.Close();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT MAHH, TENHH, DVT, CT_PHIEUCUOC.SL, GIACUOC FROM CT_PHIEUCUOC JOIN PHIEUCUOC ON MAPC = MA_PC JOIN HANGHOA ON MAHH = MA_VO WHERE MAPC = 'PC0003'", con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt3 = new DataTable();
            da1.Fill(dt3);
            
            // Create a new report instance
            rptPhieucuoc report = new rptPhieucuoc();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt2);
            ds.Tables[0].TableName = "PHIEUCUOC_1"; 
            ds.Tables.Add(dt3);
            ds.Tables[1].TableName = "CT_PHIEUCUOC_1";
            report.DataSource = ds;
            report.ShowPreviewDialog();

        }
    }
}
