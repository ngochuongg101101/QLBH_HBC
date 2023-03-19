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
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        private int CheckLogin;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void labelControlUserName_Click(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void labelControlPassword_Click(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginCheck();
        }

        private bool LoginDB(string username,string password)
        {
            return DAO.NguoidungDAO.Instance.Login(username, password);
        }

        private void LoginCheck()
        {
            try
            {
                if (CheckLogin >  2)
                {
                    MessageBox.Show("Sai mật khẩu hoặc tên người dùng quá nhiều lần.Bạn bị tạm dừng đăng nhập");
                    btnLogin.Enabled = false;
                }
                else
                {
                    string username = txtUsername.Text.Trim().ToUpper();
                    string password = txtPassword.Text.Trim();
                    if (LoginDB(username, password))
                    {
                        MessageBox.Show("Đăng nhập thành công !");
                        MainForm mainForm = new MainForm();
                        this.Hide();
                        mainForm.ShowDialog();
                        this.Show();
                        if (!cbRemember.Checked)
                        {
                            txtUsername.Text = "";
                            txtPassword.Text = "";

                        }
                    }
                    else
                    {
                        CheckLogin++;
                        MessageBox.Show("Sai mật khẩu hoặc tên người dùng. Yêu cần bạn kiểm tra lại !.Bạn còn " + (3 - CheckLogin) + " lần đăng nhập.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi máy chủ đăng nhập !");
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
        }
    }
}