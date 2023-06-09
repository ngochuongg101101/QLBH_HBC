﻿using System;
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
        public string Insert(string ngaytao, string nguoitao, string loai, string madl)
        {
            string mapc = (string)Config.DataProvider.Instance.ExecuteScalar("SELECT dbo.AUTO_ID_HANGHOA()");

            // Construct the INSERT query with the generated MAPC value
            string query = String.Format("INSERT INTO HANGHOA (MAPC, NGAYTAO, NGUOITAO, LOAI, MA_DL) VALUES ('{0}', '{1}', '{2}', N'{3}', '{4}')", mapc, ngaytao, nguoitao, loai, madl);

            // Execute the INSERT query and get the number of rows affected
            int numRowsAffected = Config.DataProvider.Instance.ExecuteNonQuery(query);
            if (numRowsAffected > 0)
            {
                return mapc.Trim();
            }
            else
            {
                return null;
            }
        }
        // Sửa
        public bool Update(DateTime ngaytao, string loai, string madl, string mapc)
        {
            string query = String.Format("UPDATE dbo.HANGHOA SET NGAYTAO = '{0}', LOAI = '{1}', MA_DL = '{2}' WHERE MAPC = '{3}'", ngaytao, loai, madl, mapc);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string mapc)
        {
            string query = String.Format("DELETE dbo.HANGHOA WHERE MAPC = '{0}'", mapc);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
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
    }
}
