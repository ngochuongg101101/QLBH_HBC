using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DAO
{
    class NguoidungDAO
    {
        private static NguoidungDAO instance;
        public static NguoidungDAO Instance
        {
            get { if (instance == null) instance = new NguoidungDAO(); return instance; }
            private set { instance = value; }
        }
        private NguoidungDAO() { }
        public bool Login(string username,string password)
        {
            string query = "SELECT * FROM NGUOIDUNG WHERE dbo.NGUOIDUNG.USERNAME = @USERNAME AND dbo.NGUOIDUNG.PASSWORD = @PASSWORD";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });
            return result.Rows.Count > 0;
        }
    }
}
