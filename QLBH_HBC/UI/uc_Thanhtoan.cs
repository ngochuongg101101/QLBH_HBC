using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
            frmThanhtoancongno f = new frmThanhtoancongno(username, frmThanhtoancongno);
            f.Show();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT MADL,TENDL,MST,TONGNO FROM DAILY WHERE TONGNO != 0";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.Refresh();
            gridView1.RowClick += gridView1_RowClick;
            gridView2.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }

        private void gridControl1_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = gridControl1.MainView as GridView;
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));
                if (info.InRow)
                {
                    view.FocusedRowHandle = info.RowHandle;
                    view.SelectRow(info.RowHandle);
                    gridView1_RowClick(sender, null);
                }
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            string sql1 = "SELECT MAPTC,HOADON.NGAYTAO,TENDL,MA_HD,HOADON.TONGTIEN FROM PHIEUTHUCHI,HOADON,DONHANG,DAILY WHERE MAHD=MA_HD AND MADH=MA_DH AND MADL=MA_DL AND " +
                            "MADL= '" + gridView1.GetRowCellValue(rowHandle, "MADL").ToString() + "'";
            gridControl2.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql1);
            gridControl2.Refresh();
            gridView2.OptionsBehavior.ReadOnly = true;
            gridView2.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }
    }
}
