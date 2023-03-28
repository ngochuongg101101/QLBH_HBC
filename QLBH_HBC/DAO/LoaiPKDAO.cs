using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class LoaiPKDAO
    {
        private static LoaiPKDAO instance;

        public static LoaiPKDAO Instance
        {
            get { if (instance == null) instance = new LoaiPKDAO(); return LoaiPKDAO.instance; }
            private set => instance = value;
        }

        public LoaiPKDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.LoaiPK> GetAll()
        {
            List<DTO.LoaiPK> list = new List<DTO.LoaiPK>();
            string query = "SELECT * FROM LOAIPK";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.LoaiPK info = new DTO.LoaiPK(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm 
        public bool Insert(string ten_lpk, string loai_lpk)
        {
            string query = String.Format("INSERT dbo.LOAIPK(TENLOAI,LOAI)VALUES('{0}','{1}',{2})", ten_lpk, loai_lpk);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Sửa
        public bool Update(string malpk,string ten_lpk, string loai_lpk)
        {
            string query = String.Format("UPDATE dbo.LOAIPK SET TENLOAI = N'{1}', LOAI = N'{2}' WHERE MALPK = '{0}'",malpk , ten_lpk, loai_lpk);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string malpk)
        {
            string query = String.Format("DELETE dbo.LOAIPK WHERE MALPK= '{0}'", malpk);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.LoaiPK Get(string malpk)
        {
            DTO.LoaiPK item = null;
            string query = "SELECT * FROM dbo.LOAIPK WHERE MALPK = '"+malpk.Trim().ToUpper()+"'";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.LoaiPK(row);
                return item;
            }
            return item;
        }
    }
}
