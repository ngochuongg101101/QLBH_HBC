using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DTO
{
    class Daily
    {
        public string MaDL { get; private set; }

        public string TenDL { get; private set; }
        public string DiaChi { get; private set; }
        public string Sdt { get; private set; }
        public string Email { get; private set; }
        public string MST { get; private set; }

        private float TongNo;
        public Daily(DataRow Row)
        {
            this.MaDL = Row["MADL"].ToString(); ;
            this.TenDL = Row["TENDL"].ToString();
            this.DiaChi = Row["DIACHI"].ToString();
            this.Sdt = Row["SDT"].ToString();
            this.Email = Row["EMAIL"].ToString();
            this.MST = Row["MST"].ToString();
            this.TongNo = (int)Convert.ToDouble(Row["TONGNO"].ToString());
        }
        public Daily(string madl, string tendl, string diachi,string sdt,string email,string mst,float tongno)
        {
            this.MaDL = madl;
            this.TenDL = tendl;
            this.DiaChi = diachi;
            this.Sdt = sdt;
            this.Email = email;
            this.MST = mst;
            this.TongNo = tongno;
        }
    }
}
