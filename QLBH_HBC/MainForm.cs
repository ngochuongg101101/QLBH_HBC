using DevExpress.XtraBars;
using QLBH_HBC.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBH_HBC
{
    public partial class Mainform : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        string t1 = "", t2 = "", t3 = "";
        public Mainform(string t1, string t2, string t3)
        {
            InitializeComponent();
            this.t1 = t1; this.t2 = t2; this.t3 = t3;
        }
        uc_Kiguihang ucKiguihang;

        private void mnKyguihang_Click(object sender, EventArgs e)
        {
            if (ucKiguihang==null)
            {
                ucKiguihang = new uc_Kiguihang();
                ucKiguihang.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucKiguihang);
                ucKiguihang.BringToFront();
            }
            else
                ucKiguihang.BringToFront();
            lblTieude.Caption = mnKyguihang.Text;
        }
    }
}
