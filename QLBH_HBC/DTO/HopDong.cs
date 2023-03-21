using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DTO
{
    class HopDong
    {
        public HopDong(string mahd,DateTime ngaytao,string nguoitao,DateTime ngaybd,DateTime ngaykt,float ck,string madl)
        {
            this.MaHD = mahd;
            this.NgayTao = ngaytao;
            this.NguoiTao = nguoitao;
            this.NgayBD = ngaybd;
            this.NgayKT = ngaykt;
            this.CK = ck;
            this.MaDL = madl;
        }

        private string MaHD { get; }
        public DateTime NgayTao { get; private set; }
        public string NguoiTao { get; private set; }
        public DateTime NgayBD { get; private set; }
        public DateTime NgayKT { get; private set; }
        public float CK { get; private set; }
        public string MaDL { get; private set; }
    }
}
