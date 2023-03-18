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

namespace QLBH_HBC.UI
{
    public partial class uc_Cuocvo : UserControl
    {
        public uc_Cuocvo()
        {
            InitializeComponent();
        }

        private void barEditItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnNhapcuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Form f = new frmNhapcuoc();
            //f.Show();
            gridView1.AddNewRow();
            gridView1.OptionsNavigation.AutoFocusNewRow = true;
            //gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;

            // set a new row cell value. The static GridControl.NewItemRowHandle field allows you to retrieve the added row
            //gridView1.SetRowCellValue(GridControl.NewItemRowHandle, gridView1.Columns["Test"], "Please enter new value");
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
