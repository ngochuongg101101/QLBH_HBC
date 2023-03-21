using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QLBH_HBC.DTO
{
    class Boom
    {
        public Boom(string mabia,string mavo,int sl)
        {
            this.MaBIA = mabia;
            this.MaVo = mavo;
            this.sl = sl;
        }
        public Boom(DataRow Row)
        {
            this.MaBIA = Row["MA_BIA"].ToString();
            this.MaVo = Row["MA_VO"].ToString();
            this.sl = (int)Convert.ToDouble(Row["SL"].ToString());

        }
        public string MaBIA { get; }
        public string MaVo { get; }

        private readonly int sl;
    }
}
