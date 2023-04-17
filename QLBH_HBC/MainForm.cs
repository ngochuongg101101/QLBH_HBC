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
        UI.uc_Cuocvo ucCuocvo;
        UI.uc_Donhang ucDonhang;
        public UI.uc_Kho ucKho;
        UI.uc_Hanghoa ucHanghoa;
        UI.uc_Daily ucDaily;
        UI.uc_Hopdong ucHopdong;
        UI.uc_Hoadon ucHoadon;
        UI.uc_Thanhtoan ucThanhtoan;
        UI.uc_Baocao ucBaocao;
        UI.uc_BCDM ucBCDM;

        private string userName;
        public MainForm(string username)
        {
            InitializeComponent();
        }

        private void mnCuocvo_Click(object sender, EventArgs e)
        {
            ucCuocvo = new UI.uc_Cuocvo(userName);
            ucCuocvo.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(ucCuocvo);
            ucCuocvo.BringToFront();
        }

        private void mnQLDH_Click(object sender, EventArgs e)
        {
            ucDonhang = new UI.uc_Donhang(userName);
            ucDonhang.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(ucDonhang);
            ucDonhang.BringToFront();
        }

        private void mnQLKho_Click(object sender, EventArgs e)
        {
            ucKho = new UI.uc_Kho(userName, ucKho);
            ucKho.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(ucKho);
            ucKho.BringToFront();
        }

        private void mnDMHanghoa_Click(object sender, EventArgs e)
        {
            ucHanghoa = new UI.uc_Hanghoa(userName);
            ucHanghoa.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(ucHanghoa);
            ucHanghoa.BringToFront();
        }

        private void mnDaily_Click(object sender, EventArgs e)
        {
            ucDaily = new UI.uc_Daily(userName);
            ucDaily.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(ucDaily);
            ucDaily.BringToFront();
        }

        private void mnHopdong_Click(object sender, EventArgs e)
        {
            ucHopdong = new UI.uc_Hopdong(userName);
            ucHopdong.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(ucHopdong);
            ucHopdong.BringToFront();
        }

        private void mnBaocao_Click(object sender, EventArgs e)
        {
            ucBaocao = new UI.uc_Baocao(userName);
            ucBaocao.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(ucBaocao);
            ucBaocao.BringToFront();
        }

        private void mnBCDM_Click(object sender, EventArgs e)
        {
            ucBCDM = new UI.uc_BCDM(userName);
            ucBCDM.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(ucBCDM);
            ucBCDM.BringToFront();
        }

        private void mnQLThanhtoan_Click(object sender, EventArgs e)
        {
            ucThanhtoan = new UI.uc_Thanhtoan(userName);
            ucThanhtoan.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(ucThanhtoan);
            ucThanhtoan.BringToFront();
        }

        private void mnQLHoadon_Click(object sender, EventArgs e)
        {
            ucHoadon = new UI.uc_Hoadon(userName);
            ucHoadon.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(ucHoadon);
            ucHoadon.BringToFront();
        }
    }
}
