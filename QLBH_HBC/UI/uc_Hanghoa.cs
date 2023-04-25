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
        private string userName;
        public uc_Hanghoa(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM HANGHOA";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridView1.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[4].DisplayFormat.FormatString = "#,##0 VND";
            gridView1.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[5].DisplayFormat.FormatString = "#,##0 VND";
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
            if (addnewflag == true)
            {
                chkCoVCK.Checked = false;
            }
            else if (addnewflag == false)
            {
                chkCoVCK.Checked = Convert.ToBoolean(row["CO_VCK"].ToString());
            }
            string sql1 = "SELECT MAHH,TENHH,BOOM.SL,DVT FROM HANGHOA,BOOM WHERE MAHH = MA_VO AND MA_BIA = '" + txtMaHH.Text + "'";
            gridControl2.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql1);
            gridControl2.Refresh();
            //gridView2.OptionsBehavior.ReadOnly = true;
            gridView2.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (addnewflag == true)
            {
                DialogResult result = MessageBox.Show("Bạn có muốn tiếp tục tạo mới?", "Thông báo", MessageBoxButtons.YesNo);

                // Xử lý sự kiện khi người dùng bấm Có hoặc Không
                switch (result)
                {
                    case DialogResult.Yes:
                        
                        // Thêm các sự kiện khi người dùng bấm Có vào đây
                        break;
                    case DialogResult.No:
                        NapCT();
                        txtMaHH.Enabled = false;
                        txtTenHH.Enabled = false;
                        cbLoai.Enabled = false;
                        cbDVT.Enabled = false;
                        txtGiaban.Enabled = false;
                        txtGiacuoc.Enabled = false;
                        chkCoVCK.Enabled = false;
                        addnewflag = false;
                        // Thêm các sự kiện khi người dùng bấm Không vào đây
                        break;
                    default:
                        break;
                }
            }
            else
            {
                NapCT();
                txtMaHH.Enabled = false;
                txtTenHH.Enabled = false;
                cbLoai.Enabled = false;
                cbDVT.Enabled = false;
                txtGiaban.Enabled = false;
                txtGiacuoc.Enabled = false;
                chkCoVCK.Enabled = false;
            }
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
            addnewflag = true;

            txtTenHH.Enabled = true;
            cbLoai.Enabled = true;
            cbDVT.Enabled = true;
            txtGiaban.Enabled = true;
            txtGiacuoc.Enabled = true;
            chkCoVCK.Enabled = true;

            gridView1.AddNewRow();
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            NapCT();
            txtTenHH.Focus();
            btnSave.Enabled = true;

            for (int i = 1; i <= 5; i++)
            {
                gridView2.AddNewRow();
            }
            gridView2.OptionsBehavior.Editable = true;
            gridView2.OptionsBehavior.ReadOnly = false;
            gridView2.Appearance.Row.BackColor = Color.Empty;
            addnewflag = true;

            string sql2 = "Select distinct DVT from HANGHOA";
            DataTable dt = Config.DataProvider.Instance.ExecuteQuery(sql2);
            cbDVT.DataSource = dt;
            cbDVT.DisplayMember = "DVT";
            cbDVT.Text = "";
            cbDVT.ValueMember = "DVT";
            txtMaHH.Enabled = false;
        }

        private void gridView2_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                // Lấy thông tin ô đang được chọn
                int rowHandle = gridView2.FocusedRowHandle;
                string fieldName = gridView2.FocusedColumn.FieldName;
                //MessageBox.Show(fieldName);

                DataRow row = gridView2.GetDataRow(rowHandle);

                // Lấy mã hàng hóa được thay đổi
                string maHH = row["MAHH"].ToString();

                // Kiểm tra nếu cột MAHH đã được cập nhật
                if (fieldName == "MAHH" && !row.IsNull("MAHH"))
                 {
                    // Lấy thông tin hàng hóa từ cơ sở dữ liệu
                    string sql = "SELECT TENHH, DVT FROM HANGHOA WHERE MAHH = '" + maHH.Trim().ToUpper() + "' AND LOAI = N'Vỏ'";
                    DataTable dt = Config.DataProvider.Instance.ExecuteQuery(sql);

                    // Hiển thị thông tin hàng hóa lên gridView
                    if (dt.Rows.Count > 0)
                    {
                        gridView2.SetRowCellValue(rowHandle, "TENHH", dt.Rows[0]["TENHH"].ToString());
                        gridView2.SetRowCellValue(rowHandle, "DVT", dt.Rows[0]["DVT"].ToString());
                    }
                    else
                    {
                        XtraMessageBox.Show("Hàng hoá bạn nhập không phải là vỏ!","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }

            }
        }

        private void cbLoai_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbLoai.Text == "Bia")
            {
                gridControl2.Visible = true;
            }
            if (cbLoai.Text == "Vỏ")
            {
                gridControl2.Visible = false;
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(txtTenHH.Text.Length > 0) {
                if(cbLoai.Text.Length > 0)
                {
                    if(cbDVT.Text.Length > 0)
                    {
                        if(txtGiaban.Text.Length > 0)
                        {
                            if(txtGiacuoc.Text.Length > 0 && cbLoai.Text == "Vỏ")
                            {
                                string mahh = DAO.HanghoaDAO.Instance.InsertVo(txtTenHH.Text.Trim(), Convert.ToInt32(txtGiaban.Text.Trim()), Convert.ToInt32(txtGiacuoc.Text.Trim()), cbDVT.Text.Trim(), cbLoai.Text.Trim());
                                if(mahh != null)
                                {
                                    XtraMessageBox.Show("Thêm vỏ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            //Them Bia
                            else
                            {
                                bool checkVoHangHoa = false;
                                // Kiem tra hang hoa table la vo
                                if (chkCoVCK.Checked)
                                {
                                    for (int i = 0; i < gridView2.RowCount; i++)
                                    {

                                        object cellValueMaHH = gridView2.GetRowCellValue(i, "MAHH");
                                        object cellValueTenHH = gridView2.GetRowCellValue(i, "TENHH");
                                        object cellValueSL = gridView2.GetRowCellValue(i, "SL");
                                        object cellValueDVT = gridView2.GetRowCellValue(i, "DVT");
                                        if (cellValueMaHH == null && cellValueTenHH == null && cellValueSL == null && cellValueDVT == null)
                                        {
                                            XtraMessageBox.Show("Bạn chưa nhập vỏ chat két", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            break;
                                        }
                                        else
                                        {
                                            if (cellValueMaHH.ToString().Trim().Length > 0 && cellValueTenHH.ToString().Trim().Length > 0 && cellValueSL.ToString().Trim().Length > 0 && cellValueDVT.ToString().Trim().Length > 0 && Convert.ToInt32(cellValueSL) > 0)
                                            {
                                                bool checkVo = DAO.HanghoaDAO.Instance.GetByDataOtherByBark(cellValueMaHH.ToString().Trim());
                                                if (!checkVo)
                                                {
                                                    checkVoHangHoa = true;
                                                    break;
                                                }

                                            }
                                        }

                                    }
                                }
                                
                                // Them hang hoa
                                if (chkCoVCK.Checked && checkVoHangHoa == false)
                                {
                                    string mahh = DAO.HanghoaDAO.Instance.InsertBia(txtTenHH.Text.Trim(),Convert.ToInt32(txtGiaban.Text.Trim()), cbDVT.Text.Trim(), cbLoai.Text.Trim(),chkCoVCK.Checked);
                                    for (int i = 0; i < gridView2.RowCount; i++)
                                    {

                                        object cellValueMaHH = gridView2.GetRowCellValue(i, "MAHH");
                                        object cellValueTenHH = gridView2.GetRowCellValue(i, "TENHH");
                                        object cellValueSL = gridView2.GetRowCellValue(i, "SL");
                                        object cellValueDVT = gridView2.GetRowCellValue(i, "DVT");
                                        if(cellValueMaHH.ToString().Trim().Length > 0 && cellValueTenHH.ToString().Trim().Length > 0 && cellValueSL.ToString().Trim().Length > 0 && cellValueDVT.ToString().Trim().Length > 0 && Convert.ToInt32(cellValueSL) >0)
                                        {
                                            bool result = DAO.BoomDAO.Instance.Insert(mahh.Trim(), cellValueMaHH.ToString().Trim(), Convert.ToInt32(cellValueSL));
                                        }

                                    }
                                }
                                else
                                {
                                    string mahh = DAO.HanghoaDAO.Instance.InsertBia(txtTenHH.Text.Trim(), Convert.ToInt32(txtGiaban.Text.Trim()), cbDVT.Text.Trim(), cbLoai.Text.Trim(), chkCoVCK.Checked);
                                    if(mahh != null)
                                    {
                                        XtraMessageBox.Show("Thêm hàng hoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                }

                            }

                        }
                        else
                        {
                            MessageBox.Show("Bạn chưa nhập giá bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtGiaban.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa nhập đơn vị tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cbDVT.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbLoai.Focus();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập tên hàng hóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenHH.Focus();
            }
            string sql = "SELECT * FROM HANGHOA";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridView1.RowClick += gridView1_RowClick;
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;

        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void cbLoai_TextChanged(object sender, EventArgs e)
        {
            if(cbLoai.Text.Length > 0)
            {
                if(cbLoai.Text.Trim()  == "Bia")
                {
                    txtGiacuoc.Enabled = false;
                    chkCoVCK.Enabled = true;

                }
                else
                {
                    txtGiacuoc.Enabled=true;
                    chkCoVCK.Enabled=false;
                    gridView2.OptionsBehavior.Editable = true;
                    gridView2.OptionsBehavior.ReadOnly = false;
                }
            }
        }

        private void chkCoVCK_CheckedChanged(object sender, EventArgs e)
        {
            if(chkCoVCK.Checked)
            {
                gridView2.OptionsBehavior.Editable = true;
                gridView2.OptionsBehavior.ReadOnly = false;
            }
            else
            {
                gridView2.OptionsBehavior.Editable = false;
                gridView2.OptionsBehavior.ReadOnly = true;
            }
        }

        private void txtGiaban_TextChanged(object sender, EventArgs e)
        {
            // Handle the TextChanged event
            string newText = txtGiaban.Text;
            if (!string.IsNullOrEmpty(newText))
            {
                // Convert the entered text to a decimal value
                decimal value = decimal.Parse(newText);
                // Format the decimal value as currency with the VND symbol
                string formattedValue = value.ToString("#,##0 ");
                // Display the formatted value in the TextEdit control
                txtGiaban.Text = formattedValue;
            }
        }

        private void txtGiacuoc_TextChanged(object sender, EventArgs e)
        {
            // Handle the TextChanged event
            string newText = txtGiacuoc.Text;
            if (!string.IsNullOrEmpty(newText))
            {
                // Convert the entered text to a decimal value
                decimal value = decimal.Parse(newText);
                // Format the decimal value as currency with the VND symbol
                string formattedValue = value.ToString("#,##0 ");
                // Display the formatted value in the TextEdit control
                txtGiacuoc.Text = formattedValue;
            }
        }

        private void txtGiaban_Enter(object sender, EventArgs e)
        {
            // Handle the TextChanged event
            string newText = txtGiacuoc.Text;
            if (!string.IsNullOrEmpty(newText))
            {
                // Convert the entered text to a decimal value
                decimal value = decimal.Parse(newText);
                // Format the decimal value as currency with the VND symbol
                string formattedValue = value.ToString("#,##0 ");
                // Display the formatted value in the TextEdit control
                txtGiacuoc.Text = formattedValue;
            }
        }

        private void txtGiacuoc_Enter(object sender, EventArgs e)
        {
            // Handle the TextChanged event
            string newText = txtGiacuoc.Text;
            if (!string.IsNullOrEmpty(newText))
            {
                // Convert the entered text to a decimal value
                decimal value = decimal.Parse(newText);
                // Format the decimal value as currency with the VND symbol
                string formattedValue = value.ToString("#,##0 ");
                // Display the formatted value in the TextEdit control
                txtGiacuoc.Text = formattedValue;
            }
        }
    }
}
