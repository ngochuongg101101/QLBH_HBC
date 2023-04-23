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
            SqlCommand cmd = new SqlCommand("SELECT MAHH,TENHH,DONGIA,GIACUOC,DVT,CO_VCK,LOAI FROM HANGHOA", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            rptNhapcuoc rpt = new rptNhapcuoc();
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
    }
}
