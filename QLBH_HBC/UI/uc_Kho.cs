﻿using DevExpress.XtraEditors;
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
        private string userName;
        public uc_Kho(string username)
        {
            this.userName = username;
        }
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
            gridView1.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            NapCT();
        }

        private void NapCT()
        {
            var row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            cbLoaiPK.Text = row["TENLOAI"].ToString();
            txtMapk.Text = row["MAPK"].ToString();
            dtNgaytao.Text = row["NGAYTAO"].ToString();
            txtNguoitao.Text = row["NGUOITAO"].ToString();
            txtPTVC.Text = row["PTVC"].ToString();
            txtBienso.Text = row["BIENSO"].ToString();
            txtNoidung.Text = row["NOIDUNG"].ToString();
            btnMadh.Text = row["MA_DH"].ToString();

            string sql1 = "SELECT MAHH, TENHH, CT_DONHANG.SL, DVT FROM CT_DONHANG JOIN HANGHOA ON MAHH = MA_HH " +
                "WHERE MA_DH = '" + btnMadh.Text + "'";
            gridControl2.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql1);
            gridControl2.Refresh();
            gridView2.OptionsBehavior.ReadOnly = true;
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

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;

            NapCT();
            btnSave.Enabled = true;
            txtMapk.Enabled = false;
            dtNgaytao.EditValue = DateTime.Today;
            //txtNguoitao = username;

            for (int i = 1; i <= 20; i++)
            {
                gridView2.AddNewRow();
            }
            gridView2.OptionsBehavior.Editable = true;
            gridView2.OptionsBehavior.ReadOnly = false;
            gridView2.Appearance.Row.BackColor = Color.Empty;

            //Lấy lên từ table LOAIPK -> cbLoaiPK, value = MALPK, displaymember = TENLOAI,
            //hiển thị sẵn value (LPK0001) - displaymember (Xuất bán hàng) (loại thường xuyên nhất -> sau đó ng dùng có thể đổi)
            //
            
        }

        private void cbLoaiPK_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nếu = LPK0001 thì txtMadh.Visible = true;
            //còn lại -> txtMadh.Visible = false; (do chỉ có trường hợp xuất bán hàng là có đi kèm mã đơn hàng)
        }

        private void btnMadh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //Người dùng bấm vào ô tìm kiếm sẽ chạy ra frmDonhang -> gửi đi 1 tham số vào frmDonhang
            frmDonhang f = new frmDonhang();
            f.Show();
            //Khi kích đúp vào 1 dòng -> sẽ truyền lại thông tin MADH vào tham số đã gửi đến
            //Hiển thị tham số đó lên btnMadh.Text

        }

        private void btnMadh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Sau khi người dùng điền xong Mã đơn hàng và ấn Enter
                //Truy cập vào bảng CT_DONHANG, HANGHOA lấy lên MAHH, TENHH, SL, DVT
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //1. Insert PHIEUKHO (NGAYTAO, NGUOITAO, NOIDUNG, PTVC, BIENSO, MA_LPK, MA_DH)
            //
            //2. Check thiếu thông tin
            //- Check txtNgaytao, txtNguoitao, MaHH, SL
            //- Nếu MALPK = 'LPK0001' -> Check thêm btnMadh

            //2. Check tồn + VCK cược
            //- Từ MALPK -> vào bảng LOAIPK xem LOAI -> Nếu LOAI = "Xuất" 
            //  -> Kiểm tra xem có vượt tồn kho ở Hàng hóa không -> nếu vượt -> hiện thông báo chặn
            //- Kiểm tra nếu MALPK = "LPK0001"
            //  -> Kiểm tra SL có vượt SL khả dụng = SL_CUOC - SL_GIU -> nếu vượt -> hiện thông báo chặn
            //
            //3. Insert CT_PHIEUKHO (MAPK (vừa tạo), MAHH, SL)
            //
            //4. Update SL (tồn kho) ở bảng HANGHOA
            //      + Nếu LOAI = "Nhập" -> cộng thêm
            //      + Nếu LOAI = "Xuất" -> trừ đi

            //5. Update SL giữ trong VCKCUOC
            //- Nếu MALPK = "LPK0001" 
            //- Với mã vỏ -> Lấy lên SL_GIU ở bảng VCKCUOC
            //- SL_GIU mới = SL_GIU hiện tại + SL trong đơn hàng

        }
    }
}
