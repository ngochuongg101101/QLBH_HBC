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
    public partial class uc_Kho : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_Kho()
        {
            InitializeComponent();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT MAPK,NGAYTAO,NGUOITAO,NOIDUNG,PTVC,BIENSO,TENLOAI,MA_DH FROM PHIEUKHO " +
                "JOIN LOAIPK ON MALPK=MA_LPK";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.Refresh();
            gridView1.RowClick += gridView1_RowClick;
            gridView1.OptionsBehavior.Editable = true;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            NapCT();
        }

        private void NapCT()
        {
            var row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            // Lấy data lên gridview
            cbLoaiPK.Text = row["TENLOAI"].ToString();
            txtMapk.Text = row["MAPK"].ToString();
            dtNgaytao.Text = row["NGAYTAO"].ToString();
            txtNguoitao.Text = row["NGUOITAO"].ToString();
            txtPTVC.Text = row["PTVC"].ToString();
            txtBienso.Text = row["BIENSO"].ToString();
            txtNoidung.Text = row["NOIDUNG"].ToString();
            txtMadh.Text = row["MA_DH"].ToString();

            string sql1 = "SELECT MAHH, TENHH, CT_DONHANG.SL, DVT FROM CT_DONHANG JOIN HANGHOA ON MAHH = MA_HH " +
                "WHERE MA_DH = '" + txtMadh.Text + "'";
            gridControl2.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql1);
            gridControl2.Refresh();
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

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;

            NapCT();
            btnSave.Enabled = true;
            txtMapk.Enabled = false;
            dtNgaytao.EditValue = DateTime.Today;

            for (int i = 1; i <= 20; i++)
            {
                gridView2.AddNewRow();
            }

            //Lấy lên list Loại PK -> cbLoaiPK, hiện value đầu tiên là LPK0001
            //

        }

        private void cbLoaiPK_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nếu = LPK0001 thì ok
            //còn lại -> txtMadh.Visible = false;
        }
    }
}
