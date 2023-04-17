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
        public string Insert(string ngaytao, string nguoitao, string trangthai, string madl,string ghichu,double tongtien)
        {
            string madh = (string)Config.DataProvider.Instance.ExecuteScalar("SELECT dbo.AUTO_ID_DONHANG()");

            // Construct the INSERT query with the generated MAPC value
            string query = String.Format("INSERT INTO DONHANG ( NGAYTAO, NGUOITAO, TRANGTHAI, MA_DL,GHICHU,TONGTIEN) VALUES ('{0}', '{1}', N'{2}', N'{3}', N'{4}',{5})", ngaytao, nguoitao, trangthai, madl,ghichu,tongtien);

            // Execute the INSERT query and get the number of rows affected
            int numRowsAffected = Config.DataProvider.Instance.ExecuteNonQuery(query);
            if (numRowsAffected > 0)
            {
                return madh.Trim();
            }
            else
            {
                return null;
            }
        }
        // Sửa
        public bool Update(string ngaytao, string madh, string trangthai, string madl, string ghichu, double tongtien)
        {
            string query = String.Format("UPDATE dbo.DONHANG SET NGAYTAO = '{0}', TRANGTHAI = '{1}', MA_DL = '{2}', GHICHU='{3}',TONGTIEN={4} WHERE MADH = '{3}'", ngaytao, trangthai, madl, ghichu, tongtien,madh);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        public bool UpdateBasic(string ngaytao, string madh, string madl, string ghichu, double tongtien)
        {
            string query = String.Format("UPDATE dbo.DONHANG SET NGAYTAO = '{0}', MA_DL = '{1}', GHICHU=N'{2}',TONGTIEN={3} WHERE MADH = '{4}'", ngaytao, madl, ghichu, tongtien, madh);
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
            string query = "SELECT * FROM dbo.DONHANG WHERE MADH ='"+madh+"'";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Donhang(row);
                return item;
            }
            return item;
        }
        // Lấy 1 dũ liệu 
        //public DTO.Donhang GetByDataOther(string ngaytao, string nguoitao, string loai, string madl)
        //{
        //    DTO.Donhang item = null;
        //    string query = "SELECT MAPC FROM dbo.DONHANG WHERE NGAYTAO = @NGAYTAO, NGUOITAO = @NGUOITAO,LOAI = N'@LOAI', MA_DL=@MADL";
        //    DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { ngaytao, nguoitao, loai, madl });
        //    foreach (DataRow row in result.Rows)
        //    {
        //        item = new DTO.Donhang(row);
        //        return item;
        //    }
        //    return item;
        //}
        //Cập nhật trạng thái   
        public bool UpdateTrangThai(string madh, string trangthai)
        {
            string query = String.Format("UPDATE dbo.DONHANG SET TRANGTHAI = '{0}' WHERE MADH = '{1}'", trangthai, madh);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
    }
}
