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
        public bool Insert(DateTime ngaytao,string nguoitao, string loai,string madl) 
        {
            //string query = String.Format("INSERT INTO PHIEUCUOC(NGAYTAO, NGUOITAO,LOAI, MA_DL) VALUES ({0},{1},N'{2}',{3})", ngaytao, nguoitao, loai, madl);
            string query = String.Format("INSERT INTO PHIEUCUOC(NGAYTAO, NGUOITAO,LOAI, MA_DL) VALUES ('2023/01/01','{0}',N'{1}','{2}')",  nguoitao, loai, madl);

            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Sửa
        public bool Update(string mabia, string mavo, int sl)
        {
            string query = String.Format("UPDATE dbo.BOOM SET SL = @SL WHERE MA_BIA = @MABIA AND MA_VO = @MAVO");
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query, new object[] { mabia, mavo, sl });
            return _result > 0;
        }
        //Xóa
        public bool Delete(string mabia, string mavo)
        {
            string query = String.Format("DELETE dbo.BOOM WHERE MA_BIA = @MABIA AND MA_VO = @MAVO");
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query, new object[] { mabia, mavo });
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.Phieucuoc Get(string mabia, string mavo)
        {
            DTO.Phieucuoc item = null;
            string query = "SELECT * FROM dbo.BOOM WHERE MA_BIA = @MABIA AND MA_VO = @MAVO";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { mabia, mavo });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Phieucuoc(row);
                return item;
            }
            return item;
        }
    }
}
