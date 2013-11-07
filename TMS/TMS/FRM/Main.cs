using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
 
using System.Data.OleDb;
 
 
using TM;
using TM.DA.Sys;

namespace TM.FRM
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
			 
			BindGridView();
		 
            if (Globals.CurrentUser != null)
                this.Text = this.Text + "   " + Globals.CurrentUser.UserName;
        }

        #region 手机
		TMDA mPhoneDA = new TMDA();
        private void BindGridView()
        {
            try
            {
				//string where = string.Format(" XSRQ>=#{0} 00:00:00# and XSRQ<=#{1} 23:59:59#", dtpPhoneStart.Value.ToString("yyyy-MM-dd"), dtpPhoneEnd.Value.ToString("yyyy-MM-dd"));
				//DataTable dt = mPhoneDA.selectViewData(where);
				//this.dgPhone.DataSource = dt;
				 

                
            }
            catch (System.Exception ex)
            {
                Error.WriteLog("SellClothe : Form:BindGridView(),001", "0001", ex.Message);
            }



        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

		private void btnAdd_Click(object sender, EventArgs e)
		{

			frmTMEdit edit = new frmTMEdit();
			edit.Owner = this;
			edit.OpType = "add";
			edit.ShowDialog();
			if (edit.IsReutrn)
				BindGridView();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection gv = this.dgPhone.SelectedRows;
			if (gv.Count == 0)
			{
				MessageBox.Show("请选择记录？", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			string id = gv[0].Cells[0].Value.ToString();
			if (string.IsNullOrEmpty(id))
			{
				MessageBox.Show("请选择记录？", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			frmTMEdit edit = new frmTMEdit();
			edit.OpType = "edit";
			edit.ID = id;
			edit.ShowDialog();
			if (edit.IsReutrn)
				BindGridView();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection gv = this.dgPhone.SelectedRows;
			if (gv.Count == 0)
			{
				MessageBox.Show("请选择记录？", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			string id = gv[0].Cells[0].Value.ToString();
			if (string.IsNullOrEmpty(id))
			{
				MessageBox.Show("请选择记录？", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (MessageBox.Show("你确定要删除此记录吗？", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				try
				{
					new TMDA().Delete(id);
					BindGridView();
				}
				catch (Exception ex)
				{
					showMsg(ex.Message);
				}
			}
		}
        #endregion
		
		#region Common
		/// <summary>
        /// 弹出窗体
        /// </summary>
        /// <param name="str">弹出内容</param>
        private void showMsg(string str)
        {
            str = str.Replace("'", "");
            MessageBox.Show(str, "提示");
		}

		#endregion

        private void btnUserAdmin_Click(object sender, EventArgs e)
        {
            FrmUserList frm = new FrmUserList();
            frm.Owner = this;
            frm.ShowDialog();
        }
	}

     
}