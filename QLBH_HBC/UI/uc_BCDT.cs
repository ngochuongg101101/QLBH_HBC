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
    public partial class uc_BCDT : DevExpress.XtraEditors.XtraUserControl
    {
        private string username;
        public uc_BCDT(string userName)
        {
            InitializeComponent();
            username = userName;
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DTO.Nguoidung Hoten = DAO.NguoidungDAO.Instance.GetFullNameByUsername(username);
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-33G4CSH;Initial Catalog=QLBH_HBC;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT MADL,TENDL,MST,SUM(HOADON.TONGTIEN) AS DOANHTHU FROM DAILY, DONHANG, HOADON WHERE MADL = MA_DL AND MADH = MA_DH " +
                "AND HOADON.NGAYTAO BETWEEN '" + dtNgay1.DateTime.ToString("yyyy/MM/dd") + "' AND '" + dtNgay2.DateTime.ToString("yyyy/MM/dd") + "'" +
                "GROUP BY MADL, TENDL, MST", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            BCDT rpt = new BCDT();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "DAILY";
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
