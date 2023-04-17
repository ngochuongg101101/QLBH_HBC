using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class BoomDAO
    {
        private static BoomDAO instance;

        public static BoomDAO Instance
        {
            get { if (instance == null) instance = new BoomDAO(); return BoomDAO.instance; }
            private set => instance = value;
        }

        public BoomDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.Boom> GetAll()
        {
            List<DTO.Boom> list = new List<DTO.Boom>();
            string query = "SELECT * FROM BOOM";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Boom info = new DTO.Boom(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm 
        public bool Insert(string mabia,string mavo,int sl)
        {
            string query = String.Format("INSERT dbo.BOOM(MA_BIA,MA_VO,SL)VALUES( '{0}', '{1}', {2})", mabia, mavo, sl);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Sửa
        public bool Update(string mabia, string mavo, int sl)
        {
            string query = String.Format("UPDATE dbo.BOOM SET SL = @SL WHERE MA_BIA = @MABIA AND MA_VO = @MAVO");
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query, new object[] { mabia, mavo, sl });
            return _result > 0;
        }
        //Xóa
        public bool Delete(string mabia, string mavo)
        {
            string query = String.Format("DELETE dbo.BOOM WHERE MA_BIA = @MABIA AND MA_VO = @MAVO");
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query, new object[] { mabia, mavo });
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.Boom Get(string mabia, string mavo)
        {
            DTO.Boom item = null;
            string query = "SELECT * FROM dbo.BOOM WHERE MA_BIA = @MABIA AND MA_VO = @MAVO";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { mabia,mavo });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Boom(row);
                return item;
            }
            return item;
        }
    }
}
