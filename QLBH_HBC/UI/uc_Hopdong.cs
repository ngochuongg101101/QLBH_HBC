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
    public partial class uc_Hopdong : DevExpress.XtraEditors.XtraUserControl
    {
        private string userName;
        public uc_Hopdong(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }
    }
}
