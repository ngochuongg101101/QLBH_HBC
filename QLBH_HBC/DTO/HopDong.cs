using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
        public HopDong(DataRow Row)
        {
            this.MaHD = Row["MAHD"].ToString();
            this.NgayTao = (DateTime)Row["NGAYTAO"];
            this.NguoiTao = Row["NGUOITAO"].ToString();
            this.NgayBD = (DateTime)Row["NGAYBD"];
            this.NgayKT = (DateTime)Row["NGAYKT"];
            this.CK = (int)Convert.ToDouble(Row["CK"].ToString());
            this.MaDL = Row["MA_DL"].ToString();

        }
        public string MaHD { get; private set; }
        public DateTime NgayTao { get; private set; }
        public string NguoiTao { get; private set; }
        public DateTime NgayBD { get; private set; }
        public DateTime NgayKT { get; private set; }
        public float CK { get; private set; }
        public string MaDL { get; private set; }
    }
}
