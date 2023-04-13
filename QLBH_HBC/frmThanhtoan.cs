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

namespace QLBH_HBC
{
    public partial class frmThanhtoan : DevExpress.XtraEditors.XtraForm
    {
        private string username;
        public frmThanhtoan(string userName)
        {
            InitializeComponent();
            username = userName;
        }

    }
}