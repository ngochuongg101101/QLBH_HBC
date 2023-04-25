using DevExpress.XtraEditors;
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
    public partial class uc_Thanhtoan : DevExpress.XtraEditors.XtraUserControl
    {
        public frmThanhtoancongno frmThanhtoancongno;
        private string username;
        public uc_Thanhtoan(string userName)
        {
            InitializeComponent();
            username = userName;
        }

        private void btnPay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = new frmThanhtoancongno(username, frmThanhtoancongno);
            f.Show();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT MADL,TENDL,MST,TONGNO FROM DAILY WHERE TONGNO != 0";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.Refresh();
        }
    }
}
