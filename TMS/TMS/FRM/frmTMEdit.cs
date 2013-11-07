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
	public partial class frmTMEdit : Form
	{
		public frmTMEdit()
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

		private void btnCanncel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private TMOR setValue()
		{
			if (string.IsNullOrEmpty(txtNO.Text)
				 || string.IsNullOrEmpty(txtName.Text)
				)
			{
				MessageBox.Show("数据输入不完整。", "提示");
				return null;
			}
			TMOR m_obj = new TMOR();
			m_obj.NO =txtNO.Text;
			m_obj.Name=txtName.Text;
			m_obj.ISUse=cbUse.Checked?"是":"否";
			 
		 
			return m_obj;
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			TMOR m_obj = setValue();
			if (m_obj == null)
				return;
			if (m_OpType == "add")
			{
				try
				{
					new TMDA().Insert(m_obj);
				}
				catch (Exception ex)
				{
					showMsg(ex.Message);
				}
			}
			else
			{
				try
				{
					m_obj.ID = Convert.ToInt32(m_ID);
					new TMDA().Update(m_obj);
				}
				catch (Exception ex)
				{
					showMsg(ex.Message);
				}
			}
			_IsReutrn = true;
			this.Close();
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
		private void frmMaintainEdit_Load(object sender, EventArgs e)
		{
			if (m_OpType == "edit")
			{
				loadData();
			}
		}

		private void loadData()
		{
			TMOR m_obj = new TMDA().selectARowDate(m_ID);

			txtName.Text = m_obj.Name;
			txtNO.Text = m_obj.NO;
			cbUse.Checked = (m_obj.ISUse == "是");
		}
	}
}
