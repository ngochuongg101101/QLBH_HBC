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
    public partial class uc_BCNTCUOC : DevExpress.XtraEditors.XtraUserControl
    {
        private string username;
        public uc_BCNTCUOC(string userName)
        {
            InitializeComponent();
            username = userName;
        }

        private void uc_BCNTCUOC_Load(object sender, EventArgs e)
        {
            string sql = "Select MADL,TENDL from DAILY";
            DataTable dt = Config.DataProvider.Instance.ExecuteQuery(sql);
            cbDaily.DataSource = dt;
            cbDaily.DisplayMember = "TENDL";
            cbDaily.Text = "";
            cbDaily.ValueMember = "MADL";

            string sql1 = "Select MAHH,TENHH from HANGHOA WHERE LOAI = N'Vỏ'";
            DataTable dt1 = Config.DataProvider.Instance.ExecuteQuery(sql1);
            cbLoaivo.DataSource = dt1;
            cbLoaivo.DisplayMember = "TENHH";
            cbLoaivo.Text = "";
            cbLoaivo.ValueMember = "MAHH";
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            DTO.Nguoidung Hoten = DAO.NguoidungDAO.Instance.GetFullNameByUsername(username);
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-33G4CSH;Initial Catalog=QLBH_HBC;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT MA_PC, NGAYTAO, MAHH, TENHH, DVT, CT_PHIEUCUOC.SL, PHIEUCUOC.LOAI FROM HANGHOA, PHIEUCUOC, CT_PHIEUCUOC WHERE MAPC = MA_PC AND MAHH = MA_VO AND MA_DL = '"+ cbDaily.SelectedValue.ToString() +"' AND MA_VO = '"+ cbLoaivo.SelectedValue.ToString() +"' " +
                "AND NGAYTAO BETWEEN '"+ dtNgay1.DateTime.ToString("yyyy/MM/dd") + "' AND '"+ dtNgay2.DateTime.ToString("yyyy/MM/dd") + "'", con);
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            rptNhapcuoc rpt = new rptNhapcuoc();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "CT_PHIEUCUOC";
            foreach (CalculatedField field in rpt.CalculatedFields)
            {
                if (field.Name == "Daily")
                {
                    CalculatedField calculatedField = field;
                    calculatedField.Expression = "'" + cbDaily.Text + "'";
                }
                if (field.Name == "Hanghoa")
                {
                    CalculatedField calculatedField = field;
                    calculatedField.Expression = "'" + cbLoaivo.Text + "'";
                }
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
                if (field.Name == "Nguoitao")
                {
                    CalculatedField calculatedField = field;
                    calculatedField.Expression = "'" + Hoten.Hoten.ToString() + "'";
                }
            }
            rpt.DataSource = ds;
            rpt.ShowPreviewDialog();
        }
    }
}
