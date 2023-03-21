using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DTO
{
    class LoaiHD
    {
        public LoaiHD(string malhd,string tenlhd)
        {
            this.MaLHD = malhd;
            this.TenLHD = tenlhd;
        }

        public string MaLHD { get; }
        public string TenLHD { get; private set; }
    }
}
