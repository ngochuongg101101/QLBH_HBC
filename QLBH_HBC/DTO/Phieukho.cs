using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
        public Phieukho(DataRow Row)
        {
            this.MaPK = Row["MAPK"].ToString();
            this.NgayTaoPhieuKho = (DateTime)Row["NGAYTAO"];
            this.NguoiTao = Row["NGUOITAO"].ToString();
            this.Noidung = Row["NOIDUNG"].ToString();
            this.PTVC = Row["PTVC"].ToString();
            this.Bienso = Row["BIENSO"].ToString();
            this.MaLPK = Row["MA_LPK"].ToString();
            this.MaDH = Row["MA_DH"].ToString();

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
