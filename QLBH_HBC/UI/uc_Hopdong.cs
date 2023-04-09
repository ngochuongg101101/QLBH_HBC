using DevExpress.XtraEditors;
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
    public partial class uc_Hopdong : DevExpress.XtraEditors.XtraUserControl
    {
        private string userName;
        private bool flat_btnAdd = false;
        private bool flat_btnEdit = false;
        private bool flat_btnDel = false;
        public uc_Hopdong(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }
        private void reload_gridview1()
        {
            gridControl1.DataSource = DAO.HopdongDAO.Instance.GetAllCustom();
            cbDaily.DataSource = DAO.DailyDAO.Instance.GetAll();
            cbDaily.DisplayMember = "TENDL";
            cbDaily.ValueMember = "MADL";
            //Click để thêm dòng
            gridView1.Columns[0].Caption = "Mã hợp đồng";
            gridView1.Columns[1].Caption = "Ngày tạo";
            gridView1.Columns[2].Caption = "Người tạo";
            gridView1.Columns[3].Caption = "Ngày hiệu lực";
            gridView1.Columns[4].Caption = "Ngày hết hiệu lực";
            gridView1.Columns[5].Caption = "Chiết khấu(%)";
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[7].Caption = "Tên đại lý";
            gridControl1.Visible = false;
            txtNguoitao.Text = userName.Trim();
            txtNguoitao.Enabled = false;
            txtMaHD.Enabled = false;
            dtNgayBD.Enabled = false;
            dtNgayKT.Enabled = false;
            dtNgaytao.Enabled = false;
            cbDaily.Enabled = false;
            txtCK.Enabled = false;
        }
        private void gridControl1_Load(object sender, EventArgs e)
        {
            reload_gridview1();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Make sure the GridView has at least one row
            if (gridView1.RowCount > 0)
            {
                int rowHandle = gridView1.FocusedRowHandle;
                DTO.HopdongDaily row = null;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (i == rowHandle)
                    {
                        int row_ds = this.gridView1.ViewRowHandleToDataSourceIndex(i);
                        row = (DTO.HopdongDaily)gridView1.GetRow(i);
                        break;
                    }
                }
                if (row != null)
                {
                    txtMaHD.Text = row.MaHD;
                    dtNgayBD.Text = row.NgayBD.ToString();
                    dtNgayKT.Text = row.NgayKT.ToString();
                    dtNgaytao.Text = row.NgayTao.ToString();
                    cbDaily.Text = row.TenDL.ToString();
                    txtCK.Text = row.CK.ToString();
                }

            }
        }
        // Them
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            flat_btnAdd = true;
            txtMaHD.Text = "";
            dtNgaytao.EditValue = DateTime.Today;
            dtNgayKT.EditValue = DateTime.Today;
            dtNgayBD.EditValue = DateTime.Today;
            cbDaily.Text = "";
            txtCK.Text = "";
            dtNgayBD.Enabled = true;
            dtNgayKT.Enabled = true;
            dtNgaytao.Enabled = true;
            cbDaily.Enabled = true;
            txtCK.Enabled = true;
        }

        //Sua
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            flat_btnEdit = true;
            if (txtMaHD.Text.Trim().Length == 0 && cbDaily.Text.Trim().Length == 0 && txtCK.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("Yêu cầu chọn thông tin cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaHD.Enabled = false;
                dtNgayBD.Enabled = false;
                dtNgayKT.Enabled = false;
                dtNgaytao.Enabled = false;
                cbDaily.Enabled = false;
                txtCK.Enabled = false;
                DTO.HopdongDaily row = null;
                //DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    int row_ds = this.gridView1.ViewRowHandleToDataSourceIndex(0);
                    row = (DTO.HopdongDaily)gridView1.GetRow(0);
                    break;

                }
                if (row != null)
                {
                    txtMaHD.Text = row.MaHD;
                    dtNgayBD.Text = row.NgayBD.ToString();
                    dtNgayKT.Text = row.NgayKT.ToString();
                    dtNgaytao.Text = row.NgayTao.ToString();
                    cbDaily.Text = row.TenDL.ToString();
                    txtCK.Text = row.CK.ToString();
                }
            }
            else if(txtMaHD.Text.Trim().Length > 0 && dtNgayBD.Text.Trim().Length > 0 && dtNgayKT.Text.Trim().Length > 0 && dtNgaytao.Text.Trim().Length > 0 && cbDaily.Text.Trim().Length > 0 && txtCK.Text.Trim().Length > 0)
            {
                dtNgayBD.Enabled = true;
                dtNgayKT.Enabled = true;
                dtNgaytao.Enabled = true;
                cbDaily.Enabled = true;
                txtCK.Enabled = true;
            }
        }
        //Xoa
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            flat_btnDel = true;
            if (txtMaHD.Text.Trim().Length == 0 && dtNgayBD.Text.Trim().Length == 0 && dtNgayKT.Text.Trim().Length == 0 && dtNgaytao.Text.Trim().Length == 0 && cbDaily.Text.Trim().Length == 0 && txtCK.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("Yêu cầu chọn thông tin cần xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaHD.Enabled = false;
                dtNgayBD.Enabled = false;
                dtNgayKT.Enabled = false;
                dtNgaytao.Enabled = false;
                cbDaily.Enabled = false;
                txtCK.Enabled = false;
                DTO.HopdongDaily row = null;
                //DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    int row_ds = this.gridView1.ViewRowHandleToDataSourceIndex(0);
                    row = (DTO.HopdongDaily)gridView1.GetRow(0);
                    break;

                }
                if (row != null)
                {
                    txtMaHD.Text = row.MaHD;
                    dtNgayBD.Text = row.NgayBD.ToString();
                    dtNgayKT.Text = row.NgayKT.ToString();
                    dtNgaytao.Text = row.NgayTao.ToString();
                    cbDaily.Text = row.TenDL.ToString();
                    txtCK.Text = row.CK.ToString();
                }
            }
            else
            {
                dtNgayBD.Enabled = true;
                dtNgayKT.Enabled = true;
                dtNgaytao.Enabled = true;
                cbDaily.Enabled = true;
                txtCK.Enabled = true;
            }
        }
        // Luu
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (flat_btnAdd)
            {
                bool check = false;
                if (check == false)
                {
                    if (string.IsNullOrWhiteSpace(cbDaily.Text))
                    {
                        XtraMessageBox.Show("Bạn chưa chọn tên đại lý", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        check = true;

                    }
                }
                if (check == false)
                {
                    if (dtNgayBD.Text.Trim().Length == 0)
                    {
                        XtraMessageBox.Show("Bạn chưa nhập ngày có hiệu lực", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        check = true;
                    }

                }
                if (check == false)
                {
                    if (dtNgayKT.Text.Trim().Length == 0)
                    {
                        XtraMessageBox.Show("Bạn chưa nhập ngày hết hiệu lực", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        check = true;
                    }

                }
                if (check == false)
                {
                    if (dtNgaytao.Text.Trim().Length == 0)
                    {
                        XtraMessageBox.Show("Bạn chưa nhập ngày tạo hợp đồng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        check = true;
                    }

                }
                if (check == false)
                {
                    if (txtCK.Text.Trim().Length == 0)
                    {
                        XtraMessageBox.Show("Bạn chưa nhập số chiết khấu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        check = true;
                    }

                }
                if (txtMaHD.Text.Length < 1 && cbDaily.Text.Length > 0 && dtNgayBD.Text.Length > 0 && dtNgayKT.Text.Length > 0 && dtNgaytao.Text.Length > 0 && txtCK.Text.Length > 0)
                {
                    string result = DAO.HopdongDAO.Instance.Insert(dtNgaytao.Text.Trim(),txtNguoitao.Text.Trim(), dtNgayBD.Text.Trim(), dtNgayKT.Text.Trim(), txtCK.Text.Trim(), cbDaily.SelectedValue.ToString().Trim());
                    if (result != null)
                    {
                        XtraMessageBox.Show("Thêm hợp đồng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbDaily.Text = "";
                        txtCK.Text = "";
                        dtNgaytao.Text = "";
                        dtNgayKT.Text = "";
                        dtNgayBD.Text = "";
                    }
                    else
                    {
                        XtraMessageBox.Show("Thêm hợp đồng không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    flat_btnAdd = false;
                }
                else
                {
                    XtraMessageBox.Show("Yêu cầu kiểm tra lại dữ liệu nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            if (flat_btnEdit)
            {
                bool result = DAO.HopdongDAO.Instance.Update(txtMaHD.Text.Trim(), dtNgaytao.Text.Trim(), dtNgayBD.Text.Trim(), dtNgayKT.Text.Trim(), txtCK.Text.Trim(), cbDaily.SelectedValue.ToString().Trim());
                if (result)
                {
                    XtraMessageBox.Show("Cập nhật hợp đồng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Cập nhật hợp đồng không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                flat_btnEdit = false;
            }
            if (flat_btnDel)
            {
                if(XtraMessageBox.Show("Bạn có chắc muốn xoá hợp đồng không ?","Cảnh báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    bool result = DAO.HopdongDAO.Instance.Delete(txtMaHD.Text.Trim());
                    if (result)
                    {
                        XtraMessageBox.Show("Xoá hợp đồng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("Xoá hợp đồng không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    flat_btnDel = false;
                }
            }
            reload_gridview1();
        }
    }
}
