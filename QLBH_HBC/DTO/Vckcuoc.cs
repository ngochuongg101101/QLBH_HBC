using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
        public Vckcuoc(DataRow Row)
        {
            this.MaDL = Row["MA_DL"].ToString();
            this.MaVO = Row["MA_VO"].ToString();
            this.SlCuoc = Convert.ToInt32(Row["SL_CUOC"].ToString());
            this.SlGiu = Convert.ToInt32(Row["SL_GIU"].ToString());

        }
        public string MaDL { get; }
        public string MaVO { get; private set; }
        public int SlCuoc { get; private set; }
        public int SlGiu { get; private set; }
    }
}
