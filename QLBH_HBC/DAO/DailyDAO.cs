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
            string query = "SELECT * FROM dbo.DAILY";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Daily info = new DTO.Daily(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm
        public string Insert(string tendl, string diachi, string sdt, string email, string mst, int tongno)
        {
            string madl = (string)Config.DataProvider.Instance.ExecuteScalar("SELECT dbo.AUTO_ID_DAILY()");

            // Construct the INSERT query with the generated MAPC value
            string query = String.Format("INSERT INTO dbo.DAILY ( MADL,TENDL,DIACHI,SDT,EMAIL,MST,TONGNO) VALUES ('{0}', N'{1}', N'{2}', '{3}', '{4}',{5},{6})", madl, tendl, diachi, sdt, email, mst, tongno);

            // Execute the INSERT query and get the number of rows affected
            int numRowsAffected = Config.DataProvider.Instance.ExecuteNonQuery(query);
            if (numRowsAffected > 0)
            {
                return madl.Trim();
            }
            else
            {
                return null;
            }
        }
        // Sửa thông tin đại lý
        public bool Update(string madl, string tendl, string diachi, string sdt, string email, string mst, int tongno)
        {
            string query = String.Format("UPDATE [dbo].[DAILY] SET [MADL] = '{0}', [TENDL] = N'{1}', [DIACHI] = N'{2}', [SDT] = '{3}', [EMAIL] = '{4}', [MST] = {5}, [TONGNO] = {6} WHERE ", madl, tendl, diachi, sdt, email, mst, tongno);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.Daily Get(string madl)
        {
            DTO.Daily item = null;
            string query = "SELECT * FROM dbo.DAILY WHERE MADL = '" + madl + "'";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Daily(row);
                return item;
            }
            return item;
        }
        // Lấy 1 danh sách dũ liệu 
        public List<DTO.Daily> GetList(string madl)
        {
            List<DTO.Daily> list = new List<DTO.Daily>();
            string query = "SELECT * FROM dbo.DAILY WHERE MADL = '" + madl + "'";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Daily info = new DTO.Daily(item);
                list.Add(info);
            }
            return list;
        }
    }
}
