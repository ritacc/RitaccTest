namespace TM.FRM
{
    partial class FrmLogin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
			this.label1 = new System.Windows.Forms.Label();
			this.txtusercode = new System.Windows.Forms.TextBox();
			this.txtpwd = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnLogin = new System.Windows.Forms.Button();
			this.btnCanncel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(30, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "帐号：";
			// 
			// txtusercode
			// 
			this.txtusercode.Location = new System.Drawing.Point(86, 38);
			this.txtusercode.Name = "txtusercode";
			this.txtusercode.Size = new System.Drawing.Size(180, 21);
			this.txtusercode.TabIndex = 1;
			// 
			// txtpwd
			// 
			this.txtpwd.Location = new System.Drawing.Point(86, 77);
			this.txtpwd.Name = "txtpwd";
			this.txtpwd.PasswordChar = '*';
			this.txtpwd.Size = new System.Drawing.Size(180, 21);
			this.txtpwd.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(30, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "密码：";
			// 
			// btnLogin
			// 
			this.btnLogin.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.btnLogin.Location = new System.Drawing.Point(86, 137);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(75, 23);
			this.btnLogin.TabIndex = 4;
			this.btnLogin.Text = "登录";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// btnCanncel
			// 
			this.btnCanncel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCanncel.Location = new System.Drawing.Point(178, 137);
			this.btnCanncel.Name = "btnCanncel";
			this.btnCanncel.Size = new System.Drawing.Size(75, 23);
			this.btnCanncel.TabIndex = 5;
			this.btnCanncel.Text = "取消";
			this.btnCanncel.UseVisualStyleBackColor = true;
			this.btnCanncel.Click += new System.EventHandler(this.btnCanncel_Click);
			// 
			// FrmLogin
			// 
			this.AcceptButton = this.btnLogin;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCanncel;
			this.ClientSize = new System.Drawing.Size(327, 205);
			this.Controls.Add(this.btnCanncel);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.txtpwd);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtusercode);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmLogin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "条码管理系统-登陆";
			this.Load += new System.EventHandler(this.FrmLogin_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtusercode;
        private System.Windows.Forms.TextBox txtpwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCanncel;
    }
}