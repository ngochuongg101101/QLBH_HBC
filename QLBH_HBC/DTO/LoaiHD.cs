using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DTO
{
    class LoaiHD
    {
        public LoaiHD(string malhd,string tenlhd)
        {
            this.MaLHD = malhd;
            this.TenLHD = tenlhd;
        }
        public LoaiHD(DataRow Row)
        {
            this.MaLHD = Row["MALHD"].ToString();
            this.TenLHD = Row["TENLOAI"].ToString();
        }
        public string MaLHD { get; }
        public string TenLHD { get; private set; }
    }
}
