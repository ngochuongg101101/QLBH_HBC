using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class DailyDAO
    {
        private static DailyDAO instance;

        public static DailyDAO Instance
        {
            get { if (instance == null) instance = new DailyDAO(); return DailyDAO.instance; }
            private set => instance = value;
        }

        public DailyDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.Daily> GetAll()
        {
            List<DTO.Daily> list = new List<DTO.Daily>();
            string query = "SELECT * FROM DAILY";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Daily info = new DTO.Daily(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm 
        public bool Insert(string tendl,string diachi,string sdt,string email,int mst,int tongno)
        {
            string query = String.Format("INSERT dbo.DAILY(TENDL,DIACHI,SDT,EMAIL,MST,TONGNO)VALUES('{0}','{1}','{2}','{3}',{4},{5})", tendl,diachi,sdt,email,mst,tongno);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Sửa
        public bool Update(string madl,string tendl, string diachi, string sdt, string email, int mst, int tongno)
        {
            string query = String.Format("UPDATE dbo.DAILY SET TENDL = '{0}',DIACHI = '{1}',SDT = '{2}',EMAIL = '{3}',MST = {4},TONGNO={5} WHERE MADL = '{6}' AND MA_VO = '{1}'", tendl, diachi, sdt, email, mst, tongno,madl);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string madl)
        {
            string query = String.Format("DELETE dbo.DAILY WHERE MADL = '{0}'", madl);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.Daily Get(string madl)
        {
            DTO.Daily item = null;
            string query = "SELECT * FROM dbo.DAILY WHERE MADL = @MADL";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { madl });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Daily(row);
                return item;
            }
            return item;
        }
    }
}
