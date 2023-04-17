using DevExpress.XtraEditors;
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
        public uc_BCDT()
        {
            InitializeComponent();
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-33G4CSH;Initial Catalog=QLBH_HBC;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM HOPDONG", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            //rptBCDT rpt = new rptDMHD();
            //DataSet ds = new DataSet();
            //ds.Tables.Add(dt);
            //ds.Tables[0].TableName = "HOPDONG";

            //foreach (CalculatedField field in rpt.CalculatedFields)
            //{
            //    if (field.Name == "Nguoitao")
            //    {
            //        CalculatedField Nguoitao = field;
            //        Nguoitao.Expression = "'" + Hoten.Hoten.ToString() + "'";
            //    }
            //}
            //rpt.DataSource = ds;
            //rpt.ShowPreviewDialog();
        }
    }
}
