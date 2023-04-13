using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.Mask;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;

namespace QLBH_HBC.UI
{
    public partial class uc_Daily : DevExpress.XtraEditors.XtraUserControl
    {
        private bool flat_btnAdd = false;
        private bool flat_btnEdit = false;
        private bool flat_btnDel = false;
        private DataTable dt1;
        private string userName;
        public uc_Daily(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }

        private void uc_Daily_Load(object sender, EventArgs e)
        {
            reload_gridview2();

        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {


            // Make sure the GridView has at least one row
            if (gridView2.RowCount > 0)
            {
                int rowHandle = gridView2.FocusedRowHandle;
                //Object row = gridView2.DataSource.[rowHandle];
                DTO.Daily row = null;
                //DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                for (int i = 0; i < gridView2.RowCount; i++)
                { 
                    if(i == rowHandle)
                    {
                        int row_ds = this.gridView2.ViewRowHandleToDataSourceIndex(i);
                        row = (DTO.Daily)gridView2.GetRow(i);
                        break;
                    }
                }
                if(row != null)
                {
                    txtMADL.Text = row.MaDL;
                    txtTENDL.Text = row.TenDL;
                    txtDIACHI.Text = row.DiaChi;
                    txtEMAIL.Text = row.Email;
                    txtMAST.Text = row.MST;
                    txtSDT.Text = row.Sdt; 
                }
            
            }



        }
        private void reload_gridview2()
        {
            gridControl2.DataSource = DAO.DailyDAO.Instance.GetAll();
            //Click để thêm dòng
            gridView2.Columns[0].Caption = "Mã Đại lý";
            gridView2.Columns[1].Caption = "Tên Đại lý";
            gridView2.Columns[2].Caption = "Địa chỉ";
            gridView2.Columns[3].Caption = "Số điện thoại";
            gridView2.Columns[4].Caption = "Email";
            gridView2.Columns[5].Caption = "Mã số thuế";
            gridControl2.Visible = false;
            txtMADL.Enabled = false;
            txtTENDL.Enabled = false;
            txtDIACHI.Enabled = false;
            txtEMAIL.Enabled = false;
            txtMAST.Enabled = false;
            txtSDT.Enabled = false;
        }
        private void gridControl2_Load(object sender, EventArgs e)
        {
            // Set the DataTable as the DataSource of the grid control
            reload_gridview2();
        }
        //Them
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMADL.Text = "";
            txtTENDL.Text = "";
            txtDIACHI.Text ="";
            txtEMAIL.Text = "";
            txtMAST.Text = "";
            txtSDT.Text = "";
            flat_btnAdd = true;
            txtMADL.Enabled = false;
            txtTENDL.Enabled = true;
            txtDIACHI.Enabled = true;
            txtEMAIL.Enabled = true;
            txtMAST.Enabled = true;
            txtSDT.Enabled = true;
        }
        //Sua
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            flat_btnEdit = true;
            if (txtMADL.Text.Trim().Length == 0 && txtTENDL.Text.Trim().Length == 0 && txtDIACHI.Text.Trim().Length == 0 && txtEMAIL.Text.Trim().Length == 0 && txtMAST.Text.Trim().Length == 0 && txtSDT.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("Yêu cầu chọn thông tin cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMADL.Enabled = false;
                txtTENDL.Enabled = false;
                txtDIACHI.Enabled = false;
                txtEMAIL.Enabled = false;
                txtMAST.Enabled = false;
                txtSDT.Enabled = false;
                DTO.Daily row = null;
                //DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                        int row_ds = this.gridView2.ViewRowHandleToDataSourceIndex(0);
                        row = (DTO.Daily)gridView2.GetRow(0);
                        break;
                    
                }
                if (row != null)
                {
                    txtMADL.Text = row.MaDL;
                    txtTENDL.Text = row.TenDL;
                    txtDIACHI.Text = row.DiaChi;
                    txtEMAIL.Text = row.Email;
                    txtMAST.Text = row.MST;
                    txtSDT.Text = row.Sdt;
                }
            }
            else
            {
                txtTENDL.Enabled = true;
                txtDIACHI.Enabled = true;
                txtEMAIL.Enabled = true;
                txtMAST.Enabled = true;
                txtSDT.Enabled = true;
            }
        }
        //Xoa
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            flat_btnDel = true;
            if (txtMADL.Text.Length > 0 && txtTENDL.Text.Length > 0 && txtDIACHI.Text.Length > 0 && txtEMAIL.Text.Length > 0 && txtMAST.Text.Length > 0 && txtSDT.Text.Length > 0)
            {
                XtraMessageBox.Show("Yêu cầu chọn thông tin cần xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Luu
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (flat_btnAdd)
            {
                bool check = false;
                if(check == false)
                {
                    if (txtTENDL.Text.Trim().Length == 0)
                    {
                        XtraMessageBox.Show("Bạn chưa nhập tên đại lý", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        check = true;

                    }
                }
                if (check == false)
                {
                    if (txtDIACHI.Text.Trim().Length == 0)
                    {
                       XtraMessageBox.Show("Bạn chưa nhập địa chỉ đại lý", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        check = true;
                    }

                }
                if (check == false)
                {
                    if (txtEMAIL.Text.Trim().Length == 0)
                    {
                       XtraMessageBox.Show("Bạn chưa nhập email đại lý", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        check = true;
                    }

                }
                if (check == false)
                {
                    if (txtMAST.Text.Trim().Length == 0)
                    {
                        XtraMessageBox.Show("Bạn chưa nhập mã số thuế đại lý", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        check = true;
                    }

                }
                if (check == false)
                {
                    if (txtSDT.Text.Trim().Length == 0)
                    {
                       XtraMessageBox.Show("Bạn chưa nhập số điện thoại đại lý", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        check = true;
                    }

                }
                if (txtTENDL.Text.Length > 0 && txtDIACHI.Text.Length > 0 && txtEMAIL.Text.Length > 0 && txtMAST.Text.Length > 0 && txtSDT.Text.Length > 0)
                {
                    string result = DAO.DailyDAO.Instance.Insert(txtTENDL.Text.Trim(), txtDIACHI.Text.Trim(), txtSDT.Text.Trim(), txtEMAIL.Text.Trim(), txtMAST.Text.Trim());
                    if (result != null)
                    {
                        XtraMessageBox.Show("Thêm đại lý thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMADL.Text = "";
                        txtTENDL.Text = "";
                        txtDIACHI.Text = "";
                        txtEMAIL.Text = "";
                        txtMAST.Text = "";
                        txtSDT.Text = "";
                        txtMADL.Enabled = true;
                    }
                    else
                    {
                        XtraMessageBox.Show("Thêm đại lý không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    flat_btnAdd = false;
                }
                
            }
            if (flat_btnEdit)
            {
                bool result = DAO.DailyDAO.Instance.Update(txtMADL.Text.Trim(),txtTENDL.Text.Trim(), txtDIACHI.Text.Trim(), txtSDT.Text.Trim(), txtEMAIL.Text.Trim(), txtMAST.Text.Trim());
                if (result)
                {
                    XtraMessageBox.Show("Cập nhật đại lý thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Cập nhật đại lý không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                flat_btnEdit = false;
            }
            if (flat_btnDel)
            {

            }
            reload_gridview2();
        }
    }
}
