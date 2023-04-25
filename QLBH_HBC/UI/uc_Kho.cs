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

        private string userName;
        private uc_Kho ucKho;
        public string maDH { get; set; }
        public string maDl { get; internal set; }

        public uc_Kho(string username, uc_Kho ucKho)
        {
            InitializeComponent();
            this.userName = username;
            this.ucKho = ucKho;
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
            txtNguoitao.Text = userName;

            for (int i = 1; i <= 20; i++)
            {
                gridView2.AddNewRow();
            }
            gridView2.OptionsBehavior.Editable = true;
            gridView2.OptionsBehavior.ReadOnly =  false;
            gridView2.Appearance.Row.BackColor = Color.Empty;
            List<DTO.LoaiPK> data_loaiPK = DAO.LoaiPKDAO.Instance.GetAll();
            //Lấy lên từ table LOAIPK -> cbLoaiPK, value = MALPK, displaymember = TENLOAI,
            //hiển thị sẵn value (LPK0001) - displaymember (Xuất bán hàng) (loại thường xuyên nhất -> sau đó ng dùng có thể đổi)
            cbLoaiPK.DataSource = data_loaiPK;
            cbLoaiPK.DisplayMember = "tenLPK";
            //DONE
            for (int i = gridView2.DataRowCount - 1; i >= 0; i--)
            {
                bool rowIsEmpty = true;
                for (int j = 0; j < gridView2.VisibleColumns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(gridView2.GetRowCellValue(i, gridView2.VisibleColumns[j]).ToString()))
                    {
                        rowIsEmpty = false;
                        break;
                    }
                }

                if (rowIsEmpty)
                {
                    gridView2.DeleteRow(i);
                }
            }
        }

        private void cbLoaiPK_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nếu = LPK0001 thì txtMadh.Visible = true;
            //còn lại -> txtMadh.Visible = false; (do chỉ có trường hợp xuất bán hàng là có đi kèm mã đơn hàng)
            if (cbLoaiPK.SelectedIndex != null)
            {
                DTO.LoaiPK loaiPK = (DTO.LoaiPK)cbLoaiPK.SelectedValue;
                if (loaiPK.MaLPK.Trim() == "LPK0001")
                {
                    btnMadh.Visible = true;
                }
                else
                {
                    btnMadh.Visible = false;
                }
            }
            //done
        }

        private void btnMadh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmDonhang f = new frmDonhang(this,null);
            f.ShowDialog();
            btnMadh.Text = maDH;
            int check = gridView2.DataRowCount - 1;
            if (check > 0)
            {
                for (int i = gridView2.DataRowCount - 1; i >= 0; i--)
                {
                    check = check - 1;
                    gridView2.DeleteRow(i);
                    break;
                }
            }
            if (maDH !=null && maDH.Trim().Length > 0 && check == 0)
            {
                int rowHandle = gridView2.FocusedRowHandle;
                DataRow row = gridView2.GetDataRow(rowHandle);

                List<DTO.CTDonhang> data_ctdonhang = DAO.CTDonhangDAO.Instance.GetMaHHByMaDH(maDH.Trim().ToUpper());
                if (data_ctdonhang != null)
                {
                    foreach (DTO.CTDonhang ctDonhang in data_ctdonhang)
                    {
                        if (ctDonhang.MaHH.Trim().ToUpper().Length > 0)
                        {
                            List<DTO.Hanghoa> hanghoa = DAO.HanghoaDAO.Instance.GetList(ctDonhang.MaHH.Trim().ToUpper());
                            if (hanghoa.Count > 0)
                            {

                                gridView2.BeginUpdate();
                                foreach (DTO.Hanghoa item in hanghoa)
                                {
                                    gridView2.AddNewRow();
                                    int newRowHandle = gridView2.RowCount - 1;

                                    gridView2.SetRowCellValue(rowHandle, "MAHH", item.MaHH);
                                    gridView2.SetRowCellValue(rowHandle, "TENHH", item.TenHH);
                                    gridView2.SetRowCellValue(rowHandle, "SL", ctDonhang.Sl);
                                    gridView2.SetRowCellValue(rowHandle, "DVT", item.Dvt);
                                    rowHandle = rowHandle + 1;
                                }
                                gridView2.EndUpdate();

                            }
                        }

                    }
                }
            }
            //Người dùng bấm vào ô tìm kiếm sẽ chạy ra frmDonhang -> gửi đi 1 tham số vào frmDonhang
            //Khi kích đúp vào 1 dòng -> sẽ truyền lại thông tin MADH vào tham số đã gửi đến
            //Hiển thị tham số đó lên btnMadh.
            //Done

        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //1. Insert PHIEUKHO (NGAYTAO, NGUOITAO, NOIDUNG, PTVC, BIENSO, MA_LPK, MA_DH)
            //
            //2. Check thiếu thông tin
            //- Check txtNgaytao, txtNguoitao, MaHH, SL
            //- Nếu MALPK = 'LPK0001' -> Check thêm btnMadh
            if (cbLoaiPK.Text.Trim().Length > 0)
            {
                if (btnMadh.Text.Trim().Length > 0)
                {
                    if (txtNguoitao.Text.Trim().Length > 0)
                    {
                        if (txtNoidung.Text.Trim().Length >= 0)
                        {
                            if (txtPTVC.Text.Trim().Length > 0)
                            {
                                if (txtBienso.Text.Trim().Length > 0)
                                {
                                    bool checkTon = false;
                                    bool checkSl = false;
                                    DTO.LoaiPK loaiPK = (DTO.LoaiPK)cbLoaiPK.SelectedValue;
                                    for (int i = 0; i < gridView2.RowCount; i++)
                                    {

                                        object cellValueMaHH = gridView2.GetRowCellValue(i, "MAHH");
                                        object cellValueSL = gridView2.GetRowCellValue(i, "SL");

                                        if (cellValueMaHH != null && cellValueMaHH.ToString().Length > 0)
                                        {
                                            bool checkHangHoaTon = DAO.HanghoaDAO.Instance.CheckSLTonTrongKho(cellValueMaHH.ToString().Trim().ToUpper(), Convert.ToInt32(cellValueSL.ToString().Trim()));
                                            if (checkHangHoaTon)
                                            {
                                                //
                                                bool checkVo = DAO.HanghoaDAO.Instance.GetByDataOtherByBark(cellValueMaHH.ToString().Trim());
                                                if (checkVo)
                                                {
                                                    if (loaiPK.MaLPK.Trim() == "LPK0004")
                                                    {

                                                        int slgiu = DAO.VCKDAO.Instance.GetSLGiu(maDl.Trim(), cellValueMaHH.ToString().Trim());
                                                        if (slgiu - Convert.ToInt32(cellValueSL.ToString().Trim()) < 0)
                                                        {
                                                            checkSl = true;
                                                            NapCT();
                                                            MessageBox.Show("Số lượng đại lý trả đang vượt quá số lượng đang giữ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                            break;
                                                        }


                                                    }

                                                    bool checkVCKCuoc = DAO.VCKDAO.Instance.Check(maDl.ToString().Trim(), cellValueMaHH.ToString().Trim());
                                                    if (checkVCKCuoc)
                                                    {
                                                        bool checkVCKCuocSL = DAO.VCKDAO.Instance.CheckSL(maDl.ToString().Trim(), cellValueMaHH.ToString().Trim(), Convert.ToInt32(cellValueSL.ToString().Trim()));
                                                        if (!checkVCKCuoc)
                                                        {
                                                            checkSl = true;
                                                            NapCT();
                                                            MessageBox.Show("Số lượng vỏ chai két của đại lý cược là không đủ. Yêu cầu đặt thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                            break;
                                                        }
                                                        //if (loaiPK.MaLPK.Trim() == "LPK0001")
                                                        //{
                                                        //    DTO.Vckcuoc vckcuoc = DAO.VCKDAO.Instance.Get(maDl.Trim().ToUpper(), cellValueMaHH.ToString().Trim().ToUpper());
                                                        //    //slgiu + Convert.ToInt32(cellValueSL.ToString().Trim())
                                                        //    if (Convert.ToInt32(vckcuoc.SlGiu) + Convert.ToInt32(cellValueSL.ToString().Trim()) > Convert.ToInt32(vckcuoc.SlCuoc.ToString()))
                                                        //    {
                                                        //        checkTon = true;
                                                        //        NapCT();
                                                        //        MessageBox.Show("Hiện tại đại lý chưa đặt cược vỏ chai két có mã hàng hoá là:" + cellValueMaHH.ToString().Trim(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        //        break;
                                                        //    }
                                                        //}
                                                    }
                                                    
                                                }
                                                
                                            }
                                            else
                                            {
                                                string text = "Hiện hàng " + cellValueMaHH.ToString().Trim() + " tạm hết hàng!";
                                                MessageBox.Show(text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                break;
                                            }
                                        }
                                    }
                                    if (checkTon == false && checkSl == false)
                                    {


                                        // Insert PHIEUKHO (NGAYTAO, NGUOITAO, NOIDUNG, PTVC, BIENSO, MA_LPK, MA_DH)
                                        string maPhieuKho = DAO.PhieukhoDAO.Instance.Insert(dtNgaytao.DateTime.ToString("MM/dd/yyyy HH:mm:ss"), userName.ToString().Trim(), txtNoidung.Text.ToString().Trim(), txtPTVC.Text.ToString().Trim(), txtBienso.Text.ToString().Trim().ToUpper(), loaiPK.MaLPK.Trim(), btnMadh.Text.Trim());
                                        if (maPhieuKho != null)
                                        {
                                            //MessageBox.Show("Đã insert được Phiếu kho");
                                            for (int i = 0; i < gridView2.RowCount; i++)
                                            {
                                                object cellValueMaHH = gridView2.GetRowCellValue(i, "MAHH");
                                                object cellValueSL = gridView2.GetRowCellValue(i, "SL");
                                                int k = 0;
                                                if (cellValueMaHH != null && cellValueSL != null)
                                                {
                                                    k = i;
                                                    bool checkInsertCTPK = DAO.CTPhieuKhoDAO.Instance.Insert(maPhieuKho.Trim(), cellValueMaHH.ToString().Trim(), Convert.ToInt32(cellValueSL.ToString().Trim()));
                                                    if (checkInsertCTPK)
                                                    {
                                                        //MessageBox.Show("Đã insert CTPK");
                                                        int sl_hh = DAO.HanghoaDAO.Instance.GetSL(cellValueMaHH.ToString().Trim());
                                                        if (loaiPK.LoaiLPK.Trim() == "Nhập")
                                                        {
                                                            int sl_new = sl_hh + Convert.ToInt32(cellValueSL.ToString().Trim());
                                                            DAO.HanghoaDAO.Instance.UpdateSL(cellValueMaHH.ToString().Trim(), sl_new);
                                                            if (loaiPK.MaLPK.Trim() == "LPK0004")
                                                            {
                                                                bool checkVo = DAO.HanghoaDAO.Instance.GetByDataOtherByBark(cellValueMaHH.ToString().Trim());
                                                                if (checkVo)
                                                                {
                                                                    int slgiu = DAO.VCKDAO.Instance.GetSLGiu(maDl.Trim(), cellValueMaHH.ToString().Trim());
                                                                    int slgiu_new = slgiu - Convert.ToInt32(cellValueSL.ToString().Trim());
                                                                    DAO.VCKDAO.Instance.UpdateSLGiu(maDl.Trim(), cellValueMaHH.ToString().Trim(), slgiu_new);

                                                                }
                                                            }
                                                        }
                                                        else if (loaiPK.LoaiLPK.Trim() == "Xuất")
                                                        {
                                                            int sl_new = sl_hh - Convert.ToInt32(cellValueSL.ToString().Trim());
                                                            DAO.HanghoaDAO.Instance.UpdateSL(cellValueMaHH.ToString().Trim(), sl_new);
                                                            if (loaiPK.MaLPK.Trim() == "LPK0001")
                                                            {
                                                                bool checkVo = DAO.HanghoaDAO.Instance.GetByDataOtherByBark(cellValueMaHH.ToString().Trim());
                                                                if (checkVo)
                                                                {
                                                                    int slgiu = DAO.VCKDAO.Instance.GetSLGiu(maDl.Trim(), cellValueMaHH.ToString().Trim());
                                                                    int slgiu_new = slgiu + Convert.ToInt32(cellValueSL.ToString().Trim());
                                                                    DAO.VCKDAO.Instance.UpdateSLGiu(maDl.Trim(), cellValueMaHH.ToString().Trim(), slgiu_new);

                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Không xác định đc Loại Nhập/Xuất");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Thêm CTPK fail");
                                                    }
                                                }
                                                else if (cellValueMaHH == null && cellValueSL == null && i <= Convert.ToInt32(Convert.ToInt32(gridView2.RowCount) - 1) && k == 0)
                                                {
                                                    gridControl1_Load(sender, e);
                                                    //Câu update trạng thái đang k chạy ạ
                                                    DAO.DonhangDAO.Instance.UpdateTrangThai(maDH.ToString().Trim(), "Đã xuất kho");
                                                    MessageBox.Show("Đã cập nhật trạng thái đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                else { }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Tạo phiếu kho không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
 
                                        }
                                    }
                                }
                                else
                                {
                                    XtraMessageBox.Show("Bạn nhập thiếu biển số xe vận chuyển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //NapCT();
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show("Bạn nhập thiếu phương tiện vận chuyển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //NapCT();
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("Bạn nhập thiếu nội dung diễn giải", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //NapCT();
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Bạn nhập thiếu người tạo", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //NapCT();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Bạn nhập thiếu trường mã đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //NapCT();
                }
            }
            else
            {
                XtraMessageBox.Show("Bạn nhập thiếu trường loại phiếu kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //NapCT();
            }
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

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnMadh.Text = ucKho.maDH;
        }
    }
}
