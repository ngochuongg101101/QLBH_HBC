using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class CTDonhangDAO
    {
        private static CTDonhangDAO instance;

        public static CTDonhangDAO Instance
        {
            get { if (instance == null) instance = new CTDonhangDAO(); return CTDonhangDAO.instance; }
            private set => instance = value;
        }

        public CTDonhangDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.CTDonhang> GetAll()
        {
            List<DTO.CTDonhang> list = new List<DTO.CTDonhang>();
            string query = "SELECT * FROM CT_PHIEUCUOC";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {       
                DTO.CTDonhang info = new DTO.CTDonhang(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm 
        public bool Insert(string mapc, string mavo, int sl)
        {
            string query = String.Format("INSERT dbo.CT_PHIEUCUOC(MA_PC,MA_VO,SL)VALUES('{0}','{1}',{2})", mapc, mavo, sl);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Sửa
        public bool Update(string mapc, string mavo, int sl)
        {
            string query = String.Format("UPDATE dbo.CT_PHIEUCUOC SET SL = {2} WHERE MA_PC = '{0}' AND MA_VO = '{1}'", mapc, mavo, sl);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string mapc, string mavo)
        {
            string query = String.Format("DELETE dbo.CT_PHIEUCUOC WHERE MA_PC = '{0}' AND MA_VO = '{1}'", mapc, mavo);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.CTDonhang Get(string mapc, string mavo)
        {
            DTO.CTDonhang item = null;
            string query = "SELECT * FROM dbo.CT_PHIEUCUOC WHERE MA_PC = @MAPC AND MA_VO = @MAVO";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { mapc, mavo });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.CTDonhang(row);
                return item;
            }
            return item;
        }
    }
}
