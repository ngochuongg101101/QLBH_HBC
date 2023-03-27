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
        //Login
        public bool Login(string username,string password)
        {
            string query = "SELECT * FROM NGUOIDUNG WHERE dbo.NGUOIDUNG.USERNAME = @USERNAME AND dbo.NGUOIDUNG.PASSWORD = @PASSWORD";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });
            return result.Rows.Count > 0;
        }
        // Lấy họ tên người dùng 
        public DTO.Nguoidung GetFullNameByUsername(string username)
        {
            DTO.Nguoidung nguoidung = null;
            string query = "SELECT NGUOIDUNG.HOTEN FROM NGUOIDUNG WHERE NGUOIDUNG.USERNAME = @USERNAME";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { username });
            foreach(DataRow row in result.Rows)
            {
                nguoidung = new DTO.Nguoidung(row);
                return nguoidung;
            }
            return nguoidung;
        }
    }
}
