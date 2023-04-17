using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DTO
{
    class HanghoaVck
    {
        public string MaHH { get; private set; }
        public string TenHH { get; private set; }
        public string Dvt { get; private set; }
        public int Sl { get; private set; }
        public double GiaCuoc { get; private set; }
        public double Thanhtien { get; private set; }

        public HanghoaVck(string mahh,string tenhh,string dvt,int sl,double giacuoc,double thanhtien)
        {
            this.MaHH = mahh;
            this.TenHH = tenhh;
            this.Dvt = dvt;
            this.Sl = sl;
            this.GiaCuoc = giacuoc;
            this.Thanhtien = thanhtien;
        }
        public HanghoaVck(DataRow Row)
        {
            this.MaHH = Row["MAHH"].ToString();
            this.TenHH = Row["TENHH"].ToString();
            this.Dvt = Row["DVT"].ToString();
            this.Sl = Convert.ToInt32(Row["SOLUONG"].ToString());
            this.GiaCuoc = Convert.ToDouble(Row["GIACUOC"].ToString());
            this.Thanhtien = Convert.ToDouble(Row["THANHTIEN"].ToString());

        }

    }

}
