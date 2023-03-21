using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DTO
{
    class Phieukho
    {
        public Phieukho(string mapk,DateTime ngaytao,string nguoitao,string noidung,string ptvc,string bienso,string malpk,string madh)
        {
            this.MaPK = mapk;
            this.NgayTaoPhieuKho = ngaytao;
            this.NguoiTao = nguoitao;
            this.Noidung = noidung;
            this.PTVC = ptvc;
            this.Bienso = bienso;
            this.MaLPK = malpk;
            this.MaDH = madh;
        }

        public string MaPK { get; }
        public DateTime NgayTaoPhieuKho { get; private set; }
        public string NguoiTao { get; private set; }
        public string Noidung { get; private set; }
        public string PTVC { get; private set; }
        public string Bienso { get; private set; }
        public string MaLPK { get; private set; }
        public string MaDH { get; private set; }
    }
}
