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
        private string userName;
        public MainForm(string username)
        {
            InitializeComponent();
            this.userName = username;
        }
        UI.uc_Cuocvo ucCuocvo;
        UI.uc_Donhang ucDonhang;
        UI.uc_Kho ucKho;
        UI.uc_Hanghoa ucHanghoa;


        private void mnCuocvo_Click(object sender, EventArgs e)
        {
            if(ucCuocvo==null)
            {
                ucCuocvo = new UI.uc_Cuocvo(userName);
                ucCuocvo.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucCuocvo);
                ucCuocvo.BringToFront();
            }
            
                //lblTieude.Caption = mnKyguihang.Text; 
        }

        private void mnQLDH_Click(object sender, EventArgs e)
        {
            if (ucDonhang == null)
            {
                ucDonhang = new UI.uc_Donhang();
                ucDonhang.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucDonhang);
                ucDonhang.BringToFront();
            }
        }

        private void mnQLKho_Click(object sender, EventArgs e)
        {
            if (ucKho == null)
            {
                ucKho = new UI.uc_Kho();
                ucKho.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucKho);
                ucKho.BringToFront();
            }
        }

        private void mnDMHanghoa_Click(object sender, EventArgs e)
        {
            if (ucHanghoa == null)
            {
                ucHanghoa = new UI.uc_Hanghoa();
                ucHanghoa.Dock = DockStyle.Fill;
                mainContainer.Controls.Add(ucHanghoa);
                ucHanghoa.BringToFront();
            }
        }
    }
}
