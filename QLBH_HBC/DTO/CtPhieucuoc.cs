using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DTO
{
    class CtPhieucuoc
    {
        public CtPhieucuoc(string mapc,string mavo,int sl)
        {
            this.MaPC = mapc;
            this.MaVO = mavo;
            this.Sl = sl;
        }
        public CtPhieucuoc(DataRow Row)
        {
            this.MaPC = Row["MA_PC"].ToString();
            this.MaVO = Row["MA_VO"].ToString();
            this.Sl = (int)Convert.ToDouble(Row["SL"].ToString());

        }
        public string MaPC { get; private set; }
        public string MaVO { get; private set; }
        public int Sl { get; private set; }
    }
}
