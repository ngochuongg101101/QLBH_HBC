using DevExpress.XtraEditors;
using DevExpress.XtraReports.Data;
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
    public partial class uc_Cuocvo : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_Cuocvo()
        {
            InitializeComponent();
        }

        private void btnNhapcuoc_Click(object sender, EventArgs e)
        {
            Form f = new frmNhapcuoc();
            f.Show();
        }

        private void gcVCKcuoc_Load(object sender, EventArgs e)
        {
            string sql = "Select MA_DL as N'Mã đại lý',DAILY.TENDL as N'Tên đại lý',MA_VO as N'Mã vỏ',TENHH as N'Tên vỏ',SL_CUOC as N'Số lượng đã cược',SL_GIU as N'Số lượng đang giữ' " +
                "from VCKCUOC,HANGHOA, DAILY WHERE VCKCUOC.MA_DL=DAILY.MADL AND VCKCUOC.MA_VO=HANGHOA.MAHH";
            gcVCKcuoc.DataSource = DataProvider.Instance.ExecuteQuery(sql);
            gcVCKcuoc.Refresh();
        }
    }
}
