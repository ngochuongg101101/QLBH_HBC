using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class PhieuCuocDAO
    {
        private static PhieuCuocDAO instance;

        public static PhieuCuocDAO Instance
        {
            get { if (instance == null) instance = new PhieuCuocDAO(); return PhieuCuocDAO.instance; }
            private set => instance = value;
        }

        public PhieuCuocDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.Phieucuoc> GetAll()
        {
            List<DTO.Phieucuoc> list = new List<DTO.Phieucuoc>();
            string query = "SELECT * FROM PHIEUCUOC";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Phieucuoc info = new DTO.Phieucuoc(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm
        public string Insert(string ngaytao,string nguoitao, string loai,string madl) 
        {
            string mapc = (string)Config.DataProvider.Instance.ExecuteScalar("SELECT dbo.AUTO_ID_PHIEUCUOC()");

            // Construct the INSERT query with the generated MAPC value
            string query = String.Format("INSERT INTO PHIEUCUOC (MAPC, NGAYTAO, NGUOITAO, LOAI, MA_DL) VALUES ('{0}', '{1}', '{2}', N'{3}', '{4}')", mapc, ngaytao, nguoitao, loai, madl);

            // Execute the INSERT query and get the number of rows affected
            int numRowsAffected = Config.DataProvider.Instance.ExecuteNonQuery(query);
            if (numRowsAffected > 0)
            {
                return mapc.Trim();
            }
            else
            {
                return null;
            }
        }
        // Sửa
        public bool Update(DateTime ngaytao, string loai, string madl,string mapc)
        {
            string query = String.Format("UPDATE dbo.PHIEUCUOC SET NGAYTAO = '{0}', LOAI = '{1}', MA_DL = '{2}' WHERE MAPC = '{3}'",ngaytao,loai,madl,mapc);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string mapc)
        {
            string query = String.Format("DELETE dbo.PHIEUCUOC WHERE MAPC = '{0}'",mapc);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.Phieucuoc Get(string mapc)
        {
            DTO.Phieucuoc item = null;
            string query = "SELECT * FROM dbo.PHIEUCUOC WHERE MAPC =@MAPC";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { mapc });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Phieucuoc(row);
                return item;
            }
            return item;
        }
        // Lấy 1 dũ liệu 
        public DTO.Phieucuoc GetByDataOther(string ngaytao, string nguoitao, string loai, string madl)
        {
            DTO.Phieucuoc item = null;
            string query = "SELECT MAPC FROM dbo.PHIEUCUOC WHERE NGAYTAO = @NGAYTAO, NGUOITAO = @NGUOITAO,LOAI = N'@LOAI', MA_DL=@MADL";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { ngaytao,nguoitao,loai,madl });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Phieucuoc(row);
                return item;
            }
            return item;
        }
    }
}
