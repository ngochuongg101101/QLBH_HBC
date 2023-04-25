using DevExpress.XtraEditors;
using QLBH_HBC.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH_HBC
{
    public partial class frmDonhang : DevExpress.XtraEditors.XtraForm
    {
        private uc_Kho _ucKho;
        private uc_Hoadon _ucHoadon;

        public frmDonhang(uc_Kho ucKho,uc_Hoadon ucHoadon)
        {
            _ucKho = ucKho;
            _ucHoadon = ucHoadon;

            InitializeComponent();
        }
        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT TRANGTHAI, MADH ,MA_DL, NGAYTAO , TENDL , NGUOITAO, TONGTIEN  FROM DONHANG " +
            "JOIN DAILY ON MADL = MA_DL";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.Refresh();
            //gridView1.OptionsBehavior.ReadOnly = true;
            //gridView1.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            // Lấy giá trị mã đơn hàng từ row được chọn
            string MaDH = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MADH").ToString();
            string MaDL = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA_DL").ToString();
            // Trả về giá trị maDH đã chọn và đóng form
            this.DialogResult = DialogResult.OK;
            if(_ucKho!=null)
            {
                _ucKho.maDH = MaDH; 
                _ucKho.maDl = MaDL;
            }
            //ucKho.maDH = maDH;
            if (_ucHoadon != null)
            {
                _ucHoadon.maDH = MaDH;
            }
            this.Close();
            //MessageBox.Show(ucKho.maDH);

            //đóng form lại và truyền madh về btnMadh trong uc_Kho
        }
    }
}