using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class PhieukhoDAO
    {
        private static PhieukhoDAO instance;

        public static PhieukhoDAO Instance
        {
            get { if (instance == null) instance = new PhieukhoDAO(); return PhieukhoDAO.instance; }
            private set => instance = value;
        }

        public PhieukhoDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.Phieukho> GetAll()
        {
            List<DTO.Phieukho> list = new List<DTO.Phieukho>();
            string query = "SELECT * FROM PHIEUKHO";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Phieukho info = new DTO.Phieukho(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm
        public string Insert(string ngaytao, string nguoitao, string noidung, string ptvc, string bienso, string malpk,string madh)
        {
            string mapk = (string)Config.DataProvider.Instance.ExecuteScalar("SELECT dbo.AUTO_ID_PHIEUKHO()");

            // Construct the INSERT query with the generated MAPC value
            string query = String.Format("INSERT INTO PHIEUKHO ( NGAYTAO, NGUOITAO, NOIDUNG, PTVC,BIENSO,MA_LPK,MA_DH) VALUES ('{0}', '{1}', N'{2}', N'{3}', N'{4}','{5}','{6}')", ngaytao.Trim(), nguoitao.Trim(), noidung.Trim(), ptvc.Trim(),bienso.Trim(),malpk.Trim(),madh.Trim());

            // Execute the INSERT query and get the number of rows affected
            int numRowsAffected = Config.DataProvider.Instance.ExecuteNonQuery(query);
            if (numRowsAffected > 0)
            {
                return mapk.Trim();
            }
            else
            {
                return null;
            }
        }
        // Sửa
        public bool Update(string mapk, string ngaytao, string noidung, string ptvc, string bienso, string malpk, string madh)
        {
            string query = String.Format("UPDATE dbo.PHIEUKHO SET NGAYTAO = '{0}', NOIDUNG = N'{1}',PTVC=N'{2}',BIENSO=N'{3}', MA_LPK = '{4}', MA_DH='{5}' WHERE MAPK = '{6}'", ngaytao.Trim(), noidung.Trim(), ptvc.Trim(), bienso.Trim(), malpk.Trim(), madh.Trim(), mapk.Trim());
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string madh)
        {
            string query = String.Format("DELETE dbo.PHIEUKHO WHERE MADH = '{0}'", madh);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.Phieukho Get(string mapk)
        {
            DTO.Phieukho item = null;
            string query = "SELECT * FROM dbo.PHIEUKHO WHERE MAPK'" + mapk + "'";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Phieukho(row);
                return item;
            }
            return item;
        }
    }
}
