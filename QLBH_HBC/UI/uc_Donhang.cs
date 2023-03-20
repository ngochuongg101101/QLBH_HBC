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
    public partial class uc_Donhang : DevExpress.XtraEditors.XtraUserControl
    {
        int i;
        public uc_Donhang()
        {
            InitializeComponent();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT TRANGTHAI, MADH , NGAYTAO , TENDL , NGUOITAO  FROM DONHANG " +
                        "JOIN DAILY ON MADL = MA_DL";
            gridControl1.DataSource = DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.Refresh();
            gridView1.RowClick += gridView1_RowClick;
            gridView1.OptionsBehavior.Editable = true;
        }

        //private void NapCT(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        //{
        //    MessageBox.Show("Hello");
        //    txtMadh.Text = gridView1.GetRowCellValue(e.RowHandle, "MADH").ToString();
        //    dtNgaytao.Text = gridView1.GetRowCellValue(e.RowHandle, "NGAYTAO").ToString();
        //    txtNguoitao.Text = gridView1.GetRowCellValue(e.RowHandle, "HOTEN").ToString();
        //    cbDaily.DisplayMember = gridView1.GetRowCellValue(e.RowHandle, "TENDL").ToString();
        //    //string trangthai = gridView1.GetRowCellValue(e.RowHandle, "TRANGTHAI").ToString();
        //}


        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //NapCT(sender, e);
            MessageBox.Show("Hihi");

            var row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            // Lấy các giá trị MADH, NGAYTAO, HOTEN, TENDL, TRANGTHAI
            txtMadh.Text = row["MADH"].ToString();
            dtNgaytao.Text = row["NGAYTAO"].ToString();
            txtNguoitao.Text = row["NGUOITAO"].ToString();
            cbDaily.Text= row["TENDL"].ToString();
            //string trangthai = row["TRANGTHAI"].ToString();

            //string madh = gridView1.GetRowCellValue(e.RowHandle, "MADH").ToString();
            //MessageBox.Show(madh);
            //txtMadh.Text = gridView1.GetRowCellValue(e.RowHandle, "MADH").ToString();
            //dtNgaytao.Text = gridView1.GetRowCellValue(e.RowHandle, "NGAYTAO").ToString();
            //txtNguoitao.Text = gridView1.GetRowCellValue(e.RowHandle, "HOTEN").ToString();
            //cbDaily.DisplayMember = gridView1.GetRowCellValue(e.RowHandle, "TENDL").ToString();
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {

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
