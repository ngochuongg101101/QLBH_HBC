using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class HanghoaDAO
    {
        private static HanghoaDAO instance;

        public static HanghoaDAO Instance
        {
            get { if (instance == null) instance = new HanghoaDAO(); return HanghoaDAO.instance; }
            private set => instance = value;
        }

        public HanghoaDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.Hanghoa> GetAll()
        {
            List<DTO.Hanghoa> list = new List<DTO.Hanghoa>();
            string query = "SELECT * FROM HANGHOA";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Hanghoa info = new DTO.Hanghoa(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm
        public string Insert(string tenhh,int dongia,int giacuoc,string dvt,int sl,string loai,int coVck)
        {
            string mahh = (string)Config.DataProvider.Instance.ExecuteScalar("SELECT dbo.AUTO_ID_HANGHOA()");

            // Construct the INSERT query with the generated MAPC value
            string query = String.Format("INSERT INTO HANGHOA ( TENHH,DONGIA,GIACUOC,DVT,SL,LOAI,CO_VCK) VALUES ( N'{0}', {1}, {2}, N'{3}',{4},N'{5}',{6})", tenhh,dongia,giacuoc,dvt,sl,loai,coVck);

            // Execute the INSERT query and get the number of rows affected
            int numRowsAffected = Config.DataProvider.Instance.ExecuteNonQuery(query);
            if (numRowsAffected > 0)
            {
                return mahh.Trim();
            }
            else
            {
                return null;
            }
        }
        //Thêm một hàng hoá gồm tenhh, dongia, dvt, sl, loai, coVck
        public string InsertBia(string tenhh, int dongia, string dvt, string loai, bool coVck)
        {
            int number_coVck = 0; // false

            string mahh = (string)Config.DataProvider.Instance.ExecuteScalar("SELECT dbo.AUTO_ID_HANGHOA()");
            if (coVck)
            {
                number_coVck = 1;
            }
            // Construct the INSERT query with the generated MAPC value
            string query = String.Format("INSERT INTO HANGHOA ( TENHH,DONGIA,DVT,SL,LOAI,CO_VCK) VALUES ( N'{0}', {1}, N'{2}',{3},N'{4}',{5})", tenhh, dongia, dvt, 0, loai, number_coVck);

            // Execute the INSERT query and get the number of rows affected
            int numRowsAffected = Config.DataProvider.Instance.ExecuteNonQuery(query);
            if (numRowsAffected > 0)
            {
                return mahh.Trim();
            }
            else
            {
                return null;
            }
        }
        //Thêm một hàng hoá gồm tenhh, dongia, dvt, sl, loai, coVck
        public string InsertVo(string tenhh, int dongia,int giacuoc, string dvt, string loai)
        {
            string mahh = (string)Config.DataProvider.Instance.ExecuteScalar("SELECT dbo.AUTO_ID_HANGHOA()");

            // Construct the INSERT query with the generated MAPC value
            string query = String.Format("INSERT INTO HANGHOA ( TENHH,DONGIA,GIACUOC,DVT,SL,LOAI) VALUES ( N'{0}', {1}, {2}, N'{3}',{4},N'{5}',{6})", tenhh, dongia, giacuoc, dvt, 0, loai);

            // Execute the INSERT query and get the number of rows affected
            int numRowsAffected = Config.DataProvider.Instance.ExecuteNonQuery(query);
            if (numRowsAffected > 0)
            {
                return mahh.Trim();
            }
            else
            {
                return null;
            }
        }
        // Sửa
        public bool Update(string mahh,string tenhh, int dongia, int giacuoc, string dvt, int sl, string loai, int coVck)
        {
            string query = String.Format("UPDATE dbo.HANGHOA SET TENHH = N'{0}', DONGIA = {1}, GIACUOC = {2}, DVT = N'{3}, SL = {4}, LOAI= N'{5}', CO_VCK={6} WHERE MAPC = '{7}'", tenhh, dongia, giacuoc, dvt, sl, loai, coVck,mahh);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        //public bool Delete(string mapc)
        //{
        //    string query = String.Format("DELETE dbo.HANGHOA WHERE MAPC = '{0}'", mapc);
        //    int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
        //    return _result > 0;
        //}
        // Lấy 1 dũ liệu 
        public DTO.Hanghoa Get(string mahh)
        {
            DTO.Hanghoa item = null;
            string query = "SELECT * FROM dbo.HANGHOA WHERE MAHH = '"+mahh+"'";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Hanghoa(row);
                return item;
            }
            return item;
        }
        // Lấy 1 danh sách dũ liệu 
        public List<DTO.Hanghoa> GetList(string mahh)
        {
            List<DTO.Hanghoa> list = new List<DTO.Hanghoa>();
            string query = "SELECT * FROM dbo.HANGHOA WHERE MAHH = '" + mahh + "'";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Hanghoa info = new DTO.Hanghoa(item);
                list.Add(info);
            }
            return list;
        }
        // Lấy 1 dũ liệu 
        public DTO.Hanghoa GetByDataOther(string ngaytao, string nguoitao, string loai, string madl)
        {
            DTO.Hanghoa item = null;
            string query = "SELECT MAPC FROM dbo.HANGHOA WHERE NGAYTAO = @NGAYTAO, NGUOITAO = @NGUOITAO,LOAI = N'@LOAI', MA_DL=@MADL";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { ngaytao, nguoitao, loai, madl });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Hanghoa(row);
                return item;
            }
            return item;
        }
        // Check dũ liệu la loai bia
        public bool GetByDataOtherByBear(string mahh)
        {
            string query = String.Format("SELECT COUNT(MAHH) FROM HANGHOA WHERE HANGHOA.MAHH = '{0}' AND HANGHOA.LOAI = N'Bia'", mahh);
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                int count = Convert.ToInt32(row[0]);
                return count > 0;
            }
            return false;
        }
        // Check dũ liệu la loai vo
        public bool GetByDataOtherByBark(string mahh)
        {
            string query = String.Format("SELECT COUNT(MAHH) FROM HANGHOA WHERE HANGHOA.MAHH = '{0}' AND HANGHOA.LOAI = N'Vỏ'", mahh);
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                int count = Convert.ToInt32(row[0]);
                return count > 0;
            }
            return false;
        }
        // Check số lượng tồn
        public bool CheckSLTonTrongKho(string mahh,int sl_check)
        {
            string query = String.Format("SELECT SL FROM dbo.HANGHOA WHERE MAHH = '{0}'", mahh);
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                int sl = Convert.ToInt32(row[0]);
                return sl - sl_check > 0;
            }
            return false;
        }
        // Lấy sô lượng hàng hoá
        public int GetSL(string mahh)
        {
            string query = String.Format("SELECT SL FROM dbo.HANGHOA WHERE MAHH = '{0}'", mahh);
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                int sl = Convert.ToInt32(row[0]);
                return sl;
            }
            return 0;
        }
        // Cập nhật sô lượng hàng hoá khi xuất kho
        public bool UpdateSL(string mahh, int sl)
        {
            string query = String.Format("UPDATE dbo.HANGHOA SET SL = {0} WHERE MAHH = '{1}'", sl, mahh);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
    }
}
