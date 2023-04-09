using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class CTPhieuKhoDAO
    {
        private static CTPhieuKhoDAO instance;

        public static CTPhieuKhoDAO Instance
        {
            get { if (instance == null) instance = new CTPhieuKhoDAO(); return CTPhieuKhoDAO.instance; }
            private set => instance = value;
        }

        public CTPhieuKhoDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.CTDonhang> GetAll()
        {
            List<DTO.CTDonhang> list = new List<DTO.CTDonhang>();
            string query = "SELECT * FROM CT_PHIEUKHO";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.CTDonhang info = new DTO.CTDonhang(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm 
        public bool Insert(string mapk, string mahh, int sl)
        {
            string query = String.Format("INSERT dbo.CT_PHIEUKHO(MA_PK,MA_HH,SL)VALUES('{0}','{1}',{2})", mapk, mahh, sl);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //// Sửa
        //public bool Update(string madh, string mahh, int sl, double dongia, double thanhtien)
        //{
        //    string query = String.Format("UPDATE dbo.CT_PHIEUKHO SET SL = {2}, DONGIA={3},THANHTIEN={4} WHERE MA_DH = '{0}' AND MA_HH = '{1}'", madh, mahh, sl, dongia, thanhtien);
        //    int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
        //    return _result > 0;
        //}
        ////Xóa
        //public bool Delete(string madh, string mahh)
        //{
        //    string query = String.Format("DELETE dbo.CT_PHIEUKHO WHERE MA_DH = '{0}' AND MA_HH = '{1}'", madh, mahh);
        //    int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
        //    return _result > 0;
        //}
        //// Lấy 1 dũ liệu 
        //public DTO.CTDonhang Get(string madh, string mahh)
        //{
        //    DTO.CTDonhang item = null;
        //    string query = "SELECT * FROM dbo.CT_PHIEUKHO WHERE MA_DH = @MADH AND MA_HH = @MAHH";
        //    DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { madh, mahh });
        //    foreach (DataRow row in result.Rows)
        //    {
        //        item = new DTO.CTDonhang(row);
        //        return item;
        //    }
        //    return item;
        //}

        //public List<DTO.CTDonhang> GetMaHHByMaDH(string madh)
        //{
        //    List<DTO.CTDonhang> list = new List<DTO.CTDonhang>();
        //    string query = "SELECT * FROM CT_PHIEUKHO WHERE MA_DH ='" + madh + "'";
        //    DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
        //    foreach (DataRow item in data.Rows)
        //    {
        //        DTO.CTDonhang info = new DTO.CTDonhang(item);
        //        list.Add(info);
        //    }

        //    return list;
        //}
    }
}
