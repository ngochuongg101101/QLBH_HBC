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
        public Mainform()
        {
            InitializeComponent();
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
