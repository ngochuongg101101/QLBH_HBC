using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DTO
{
    class Donhang
    {
        public Donhang(string madh,DateTime ngaytao,string nguoitao,string trangthai,string madl,string ghichu,double tongtien)
        {
            this.MaDonHang = madh;
            this.NgayTaoDonHang = ngaytao;
            this.NguoiTaoDonHang = nguoitao;
            this.TrangthaiDonHang = trangthai;
            this.MaDL_DonHang = madl;
            this.Ghichu = ghichu;
            this.Tongtien = tongtien;
        }
        public Donhang(DataRow Row)
        {
            this.MaDonHang = Row["MADH"].ToString();
            this.NgayTaoDonHang = (DateTime)Row["NGAYTAO"];
            this.NguoiTaoDonHang = Row["NGUOITAO"].ToString();
            this.TrangthaiDonHang = Row["TRANGTHAI"].ToString();
            this.MaDL_DonHang = Row["MA_DL"].ToString();
            this.Ghichu = Row["GHICHU"].ToString();
            this.Tongtien = Convert.ToDouble(Row["TONGTIEN"].ToString());
        }
        public string MaDonHang { get; }
        public DateTime NgayTaoDonHang { get; private set; }
        public string NguoiTaoDonHang { get; private set; }
        public string TrangthaiDonHang { get; private set; }
        public string MaDL_DonHang { get; private set; }
        public string Ghichu { get; private set; }
        public double Tongtien { get; private set; }
    }
}
