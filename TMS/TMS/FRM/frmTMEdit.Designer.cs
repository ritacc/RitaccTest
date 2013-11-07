namespace TM.FRM
{
	partial class frmTMEdit
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTMEdit));
			this.txtName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtNO = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnCanncel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.cbUse = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(83, 39);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(171, 21);
			this.txtName.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(46, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 6;
			this.label1.Text = "名称：";
			// 
			// txtNO
			// 
			this.txtNO.Location = new System.Drawing.Point(83, 12);
			this.txtNO.Name = "txtNO";
			this.txtNO.Size = new System.Drawing.Size(171, 21);
			this.txtNO.TabIndex = 13;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(23, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 12);
			this.label5.TabIndex = 12;
			this.label5.Text = "条码编号：";
			// 
			// btnCanncel
			// 
			this.btnCanncel.Location = new System.Drawing.Point(155, 138);
			this.btnCanncel.Name = "btnCanncel";
			this.btnCanncel.Size = new System.Drawing.Size(65, 23);
			this.btnCanncel.TabIndex = 21;
			this.btnCanncel.Text = "取消";
			this.btnCanncel.UseVisualStyleBackColor = true;
			this.btnCanncel.Click += new System.EventHandler(this.btnCanncel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(72, 139);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(66, 23);
			this.btnSave.TabIndex = 20;
			this.btnSave.Text = "保存";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// cbUse
			// 
			this.cbUse.AutoSize = true;
			this.cbUse.Location = new System.Drawing.Point(83, 77);
			this.cbUse.Name = "cbUse";
			this.cbUse.Size = new System.Drawing.Size(72, 16);
			this.cbUse.TabIndex = 22;
			this.cbUse.Text = "是否启用";
			this.cbUse.UseVisualStyleBackColor = true;
			// 
			// frmTMEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(321, 180);
			this.Controls.Add(this.cbUse);
			this.Controls.Add(this.btnCanncel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.txtNO);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmTMEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "条码";
			this.Load += new System.EventHandler(this.frmMaintainEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtNO;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnCanncel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.CheckBox cbUse;
	}
}