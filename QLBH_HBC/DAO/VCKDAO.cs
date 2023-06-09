﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QLBH_HBC.DAO
{
    class VCKDAO
    {
        private static VCKDAO instance;

        public static VCKDAO Instance
        {
            get { if (instance == null) instance = new VCKDAO(); return VCKDAO.instance; }
            private set => instance = value;
        }

        public VCKDAO() { }
        // Lấy danh sách dữ liệu
        public List<DTO.Vckcuoc> GetAll()
        {
            List<DTO.Vckcuoc> list = new List<DTO.Vckcuoc>();
            string query = "SELECT * FROM VCKCUOC";
            DataTable data = Config.DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Vckcuoc info = new DTO.Vckcuoc(item);
                list.Add(info);
            }

            return list;
        }
        // Thêm 
        public bool Insert(string madl,string mavo,int slcuoc)
        {
            string query = String.Format("INSERT dbo.VCKCUOC(MA_DL,MA_VO,SL_CUOC,SL_GIU)VALUES('{0}','{1}',{2},0)", madl,mavo,slcuoc);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Sửa
        public bool Update(string madl, string mavo, int slcuoc,int slgiu)
        {
            string query = String.Format("UPDATE dbo.VCKCUOC SET SL_CUOC = {2}, SL_GIU = {3} WHERE MA_DL = '{0}' AND MA_VO = '{1}'", madl, mavo, slcuoc,slgiu);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        //Xóa
        public bool Delete(string madl, string mavo)
        {
            string query = String.Format("DELETE dbo.VCKCUOC WHERE MA_DL = '{0}' AND MA_VO = '{1}'", madl, mavo);
            int _result = Config.DataProvider.Instance.ExecuteNonQuery(query);
            return _result > 0;
        }
        // Lấy 1 dũ liệu 
        public DTO.Vckcuoc Get(string madl, string mavo)
        {
            DTO.Vckcuoc item = null;
            string query = "SELECT * FROM dbo.VCKCUOC WHERE MA_DL = @MADL AND MA_VO = @MAVO";
            DataTable result = Config.DataProvider.Instance.ExecuteQuery(query, new object[] { madl, mavo });
            foreach (DataRow row in result.Rows)
            {
                item = new DTO.Vckcuoc(row);
                return item;
            }
            return item;
        }
    }
}
