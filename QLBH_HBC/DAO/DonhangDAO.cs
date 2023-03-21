using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class DonhangDAO
    {
        private static DonhangDAO instance;

        public static DonhangDAO Instance
        {
            get { if (instance == null) instance = new DonhangDAO(); return DonhangDAO.instance; }
            private set => instance = value;
        }

        public DonhangDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.Donhang> GetAll()
        {
            List<DTO.Donhang> list = new List<DTO.Donhang>();
            string query = "SELECT * FROM DONHANG";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Donhang info = new DTO.Donhang(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm 
        public bool Insert(DateTime ngaytao,string nguoitao,string trangthai,string madl)
        {
            string query = String.Format("INSERT dbo.DONHANG(NGAYTAO,NGUOITAO,TRANGTHAI,MA_DL)VALUES('{0}','{1}','{2}','{3}')", ngaytao,nguoitao,trangthai,madl);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Sửa
        public bool Update(DateTime ngaytao, string nguoitao, string trangthai, string madl,string madh)
        {
            string query = String.Format("UPDATE dbo.DONHANG SET NGAYTAO = '{0}', NGUOITAO='{1}',TRANGTHAI='{2}',MA_DL='{3}' WHERE MADH = '{4}'", ngaytao, nguoitao, trangthai, madl,madh);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string madh)
        {
            string query = String.Format("DELETE dbo.DONHANG WHERE MADH = '{0}'", madh);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.Donhang Get(string madh)
        {
            DTO.Donhang item = null;
            string query = "SELECT * FROM dbo.DONHANG WHERE MADH = @MADH";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { madh });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Donhang(row);
                return item;
            }
            return item;
        }
    }
}
