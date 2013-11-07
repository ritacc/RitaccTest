using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms; 
using TM;
using TM.DA.Sys;

namespace TM.FRM
{
    public partial class FrmUserList : Form
    {
		public FrmUserList()
        {
            InitializeComponent();
        }

        UserDA mUserDA = new UserDA();
        private void BindGridView()
        {
            try
            {
                DataTable dt = mUserDA.selectViewData();
                this.dgDate.DataSource = dt;
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

        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            FrmUserEdit edit = new FrmUserEdit();
            edit.Owner = this;
            edit.OpType = "add";
            edit.ShowDialog();
            if (edit.IsReutrn)
                BindGridView();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection gv = this.dgDate.SelectedRows;
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

            FrmUserEdit edit = new FrmUserEdit();
            edit.OpType = "edit";
            edit.ID = id;
            edit.ShowDialog();
            if (edit.IsReutrn)
                BindGridView();
        }
        /// <summary>
        /// 弹出窗体
        /// </summary>
        /// <param name="str">弹出内容</param>
        private void showMsg(string str)
        {
            str = str.Replace("'", "");
            MessageBox.Show(str, "提示");
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection gv = this.dgDate.SelectedRows;
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
                    mUserDA.Delete(id);
                    BindGridView();
                }
                catch (Exception ex)
                {
                    showMsg(ex.Message);
                }
            }
        }

        private void FrmUserList_Load(object sender, EventArgs e)
        {
            BindGridView();
        }
    }
}
