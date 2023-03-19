using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DTO
{
    class Hanghoa
    {
        public Hanghoa(string mahh,string tenhh,float dongia,float giacuoc,string dvt,int sl,string loai,Boolean co_vck)
        {
            this.MaHH = mahh;
            this.TenHH = tenhh;
            this.Dongia = dongia;
            this.Giacuoc = giacuoc;
            this.Dvt = dvt;
            this.Sl = sl;
            this.Loai = loai;
            this.Co_vck = co_vck;
        }

        public float Giacuoc { get; private set; }
        public string Dvt { get; private set; }
        public int Sl { get; private set; }
        public string Loai { get; private set; }
        public bool Co_vck { get; private set; }
        public string MaHH { get; private set; }
        public string TenHH { get; private set; }
        public float Dongia { get; private set; }
    }
}
