using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class PhieuThuChiDAO
    {
        private static PhieuThuChiDAO instance;

        public static PhieuThuChiDAO Instance
        {
            get { if (instance == null) instance = new PhieuThuChiDAO(); return PhieuThuChiDAO.instance; }
            private set => instance = value;
        }

        public PhieuThuChiDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.Phieuthuchi> GetAll()
        {
            List<DTO.Phieuthuchi> list = new List<DTO.Phieuthuchi>();
            string query = "SELECT * FROM PHIEUTHUCHI";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Phieuthuchi info = new DTO.Phieuthuchi(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm
        public bool Insert(string ngaytao, string nguoitao, string pttt, int tongtien,string mavv,string mapc)
        {
            // Construct the INSERT query with the generated MAPC value
            string query = String.Format("INSERT INTO PHIEUTHUCHI ( NGAYTAO, NGUOITAO, PTTT, TONGTIEN,MA_VV,MA_PC) VALUES ('{0}', '{1}', N'{2}', {3}, '{4}','{5}')", ngaytao, nguoitao, pttt, tongtien,mavv,mapc);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Thêm
        public bool InsertThanhToan(string ngaytao, string nguoitao, string pttt, int tongtien, string mavv,string mahd)
        {
            // Construct the INSERT query with the generated MAPC value
            string query = String.Format("INSERT INTO PHIEUTHUCHI ( NGAYTAO, NGUOITAO, PTTT, TONGTIEN,MA_VV,MA_HD) VALUES ('{0}', '{1}', N'{2}', {3}, '{4}','{5}')", ngaytao, nguoitao, pttt, tongtien, mavv,mahd);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Sửa
        public bool Update(DateTime ngaytao, string loai, string madl, string mapc)
        {
            string query = String.Format("UPDATE dbo.PHIEUTHUCHI SET NGAYTAO = '{0}', LOAI = '{1}', MA_DL = '{2}' WHERE MAPC = '{3}'", ngaytao, loai, madl, mapc);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string mapc)
        {
            string query = String.Format("DELETE dbo.PHIEUTHUCHI WHERE MAPC = '{0}'", mapc);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.Phieuthuchi Get(string mapc)
        {
            DTO.Phieuthuchi item = null;
            string query = "SELECT * FROM dbo.PHIEUTHUCHI WHERE MAPC =@MAPC";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { mapc });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Phieuthuchi(row);
                return item;
            }
            return item;
        }
        // Lấy 1 dũ liệu 
        public DTO.Phieuthuchi GetByDataOther(string ngaytao, string nguoitao, string loai, string madl)
        {
            DTO.Phieuthuchi item = null;
            string query = "SELECT MAPC FROM dbo.PHIEUTHUCHI WHERE NGAYTAO = @NGAYTAO, NGUOITAO = @NGUOITAO,LOAI = N'@LOAI', MA_DL=@MADL";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { ngaytao, nguoitao, loai, madl });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Phieuthuchi(row);
                return item;
            }
            return item;
        }
        // Lấy 1 dũ liệu từ mã 
        public List<DTO.Phieuthuchi> GetByMaHD(string mahd)
        {
            List<DTO.Phieuthuchi> list = new List<DTO.Phieuthuchi>();
            string query = "SELECT * FROM dbo.PHIEUTHUCHI WHERE MA_HD ='"+ mahd+ "'";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                DTO.Phieuthuchi info = new DTO.Phieuthuchi(row);
                list.Add(info);
            }
            return list;
        }
        // Lấy 1 dũ liệu từ mã 
        public string GetMinNgayTaoByMaHD(string mahd)
        {
            string ngaytao = null;
            string query = "SELECT MIN(dbo.PHIEUTHUCHI.NGAYTAO) FROM dbo.PHIEUTHUCHI WHERE MA_HD ='" + mahd + "'";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                ngaytao = row.ItemArray[0].ToString().Trim();
                return ngaytao;
            }
            return ngaytao;
        }
    }
}
