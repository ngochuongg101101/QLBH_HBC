using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DTO
{
    class CTDonhang
    {
        public CTDonhang(string madh,string mahh,int sl,double dongia,double thanhtien)
        {
            this.MaDH = madh;
            this.MaHH = mahh;
            this.Sl = sl;
            this.Dongia = dongia;
            this.Thanhtien = thanhtien;
        }
        public CTDonhang(DataRow Row)
        {
            this.MaDH = Row["MA_DH"].ToString();
            this.MaHH = Row["MA_HH"].ToString();
            this.Sl = (int)Convert.ToDouble(Row["SL"].ToString());
            this.Dongia = Convert.ToDouble(Row["DONGIA"].ToString());
            this.Thanhtien = Convert.ToDouble(Row["THANHTIEN"].ToString());


        }
        public string MaDH { get; }
        public string MaHH { get; }
        public int Sl { get; private set; }
        public double Dongia { get; private set; }
        public double Thanhtien { get; private set; }
    }
}
