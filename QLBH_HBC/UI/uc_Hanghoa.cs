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
    public partial class uc_Hanghoa : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_Hanghoa()
        {
            InitializeComponent();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT *  FROM HANGHOA";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridView1.RowClick += gridView1_RowClick;
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }

        private void NapCT()
        {
            var row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            // Lấy các giá trị MADH, NGAYTAO, HOTEN, TENDL, TRANGTHAI
            txtMaHH.Text = row["MAHH"].ToString();
            txtTenHH.Text = row["TENHH"].ToString();
            cbLoai.Text = row["LOAI"].ToString();
            cbDVT.Text = row["DVT"].ToString();
            txtGiaban.Text = row["DONGIA"].ToString();
            txtGiaban.Text = row["GIACUOC"].ToString();

            
            string sql1 = "SELECT * FROM HANGHOA,BOOM WHERE MA_BIA = '" + txtMaHH.Text + "'";
            gridControl2.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql1);
            gridControl2.Refresh();
            //gridView2.OptionsBehavior.ReadOnly = true;
            gridView2.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            NapCT();
            txtMaHH.Enabled = false;
            txtTenHH.Enabled = false;
            cbLoai.Enabled = false;
            cbLoai.Enabled = false;
            cbDVT.Enabled = false;
            txtGiaban.Enabled = false;
            txtGiacuoc.Enabled = false;
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


    }
}
