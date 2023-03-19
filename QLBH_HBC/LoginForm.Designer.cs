
namespace QLBH_HBC
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.panelControlLogin = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cbRemember = new System.Windows.Forms.CheckBox();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.labelControlPassword = new DevExpress.XtraEditors.LabelControl();
            this.labelControlUserName = new DevExpress.XtraEditors.LabelControl();
            this.labelControlLogin = new DevExpress.XtraEditors.LabelControl();
            this.panelImageLogin = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlLogin)).BeginInit();
            this.panelControlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlLogin
            // 
            this.panelControlLogin.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControlLogin.Appearance.Options.UseBackColor = true;
            this.panelControlLogin.Controls.Add(this.panelControl1);
            this.panelControlLogin.Controls.Add(this.labelControlLogin);
            this.panelControlLogin.Controls.Add(this.panelImageLogin);
            this.panelControlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlLogin.Location = new System.Drawing.Point(0, 0);
            this.panelControlLogin.Name = "panelControlLogin";
            this.panelControlLogin.Size = new System.Drawing.Size(741, 384);
            this.panelControlLogin.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.cbRemember);
            this.panelControl1.Controls.Add(this.btnLogin);
            this.panelControl1.Controls.Add(this.txtPassword);
            this.panelControl1.Controls.Add(this.txtUsername);
            this.panelControl1.Controls.Add(this.labelControlPassword);
            this.panelControl1.Controls.Add(this.labelControlUserName);
            this.panelControl1.Location = new System.Drawing.Point(55, 121);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(427, 150);
            this.panelControl1.TabIndex = 3;
            // 
            // cbRemember
            // 
            this.cbRemember.AutoSize = true;
            this.cbRemember.Location = new System.Drawing.Point(18, 82);
            this.cbRemember.Name = "cbRemember";
            this.cbRemember.Size = new System.Drawing.Size(120, 17);
            this.cbRemember.TabIndex = 5;
            this.cbRemember.Text = "Ghi nhớ đăng nhập.";
            this.cbRemember.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(274, 122);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(148, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(102, 56);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(251, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(102, 19);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(251, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // labelControlPassword
            // 
            this.labelControlPassword.Location = new System.Drawing.Point(20, 59);
            this.labelControlPassword.Name = "labelControlPassword";
            this.labelControlPassword.Size = new System.Drawing.Size(48, 13);
            this.labelControlPassword.TabIndex = 1;
            this.labelControlPassword.Text = "Mật khẩu:";
            this.labelControlPassword.Click += new System.EventHandler(this.labelControlPassword_Click);
            // 
            // labelControlUserName
            // 
            this.labelControlUserName.Location = new System.Drawing.Point(18, 20);
            this.labelControlUserName.Name = "labelControlUserName";
            this.labelControlUserName.Size = new System.Drawing.Size(76, 13);
            this.labelControlUserName.TabIndex = 0;
            this.labelControlUserName.Text = "Tên đăng nhập:";
            this.labelControlUserName.Click += new System.EventHandler(this.labelControlUserName_Click);
            // 
            // labelControlLogin
            // 
            this.labelControlLogin.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlLogin.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControlLogin.Appearance.Options.UseFont = true;
            this.labelControlLogin.Appearance.Options.UseForeColor = true;
            this.labelControlLogin.Location = new System.Drawing.Point(55, 36);
            this.labelControlLogin.Name = "labelControlLogin";
            this.labelControlLogin.Size = new System.Drawing.Size(158, 33);
            this.labelControlLogin.TabIndex = 2;
            this.labelControlLogin.Text = "Login System";
            // 
            // panelImageLogin
            // 
            this.panelImageLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelImageLogin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelImageLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelImageLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelImageLogin.BackgroundImage")));
            this.panelImageLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelImageLogin.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.panelImageLogin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panelImageLogin.Location = new System.Drawing.Point(535, 121);
            this.panelImageLogin.Name = "panelImageLogin";
            this.panelImageLogin.Size = new System.Drawing.Size(150, 150);
            this.panelImageLogin.TabIndex = 1;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 384);
            this.Controls.Add(this.panelControlLogin);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("LoginForm.IconOptions.LargeImage")));
            this.Name = "LoginForm";
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.panelControlLogin)).EndInit();
            this.panelControlLogin.ResumeLayout(false);
            this.panelControlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlLogin;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.LabelControl labelControlPassword;
        private DevExpress.XtraEditors.LabelControl labelControlUserName;
        private DevExpress.XtraEditors.LabelControl labelControlLogin;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private System.Windows.Forms.Panel panelImageLogin;
        private System.Windows.Forms.CheckBox cbRemember;
    }
}