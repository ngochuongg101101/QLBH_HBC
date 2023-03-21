using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
        public LoaiPK(DataRow Row)
        {
            this.MaLPK = Row["MALPK"].ToString();
            this.TenLPK = Row["TENLOAI"].ToString();
        }
        public string MaLPK { get; }
        public string TenLPK { get; private set; }
        public string Loai { get; private set; }
    }
}
