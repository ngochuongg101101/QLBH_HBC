using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
        public Phieucuoc(DataRow Row)
        {
            this.MaPC = Row["MAPC"].ToString();
            this.NgayTaoPC = (DateTime)Row["NGAYTAO"];
            this.Nguoitao = Row["NGUOITAO"].ToString();
            this.Loai = Row["LOAI"].ToString();
            this.MaDL = Row["MA_DL"].ToString();
        }
        public string MaPC { get; }
        public DateTime NgayTaoPC { get; private set; }
        public string Nguoitao { get; private set; }
        public string Loai { get; private set; }
        public string MaDL { get; private set; }
    }
}
