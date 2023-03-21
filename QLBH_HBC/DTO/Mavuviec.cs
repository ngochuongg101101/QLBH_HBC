using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DTO
{
    class Mavuviec
    {
        public Mavuviec(string mamvv,string tenmvv,string loai)
        {
            this.MaMVV = mamvv;
            this.TenMVV = tenmvv;
            this.Loai = loai;
        }

        public string MaMVV { get; }
        public string TenMVV { get; private set; }
        public string Loai { get; private set; }
    }
}
