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
        UI.uc_Cuocvo ucCuocvo;

        private void mnCuocvo_Click(object sender, EventArgs e)
        {
            if(ucCuocvo==null)
            {
                ucCuocvo = new UI.uc_Cuocvo();
                ucCuocvo.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucCuocvo);
                ucCuocvo.BringToFront();
            }
            
                lblTieude.Caption = mnKyguihang.Text; //sau đặt tên nút menu kiu gì được ta
        }
    }
}
