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

namespace QLBH_HBC.UI
{
    public partial class uc_Daily : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable dt1;
        private string userName;
        public uc_Daily(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            GridView gridView1 = new GridView(gridControl1);
            gridControl1.MainView = gridView1;
            // Set the DataTable as the DataSource of the grid control
            gridControl1.DataSource = DAO.DailyDAO.Instance.GetAll();

            //Click để thêm dòng
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            //if (dataRow != null)
            //{
            //    txtMADL.Text = dataRow[0].ToString();
            //    txtTENDL.Text = dataRow[0].ToString();
            //    txtMAST.Text = dataRow[0].ToString();
            //    txtDIACHI.Text = dataRow[0].ToString();
            //    txtEMAIL.Text = dataRow[0].ToString();
            //}
            //MessageBox.Show("teee");
            //string dailyName = gridView1.GetRowCellValue(e.FocusedRowHandle, "MADL").ToString();
            //MessageBox.Show("You have selected the Daily with name: " + dailyName);

            //string dailyName1 = gridView1.GetRowCellValue(e.FocusedRowHandle, "TENDL").ToString();
            //MessageBox.Show("You have selected the Daily with name: " + dailyName1);
            if (!gridView1.IsMultiSelect)
                Test();
        }

        private void Test()
        {
            int[] rows = gridView1.GetSelectedRows();
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            Test();
        }
    }
}
