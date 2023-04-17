using DevExpress.XtraEditors;
using DevExpress.XtraReports.Parameters;
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

namespace QLBH_HBC
{
    public partial class frmInBCDM : DevExpress.XtraEditors.XtraForm
    {
        public frmInBCDM()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-33G4CSH;Initial Catalog=QLBH_HBC;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT MAHH,TENHH,DONGIA,GIACUOC,DVT,CO_VCK,LOAI FROM HANGHOA WHERE LOAI = N'Vỏ'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

           
            // Create a new report instance
            rptDMHH rpt = new rptDMHH();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "HANGHOA";

            //CalculatedField calculatedField = null;
            foreach (CalculatedField field in rpt.CalculatedFields)
            {
                if (field.Name == "Loai")
                {
                    MessageBox.Show("Có Loai");
                    CalculatedField Nguoitao = field;
                    Nguoitao.Expression = "'Vỏ'";
                }
                if (field.Name == "Nguoitao")
                {
                    MessageBox.Show("Có Nguoitao");
                    CalculatedField Nguoitao = field;
                    Nguoitao.Expression = "'Nguyễn Thị Ngọc Hương'";
                }
            }
            rpt.DataSource = ds;
            rpt.ShowPreviewDialog();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}