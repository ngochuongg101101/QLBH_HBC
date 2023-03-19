using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH_HBC
{
    public partial class frmNhapcuoc : DevExpress.XtraEditors.XtraForm
    {
        public frmNhapcuoc()
        {
            InitializeComponent();
        }

        private void gridControl_Click(object sender, EventArgs e)
        {

        }

        private void gridControl_Load(object sender, EventArgs e)
        {
            //gridView.SetRowCellValue(GridControl.NewItemRowHandle, gridView.Columns["STT"], "Please enter new value");
            gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            //gridView.AddNewRow();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //gridView.AddNewRow();

            //gridView.SetRowCellValue(GridControl.NewItemRowHandle, gridView.Columns["STT"], "Please enter new value");

            //gridView.OptionsNavigation.AutoFocusNewRow = true;
        }

        private void frmNhapcuoc_Load(object sender, EventArgs e)
        {
            dtNgaytao.EditValue = DateTime.Today;

            string sql = "Select MADL,TENDL from DAILY";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            cbDaily.DataSource = dt;
            dt.Columns.Add("FULL",typeof(string),"MADL + ' - ' + TENDL");
            cbDaily.DisplayMember = "FULL";
            cbDaily.Text = "";
            cbDaily.ValueMember = "MADL";
        }
    }
}
