using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DTO
{
    class Phieuthuchi
    {
        public Phieuthuchi(string maptc,DateTime ngaytao,string nguoitao,string pttt,float tongtien,string mamvv,string mapc,string mahd)
        {
            this.MaPTC = maptc;
            this.NgaytaoPTC = ngaytao;
            this.NguoitaoPTC = nguoitao;
            this.Pttt = pttt;
            this.TongTienPTC = tongtien;
            this.MaMVV = mamvv;
            this.MaPC = mapc;
            this.MaHD = mahd;
        }
        public Phieuthuchi(DataRow Row)
        {
            this.MaPTC = Row["MAPTC"].ToString();
            this.NgaytaoPTC = (DateTime)Row["NGAYTAO"];
            this.NguoitaoPTC = Row["NGUOITAO"].ToString();
            this.Pttt = Row["PTTT"].ToString();
            this.TongTienPTC = (int)Convert.ToDouble(Row["TONGTIEN"].ToString());
            this.MaMVV = Row["MA_MVV"].ToString();
            this.MaPC = Row["MA_PC"].ToString();
            this.MaHD = Row["MA_HD"].ToString();

        }
        public string MaPTC { get; }
        public DateTime NgaytaoPTC { get; private set; }
        public string NguoitaoPTC { get; private set; }
        public string Pttt { get; private set; }
        public float TongTienPTC { get; private set; }
        public string MaMVV { get; private set; }
        public string MaPC { get; private set; }
        public string MaHD { get; private set; }
    }
}
