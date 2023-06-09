﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH_HBC
{
    public partial class frmDonhang : DevExpress.XtraEditors.XtraForm
    {
        public frmDonhang()
        {
            InitializeComponent();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT TRANGTHAI, MADH , NGAYTAO , TENDL , NGUOITAO, TONGTIEN  FROM DONHANG " +
            "JOIN DAILY ON MADL = MA_DL";
            gridControl1.DataSource = Config.DataProvider.Instance.ExecuteQuery(sql);
            gridControl1.Refresh();
            //gridView1.DoubleClick += gridView1_DoubleClick;
            //gridView1.OptionsBehavior.ReadOnly = true;
            //gridView1.Appearance.Row.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            var row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            string madh = row["MADH"].ToString();
            MessageBox.Show("hh");

            //đóng form lại và truyền madh về btnMadh trong uc_Kho
        }
    }
}