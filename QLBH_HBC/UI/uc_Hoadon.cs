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
    public partial class uc_Hoadon : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_Hoadon()
        {
            InitializeComponent();
        }
        //Insert -> Chọn loại HD -> Click vào kính lúp sẽ mở ra form frmDonhang (tương tự uc_Kho) 
        //    * Sửa lại như sao cho khi mở form từ:
        //                                       + uc_Kho -> lấy Đơn hàng trạng thái "Chờ xuất kho"
        //                                       + uc_Hoadon -> Lấy Đơn hàng trạng thái "Đã xuất kho"
        //    * Sau khi chọn được số đơn hàng từ frmDonhang hoặc nhập số đơn hàng rồi nhấn Enter
        //      -> Lấy lên gridView2 thông tin bảng CT_DONHANG where MA_DH = btnMadh.Text
        //    * Nhập thuế vào txtVAT -> tự động tính txtTongtien

    }
}
