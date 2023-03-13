using DevExpress.XtraEditors;
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
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        int count = 0;
        public frmLogin()
        {
            InitializeComponent();
        }
        public static string Pass;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string sql = "Select Username,Pass, Usergroup from ACCOUNT where Username='" + txtUser.Text + "' and Pass='" + txtPass.Text + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            int count = dt.Rows.Count;
            if (count == 1)
            {
                MessageBox.Show("Đăng nhập thành công!");
                Form f = new MainForm(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
                f.Show();
                this.Hide();
                Pass = txtPass.Text;
            }
            else
            {
                count++;
                if ( count < 3) 
                {
                    int i = 3 - count;
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng." +
                        "Bạn còn " + i.ToString() + " lần đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Bạn đã nhập sai 3 lần! Thoát chương trình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ckbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbShowPass.Checked)
                txtPass.UseSystemPasswordChar = false;
            if (!ckbShowPass.Checked)
                txtPass.UseSystemPasswordChar = true;
        }
    }
}