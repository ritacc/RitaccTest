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
    public partial class FrmUserEdit : Form
    {
        public FrmUserEdit()
        {
            InitializeComponent();
        }

        #region 属性
        private string m_OpType = string.Empty;
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OpType
        {
            set { m_OpType = value; }
        }

        string m_ID;
        /// <summary>
        /// 操作ID
        /// </summary>
        public string ID
        {
            set { m_ID = value; }
        }

        private bool _IsReutrn = false;

        public bool IsReutrn
        {
            get { return _IsReutrn; }
        }
        #endregion
        /// <summary>
        /// 弹出窗体
        /// </summary>
        /// <param name="str">弹出内容</param>
        private void showMsg(string str)
        {
            str = str.Replace("'", "");
            MessageBox.Show(str, "提示");
        }
        private UserOR setValue()
        {
            if (string.IsNullOrEmpty(txtUserName.Text)
                 || string.IsNullOrEmpty(txtAccount.Text)
                 || string.IsNullOrEmpty(txtUserPwd.Text)
                )
            {
                MessageBox.Show("数据输入不完整。", "提示");
                return null;
            }
            UserOR m_obj = new UserOR();
            m_obj.UserName = txtUserName.Text;
            m_obj.UserCode = txtAccount.Text;
            m_obj.PWD = txtUserPwd.Text;
             
            return m_obj;
        }
        private void loadData()
        {
            UserOR m_obj = new UserDA().selectARowDate(m_ID);

            txtUserName.Text=m_obj.UserName;
            txtAccount.Text=m_obj.UserCode;
            txtUserPwd.Text=m_obj.PWD;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            UserOR m_obj = setValue();
            if (m_obj == null)
                return;
            if (m_OpType == "add")
            {
                try
                {
                    new UserDA().Insert(m_obj);
                }
                catch (Exception ex)
                {
                    showMsg(ex.Message);
                    return;
                }
            }
            else
            {
                try
                {
                    m_obj.ID = Convert.ToInt32(m_ID);
                    new UserDA().Update(m_obj);
                }
                catch (Exception ex)
                {
                    showMsg(ex.Message);
                    return;
                }
            }
            _IsReutrn = true;
            this.Close();
        }

        private void btnCanncel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmUserEdit_Load(object sender, EventArgs e)
        {
            if (m_OpType == "edit")
            {
                loadData();
            }
        }
       
    }
}
