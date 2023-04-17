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
            this.LoaiLPK = loai;
        }
        public LoaiPK(DataRow Row)
        {
            this.MaLPK = Row["MALPK"].ToString();
            this.TenLPK = Row["TENLOAI"].ToString();
            this.LoaiLPK = Row["LOAI"].ToString();
        }
        private string maLPK;
        private string tenLPK;
        public string MaLPK { get => maLPK; set => maLPK = value; }
        public string TenLPK { get => tenLPK; set => tenLPK = value; }
        public string LoaiLPK { get; private set; }
    }
}
