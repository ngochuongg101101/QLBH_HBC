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
    public partial class frmDonhangThanhToan : DevExpress.XtraEditors.XtraForm
    {
        private frmThanhtoancongno _frmThanhtoancongno;

        public frmDonhangThanhToan(frmThanhtoancongno frmThanhtoancongno)
        {
            _frmThanhtoancongno = frmThanhtoancongno;
            InitializeComponent();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT TRANGTHAI, MADH ,MA_DL, NGAYTAO , TENDL , NGUOITAO, TONGTIEN  FROM DONHANG " +
            "JOIN DAILY ON MADL = MA_DL WHERE MADL = '"+ _frmThanhtoancongno.maDL+ "'";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.Refresh();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            // Lấy giá trị mã đơn hàng từ row được chọn
            for (int i = 0; i < gridView1.RowCount; i++)
            { 
                string MaDH = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MADH").ToString();
                // Trả về giá trị maDH đã chọn và đóng form
                this.DialogResult = DialogResult.OK;
                if(MaDH.Trim().Length > 0)
                {
                    _frmThanhtoancongno.maDH = MaDH.Trim();
                }
                else
                {
                    break;
                }
            
            }
            //frmThanhtoancongno.maDl = MaDL;
            this.Close();
            //MessageBox.Show(ucKho.maDH);

            //đóng form lại và truyền madh về btnMadh trong uc_Kho
        }
    }
}