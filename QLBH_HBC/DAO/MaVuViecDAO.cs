using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class MaVuViecDAO
    {
        private static MaVuViecDAO instance;

        public static MaVuViecDAO Instance
        {
            get { if (instance == null) instance = new MaVuViecDAO(); return MaVuViecDAO.instance; }
            private set => instance = value;
        }

        public MaVuViecDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.Mavuviec> GetAll()
        {
            List<DTO.Mavuviec> list = new List<DTO.Mavuviec>();
            string query = "SELECT * FROM MAVUVIEC";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Mavuviec info = new DTO.Mavuviec(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm
        public bool Insert(string tenvv,string loaivv)
        {
            string query = String.Format("INSERT INTO MAVUVIEC(TENVV,LOAI) VALUES (N'{0}',N'{1}')", tenvv,loaivv);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Sửa
        public bool Update(string tenvv, string loaivv,string mavv)
        {
            string query = String.Format("UPDATE dbo.MAVUVIEC SET TENVV = N'{0}', LOAI = N'{1}' WHERE MAVV = {2}", tenvv,loaivv,mavv);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string mavv)
        {
            string query = String.Format("DELETE dbo.MAVUVIEC WHERE MAVV = '{0}'",mavv);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.Mavuviec Get(string mapc)
        {
            DTO.Mavuviec item = null;
            string query = "SELECT * FROM dbo.MAVUVIEC WHERE MAVV =@MAVV";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { mapc });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Mavuviec(row);
                return item;
            }
            return item;
        }
    }
}
