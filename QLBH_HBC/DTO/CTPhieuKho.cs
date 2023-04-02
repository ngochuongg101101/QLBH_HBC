using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DTO
{
    class CTPhieuKho
    {
        public CTPhieuKho(string mapk, string mahh, int sl)
        {
            this.MaPK = mapk;
            this.MaHH = mahh;
            this.Sl = sl;
        }
        public CTPhieuKho(DataRow Row)
        {
            this.MaPK = Row["MA_PK"].ToString();
            this.MaHH = Row["MA_HH"].ToString();
            this.Sl = (int)Convert.ToDouble(Row["SL"].ToString());
            


        }
        public string MaPK { get; }
        public string MaHH { get; }
        public int Sl { get; private set; }
    }
}
