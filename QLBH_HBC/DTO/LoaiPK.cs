using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DTO
{
    class LoaiPK
    {
        public LoaiPK(string malpk,string tenlpk,string loai)
        {
            this.MaLPK = malpk;
            this.TenLPK = tenlpk;
            this.Loai = loai;
        }

        public string MaLPK { get; }
        public string TenLPK { get; private set; }
        public string Loai { get; private set; }
    }
}
