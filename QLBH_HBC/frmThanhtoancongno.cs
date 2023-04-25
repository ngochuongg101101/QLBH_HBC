using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH_HBC
{
    public partial class frmThanhtoancongno : DevExpress.XtraEditors.XtraForm
    {
        private string username;
        private string maHD;
        private frmThanhtoancongno _frmThanhtoancongno;
        public string maDH { get; set; }
        public string maDL { get; set; }
        public frmThanhtoancongno(string userName, frmThanhtoancongno frmThanhtoancongno)
        {
            InitializeComponent();
            this.username = userName;
            this._frmThanhtoancongno = frmThanhtoancongno;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmThanhtoancongno_Load(object sender, EventArgs e)
        {
            cbDaily.DataSource = DAO.DailyDAO.Instance.GetAll();
            cbDaily.DisplayMember = "TENDL";
            cbDaily.ValueMember = "MADL";
            btnMadh.Text = "";
            txtUsername.Text = username;
            txtUsername.Enabled = false;
            dtNgaytao.EditValue = DateTime.Today;
        }

        private void btnMadh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (maDL.Trim().Substring(0, 2) == "DL")
            {
                btnPay.Enabled = true;
                frmDonhangThanhToan f = new frmDonhangThanhToan(this);
                f.ShowDialog();
                btnMadh.Text = maDH;
                //txtTongSoDuNo.Text
                if (maDH.Trim().Length > 0)
                {
                    DTO.Hoadon hoadon = DAO.HoaDonDAO.Instance.Get(btnMadh.Text.Trim());
                    if (hoadon != null)
                    {
                        if (hoadon.TongTienHoaDon > 0)
                        {
                            //txtTongSoDuNo.Text = hoadon.TongTienHoaDon.ToString();
                            List<DTO.Phieuthuchi> list = DAO.PhieuThuChiDAO.Instance.GetByMaHD(hoadon.MaHD.Trim());
                            maHD = hoadon.MaHD.Trim();
                            double tongtien_ptc = 0;
                            if (list.Count > 0)
                            {
                                foreach (DTO.Phieuthuchi phieuthuchi in list)
                                {
                                    if (phieuthuchi != null)
                                    {
                                        if (phieuthuchi.TongTienPTC >= 0)
                                        {
                                            tongtien_ptc = tongtien_ptc + (double)phieuthuchi.TongTienPTC;
                                        }
                                    }
                                }
                                if (hoadon.TongTienHoaDon - tongtien_ptc > 0)
                                {
                                    txtTongSoDuNo.Text = (hoadon.TongTienHoaDon - tongtien_ptc).ToString();
                                    //txtTongSoDuNo.DisplayFormat.FormatString = "#,##0 VND";
                                }
                                else
                                {
                                    if(tongtien_ptc - hoadon.TongTienHoaDon == 0)
                                    {
                                        XtraMessageBox.Show("Đại lý đã thanh toán hết khoản nợ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtTongSoDuNo.Text = (tongtien_ptc - hoadon.TongTienHoaDon).ToString("C", new CultureInfo("vi-VN"));
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("Đại lý còn nợ số tiền là:" + (tongtien_ptc - hoadon.TongTienHoaDon).ToString("C", new CultureInfo("vi-VN")), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtTongSoDuNo.Text = "-" + (tongtien_ptc - hoadon.TongTienHoaDon).ToString("C", new CultureInfo("vi-VN"));

                                    }
                                    txtSoTienThanhToan.Enabled = false;
                                    btnPay.Enabled = false;
                                }
                            }
                            else
                            {
                                btnMadh.Text = "";
                            }
                        }
                        if (hoadon.TongTienHoaDon == 0)
                        {
                            XtraMessageBox.Show("Yêu cầu bạn chọn đơn hàng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Bạn chọn lại đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbDaily_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbDaily.SelectedValue.ToString().Trim() != "QLBH_HBC.DTO.Daily")
            {
                if (cbDaily.SelectedValue.ToString().Trim().Substring(0, 2) == "DL")
                {
                    maDL = cbDaily.SelectedValue.ToString().Trim();

                }
                else
                {
                    XtraMessageBox.Show("Bạn chọn lại đại lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim().Length > 0)
            {
                if (txtTongSoDuNo.Text.Trim().Length > 0)
                {
                    if (txtSoTienThanhToan.Text.Trim().Length > 0)
                    {
                        if (txtPTTT.Text.Trim().Length > 0)
                        {
                            string dateMin = DAO.PhieuThuChiDAO.Instance.GetMinNgayTaoByMaHD(maHD);
                            DateTime minDate = DateTime.Parse(dateMin);
                            if (dtNgaytao.DateTime > minDate)
                            {
                                bool result = DAO.PhieuThuChiDAO.Instance.InsertThanhToan(dtNgaytao.DateTime.ToString("MM/dd/yyyy HH:mm:ss"), username, txtPTTT.Text.Trim(), Convert.ToInt32(txtSoTienThanhToan.Text), "MVV0001", maHD);
                                if (result == true)
                                {
                                    XtraMessageBox.Show("Thanh toán phiếu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    XtraMessageBox.Show("Thanh toán phiếu không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                dtNgaytao.EditValue = DateTime.Today;
                                bool result = DAO.PhieuThuChiDAO.Instance.InsertThanhToan(dtNgaytao.DateTime.ToString("MM/dd/yyyy HH:mm:ss"), username, txtPTTT.Text.Trim(), Convert.ToInt32(txtSoTienThanhToan.Text), "MVV0001", maHD);
                                if (result == true)
                                {
                                    XtraMessageBox.Show("Thanh toán phiếu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    XtraMessageBox.Show("Thanh toán phiếu không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                        }
                        else
                        {
                            XtraMessageBox.Show("Yêu cầu phương thức thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Yêu cầu nhập số tiền thanh toán công nợ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Yêu cầu kiểm tra đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Yêu cầu kiểm tra lại tên người tạo hoá đơn thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}