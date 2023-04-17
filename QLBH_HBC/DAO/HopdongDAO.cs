using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class HopdongDAO
    {
        private static HopdongDAO instance;

        public static HopdongDAO Instance
        {
            get { if (instance == null) instance = new HopdongDAO(); return HopdongDAO.instance; }
            private set => instance = value;
        }

        public HopdongDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.HopDong> GetAll()
        {
            List<DTO.HopDong> list = new List<DTO.HopDong>();
            string query = "SELECT * FROM dbo.HOPDONG";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.HopDong info = new DTO.HopDong(item);
                list.Add(info);
            }

            return list;
        }

        // Lấy danh sách dữ liệu
        public List<DTO.HopdongDaily> GetAllCustom()
        {
            List<DTO.HopdongDaily> list = new List<DTO.HopdongDaily>();
            string query = "SELECT HOPDONG.MAHD, HOPDONG.NGAYTAO, HOPDONG.NGUOITAO, HOPDONG.NGAYBD, HOPDONG.NGAYKT, HOPDONG.CK, HOPDONG.MA_DL, DAILY.TENDL FROM dbo.HOPDONG INNER JOIN dbo.DAILY ON HOPDONG.MA_DL = DAILY.MADL";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.HopdongDaily info = new DTO.HopdongDaily(item);
                list.Add(info);
            }
            return list;
        }

        // Thêm
        public string Insert(string ngaytao, string nguoitao, string ngaybd,string ngaykt,string ck,string madl)
        {
            string ma = (string)Config.DataProvider.Instance.ExecuteScalar("SELECT dbo.AUTO_ID_HOPDONG()");

            // Construct the INSERT query with the generated MAPC value
            string query = String.Format("INSERT INTO [dbo].[HOPDONG] ([MAHD] ,[NGAYTAO] ,[NGUOITAO] ,[NGAYBD] ,[NGAYKT] ,[CK] ,[MA_DL]) VALUES ( '{0}','{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}')", ma, ngaytao,nguoitao,ngaybd,ngaykt,ck,madl);

            // Execute the INSERT query and get the number of rows affected
            int numRowsAffected = Config.DataProvider.Instance.ExecuteNonQuery(query);
            if (numRowsAffected > 0)
            {
                return ma.Trim();
            }
            else
            {
                return null;
            }
        }
        // Sửa
        public bool Update(string mahd, string ngaytao, string ngaybd, string ngaykt, string ck, string madl)
        {
            string query = String.Format("UPDATE [dbo].[HOPDONG] SET [NGAYTAO] = '{1}',[NGAYBD] = '{2}',[NGAYKT] ='{3}' ,[CK] = '{4}',[MA_DL] = '{5}' WHERE [MAHD] = '{0}'", mahd, ngaytao, ngaybd, ngaykt, ck, madl);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        public bool Delete(string mahd)
        {
            string query = String.Format("DELETE [dbo].[HOPDONG] WHERE [MAHD] = '{0}'", mahd);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.HopDong Get(string madl)
        {
            DTO.HopDong item = null;
            string query = "SELECT * FROM dbo.HOPDONG WHERE MADL = '" + madl + "'";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.HopDong(row);
                return item;
            }
            return item;
        }
        // Lấy 1 danh sách dũ liệu 
        public List<DTO.HopDong> GetList(string madl)
        {
            List<DTO.HopDong> list = new List<DTO.HopDong>();
            string query = "SELECT * FROM dbo.HOPDONG WHERE MADL = '" + madl + "'";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.HopDong info = new DTO.HopDong(item);
                list.Add(info);
            }
            return list;
        }
    }
}
