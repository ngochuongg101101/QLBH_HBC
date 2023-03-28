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
        Boolean addnewflag = false;

        public uc_Hanghoa()
        {
            InitializeComponent();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM HANGHOA";
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
            txtGiacuoc.Text = row["GIACUOC"].ToString();
            chkCoVCK.Checked = Convert.ToBoolean(row["CO_VCK"].ToString());


            string sql1 = "SELECT MAHH,TENHH,BOOM.SL,DVT FROM HANGHOA,BOOM WHERE MAHH = MA_VO AND MA_BIA = '" + txtMaHH.Text + "'";
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

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtTenHH.Enabled = true;
            cbLoai.Enabled = true;
            cbDVT.Enabled = true;
            txtGiaban.Enabled = true;
            txtGiacuoc.Enabled = true;

            gridView1.AddNewRow();
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            NapCT();
            //txtTenHH.Focus();
            //btnSave.Enabled = true;

            //for (int i = 1; i <= 20; i++)
            //{
            //    gridView2.AddNewRow();
            //}
            //gridView2.OptionsBehavior.Editable = true;
            //gridView2.OptionsBehavior.ReadOnly = false;
            //gridView2.Appearance.Row.BackColor = Color.Empty;
            //addnewflag = true;

            //string sql2 = "Select distinct DVT from HANGHOA";
            //DataTable dt = Config.DataProvider.Instance.ExecuteQuery(sql2);
            //cbDVT.DataSource = dt;
            //cbDVT.DisplayMember = "DVT";
            //cbDVT.Text = "";
            //cbDVT.ValueMember = "DVT";
        }

        private void gridView2_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                // Lấy thông tin ô đang được chọn
                int rowHandle = gridView2.FocusedRowHandle;
                string fieldName = gridView2.FocusedColumn.FieldName;

                DataRow row = gridView2.GetDataRow(rowHandle);

                // Lấy mã hàng hóa được thay đổi
                string maHH = row["MAHH"].ToString();

                // Kiểm tra nếu cột MAHH đã được cập nhật
                if (fieldName == "MAHH" && !row.IsNull("MAHH"))
                 {
                    // Lấy thông tin hàng hóa từ cơ sở dữ liệu
                    string sql = "SELECT TENHH, DVT, FROM HANGHOA WHERE MAHH = '" + maHH.Trim().ToUpper() + "'";
                    DataTable dt = Config.DataProvider.Instance.ExecuteQuery(sql);

                    // Hiển thị thông tin hàng hóa lên gridView
                    if (dt.Rows.Count > 0)
                    {
                    gridView2.SetRowCellValue(rowHandle, "TENHH", dt.Rows[0]["TENHH"].ToString());
                    gridView2.SetRowCellValue(rowHandle, "DVT", dt.Rows[0]["DVT"].ToString());                        
                    }
                    
                }

            }
        }
    }
}
