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
    public partial class MainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public MainForm()
        {
            InitializeComponent();
            
        }
        uc_Cuocvo ucCuocvo;

        private void Mainform_Load(object sender, EventArgs e)
        {

        }

        private void accordionControl1_Click(object sender, EventArgs e)
        {

        }

        private void mnCuocvo_Click(object sender, EventArgs e)
        {
            if (ucCuocvo == null)
            {
                ucCuocvo = new uc_Cuocvo();
                ucCuocvo.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucCuocvo);
                ucCuocvo.BringToFront();
            }
            else
                ucCuocvo.BringToFront();
                lblTieude.Caption = mnKyguihang.Text; //sau đặt tên nút menu kiu gì được ta
        }
    }
}
