using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DTO
{
    class Vckcuoc
    {
        public Vckcuoc(string madl,string mavo,int slcuoc,int slgiu)
        {
            this.MaDL = madl;
            this.MaVO = mavo;
            this.SlCuoc = slcuoc;
            this.SlGiu = slgiu;
        }

        public string MaDL { get; }
        public string MaVO { get; private set; }
        public int SlCuoc { get; private set; }
        public int SlGiu { get; private set; }
    }
}
