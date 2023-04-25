using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class LoaiHDDAO
    {
        private static LoaiHDDAO instance;

        public static LoaiHDDAO Instance
        {
            get { if (instance == null) instance = new LoaiHDDAO(); return LoaiHDDAO.instance; }
            private set => instance = value;
        }

        public LoaiHDDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.LoaiHD> GetAll()
        {
            List<DTO.LoaiHD> list = new List<DTO.LoaiHD>();
            string query = "SELECT * FROM LOAIHD";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.LoaiHD info = new DTO.LoaiHD(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm 
        public bool Insert(string ten_lhd, string loai_lhd)
        {
            string query = String.Format("INSERT dbo.LOAIHD(TENLOAI,LOAI)VALUES('{0}','{1}',{2})", ten_lhd, loai_lhd);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Sửa
        public bool Update(string MALHD,string ten_lhd, string loai_lhd)
        {
            string query = String.Format("UPDATE dbo.LOAIHD SET TENLOAI = N'{1}', LOAI = N'{2}' WHERE MALHD = '{0}'",MALHD , ten_lhd, loai_lhd);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string MALHD)
        {
            string query = String.Format("DELETE dbo.LOAIHD WHERE MALHD= '{0}'", MALHD);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.LoaiHD Get(string MALHD)
        {
            DTO.LoaiHD item = null;
            string query = "SELECT * FROM dbo.LOAIHD WHERE MALHD = '"+MALHD.Trim().ToUpper()+"'";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.LoaiHD(row);
                return item;
            }
            return item;
        }
    }
}
