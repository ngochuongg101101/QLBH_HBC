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
        public CTDonhang(string madh,string mahh,int sl)
        {
            this.MaDH = madh;
            this.MaHH = mahh;
            this.Sl = sl;
        }
        public CTDonhang(DataRow Row)
        {
            this.MaDH = Row["MA_DH"].ToString();
            this.MaHH = Row["MA_HH"].ToString();
            this.Sl = (int)Convert.ToDouble(Row["SL"].ToString());

        }
        public string MaDH { get; }
        public string MaHH { get; }
        public int Sl { get; private set; }
    }
}
