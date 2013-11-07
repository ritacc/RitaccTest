using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TM.OR.Sys;
using TM.DA.Sys;
 
 

namespace TM.FRM
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtusercode.Text)
                || string.IsNullOrEmpty(txtpwd.Text))
            {
                MessageBox.Show("请输入用户名或密码！", "出错");
                return;
            }
            UserOR obj = new UserDA().Login(txtusercode.Text, txtpwd.Text);
            if (obj == null)
            {
                MessageBox.Show("用户名或密码错误。", "出错");
                return;
            }
            Globals.isLogin = true;
            Globals.CurrentUser = obj;
            this.Close();
        }

        private void btnCanncel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
