using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using QLBH_HBC.Reports;
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

namespace QLBH_HBC.UI
{
    public partial class uc_BCDM : DevExpress.XtraEditors.XtraUserControl
    {
        private string username;
        public uc_BCDM(string userName)
        {
            InitializeComponent();
            username = userName;
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DTO.Nguoidung Hoten = DAO.NguoidungDAO.Instance.GetFullNameByUsername(username);

            if (btnDMHH.Checked == true)
            {
                if (cbLoai.Text == "")
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-33G4CSH;Initial Catalog=QLBH_HBC;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MAHH,TENHH,DONGIA,GIACUOC,DVT,CO_VCK,LOAI FROM HANGHOA", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    rptDMHH rpt = new rptDMHH();
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    ds.Tables[0].TableName = "HANGHOA";

                    foreach (CalculatedField field in rpt.CalculatedFields)
                    {
                        if (field.Name == "Loai")
                        {
                            CalculatedField Nguoitao = field;
                            Nguoitao.Expression = "'Bia, Vỏ'";
                        }
                        if (field.Name == "Nguoitao")
                        {
                            CalculatedField Nguoitao = field;
                            Nguoitao.Expression = "'" + Hoten.Hoten.ToString() + "'";
                        }
                    }
                    rpt.DataSource = ds;
                    rpt.ShowPreviewDialog();
                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-33G4CSH;Initial Catalog=QLBH_HBC;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MAHH,TENHH,DONGIA,GIACUOC,DVT,CO_VCK,LOAI FROM HANGHOA WHERE LOAI =N'" + cbLoai.Text + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    rptDMHH rpt = new rptDMHH();
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    ds.Tables[0].TableName = "HANGHOA";

                    foreach (CalculatedField field in rpt.CalculatedFields)
                    {
                        if (field.Name == "Loai")
                        {
                            CalculatedField Nguoitao = field;
                            Nguoitao.Expression = "'" + cbLoai.Text + "'";
                        }
                        if (field.Name == "Nguoitao")
                        {
                            CalculatedField Nguoitao = field;
                            Nguoitao.Expression = "'" + Hoten.Hoten.ToString() + "'";
                        }
                    }
                    rpt.DataSource = ds;
                    rpt.ShowPreviewDialog();
                }
            }
            if (btnDMDL.Checked == true)
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-33G4CSH;Initial Catalog=QLBH_HBC;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 10 * FROM DAILY", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                rptDMDL rpt = new rptDMDL();
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "DAILY";

                foreach (CalculatedField field in rpt.CalculatedFields)
                {
                    if (field.Name == "Nguoitao")
                    {
                        CalculatedField Nguoitao = field;
                        Nguoitao.Expression = "'" + Hoten.Hoten.ToString() + "'";
                    }
                }
                rpt.DataSource = ds;
                rpt.ShowPreviewDialog();
            }
            if (btnDMHD.Checked == true)
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-33G4CSH;Initial Catalog=QLBH_HBC;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT MAHD,NGAYTAO,NGUOITAO,NGAYBD,NGAYKT,CK,TENDL FROM HOPDONG,DAILY WHERE MADL=MA_DL " +
                    "AND NGAYTAO BETWEEN '"+ dtNgay1.DateTime.ToString("yyyy/MM/dd") + "' AND '"+ dtNgay2.DateTime.ToString("yyyy/MM/dd") + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                rptDMHD rpt = new rptDMHD();
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "HOPDONG";

                foreach (CalculatedField field in rpt.CalculatedFields)
                {
                    if (field.Name == "Day1")
                    {
                        CalculatedField calculatedField = field;
                        calculatedField.Expression = "'" + dtNgay1.DateTime.ToString("dd/MM/yyyy") + "'";
                    }
                    if (field.Name == "Day2")
                    {
                        CalculatedField calculatedField = field;
                        calculatedField.Expression = "'" + dtNgay2.DateTime.ToString("dd/MM/yyyy") + "'";
                    }
                    if (field.Name == "Username")
                    {
                        CalculatedField calculatedField = field;
                        calculatedField.Expression = "'" + Hoten.Hoten.ToString() + "'";
                    }
                }
                rpt.DataSource = ds;
                rpt.ShowPreviewDialog();
            }
        }

        private void btnDMHH_CheckedChanged(object sender, EventArgs e)
        {
            if (btnDMHH.Checked == true)
            {
                gpLocHH.Visible = true;
                gpLocHD.Visible = false;
            }    
            else
            {
                gpLocHH.Visible = false;
            }
        }

        private void btnDMHD_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(gpLocHD.Visible.ToString());
            if (btnDMHD.Checked == true)
            {
                gpLocHD.Visible = true;
                gpLocHH.Visible = false;

            }
            else
            {
                gpLocHD.Visible = false;
                gpLocHH.Visible = false;
            }
        }
    }
}
