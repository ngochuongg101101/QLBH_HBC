using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_HBC.DAO
{
    class HoaDonDAO
    {
        private static HoaDonDAO instance;

        public static HoaDonDAO Instance
        {
            get { if (instance == null) instance = new HoaDonDAO(); return HoaDonDAO.instance; }
            private set => instance = value;
        }

        public HoaDonDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.Hoadon> GetAll()
        {
            List<DTO.Hoadon> list = new List<DTO.Hoadon>();
            string query = "SELECT * FROM HOADON";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Hoadon info = new DTO.Hoadon(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm
        //public string Insert(string ngaytao, string nguoitao, string trangthai, string madl, string ghichu, double tongtien)
        //{
        //    string ma = (string)Config.DataProvider.Instance.ExecuteScalar("SELECT dbo.AUTO_ID_HOADON()");

        //    // Construct the INSERT query with the generated MAPC value
        //    string query = String.Format("INSERT INTO HOADON (MAHD, NGAYTAO, NGUOITAO, TRANGTHAI, MA_DL,GHICHU,TONGTIEN) VALUES ('{0}', '{1}', N'{2}', N'{3}', N'{4}',{5})",ma, ngaytao, nguoitao, trangthai, madl, ghichu, tongtien);

        //    // Execute the INSERT query and get the number of rows affected
        //    int numRowsAffected = Config.DataProvider.Instance.ExecuteNonQuery(query);
        //    if (numRowsAffected > 0)
        //    {
        //        return madh.Trim();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        // Sửa
        public bool Update(string ngaytao, string madh, string trangthai, string madl, string ghichu, double tongtien)
        {
            string query = String.Format("UPDATE dbo.HOADON SET NGAYTAO = '{0}', TRANGTHAI = '{1}', MA_DL = '{2}', GHICHU='{3}',TONGTIEN={4} WHERE MAHD = '{3}'", ngaytao, trangthai, madl, ghichu, tongtien, madh);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        public bool UpdateBasic(string mahd,double thanhtoan)
        {
            string query = String.Format("UPDATE dbo.HOADON SET THANHTOAN={1} WHERE MAHD = '{0}'",thanhtoan,mahd);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string madh)
        {
            string query = String.Format("DELETE dbo.HOADON WHERE MAHD = '{0}'", madh);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.Hoadon Get(string madh)
        {
            DTO.Hoadon item = null;
            string query = "SELECT * FROM dbo.HOADON WHERE dbo.HOADON.MA_DH = '" + madh + "' AND HOADON.TONGTIEN > 0 AND HOADON.THANHTOAN = 0";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Hoadon(row);
                return item;
            }
            return item;
        }
        // Lấy 1 dũ liệu 
        //public DTO.Hoadon GetByDataOther(string ngaytao, string nguoitao, string loai, string madl)
        //{
        //    DTO.Hoadon item = null;
        //    string query = "SELECT MAPC FROM dbo.HOADON WHERE NGAYTAO = @NGAYTAO, NGUOITAO = @NGUOITAO,LOAI = N'@LOAI', MA_DL=@MADL";
        //    DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { ngaytao, nguoitao, loai, madl });
        //    foreach (DataRow row in result.Rows)
        //    {
        //        item = new DTO.Hoadon(row);
        //        return item;
        //    }
        //    return item;
        //}
        //Cập nhật trạng thái   
        public bool UpdateTrangThai(string madh, string trangthai)
        {
            string query = String.Format("UPDATE dbo.HOADON SET TRANGTHAI = '{0}' WHERE MAHD = '{1}'", trangthai, madh);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
    }
}
