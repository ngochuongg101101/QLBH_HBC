using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DTO
{
    class Phieucuoc
    {
        public Phieucuoc(string mapc,DateTime ngaytao,string nguoitao,string loai,string madl)
        {
            this.MaPC = mapc;
            this.NgayTaoPC = ngaytao;
            this.Nguoitao = nguoitao;
            this.Loai = loai;
            this.MaDL = madl;
        }

        public string MaPC { get; }
        public DateTime NgayTaoPC { get; private set; }
        public string Nguoitao { get; private set; }
        public string Loai { get; private set; }
        public string MaDL { get; private set; }
    }
}
