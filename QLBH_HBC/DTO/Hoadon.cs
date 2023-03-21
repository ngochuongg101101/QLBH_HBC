using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DTO
{
    class Hoadon
    {
        public Hoadon(string mahd,DateTime ngaytao,string nguoitao,int vat,bool thanhtoan,float tongtien,string madh,string malhd)
        {
            this.MaHD = mahd;
            this.NgayTaoHoaDon = ngaytao;
            this.NguoiTaoHoaDon = nguoitao;
            this.VatHoaDon = vat;
            this.ThanhToan = thanhtoan;
            this.TongTienHoaDon = tongtien;
            this.MaDH_Hoadon = madh;
            this.MaLHD_Hoadon = malhd;
        }

        private string NguoiTaoHoaDon { get; }
        public int VatHoaDon { get; private set; }
        public bool ThanhToan { get; private set; }
        public float TongTienHoaDon { get; private set; }
        public string MaDH_Hoadon { get; private set; }
        public string MaLHD_Hoadon { get; private set; }
        public string MaHD { get; }
        public DateTime NgayTaoHoaDon { get; private set; }
    }
}
