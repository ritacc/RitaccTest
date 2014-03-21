namespace AnalysisJson
{
	partial class frmMain
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
			this.gvDataList = new System.Windows.Forms.DataGridView();
			this.btnAnalysis = new System.Windows.Forms.Button();
			this.pTop = new System.Windows.Forms.Panel();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.cbLoadFile = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.gvDataList)).BeginInit();
			this.pTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// gvDataList
			// 
			this.gvDataList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.gvDataList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gvDataList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gvDataList.Location = new System.Drawing.Point(0, 45);
			this.gvDataList.Name = "gvDataList";
			this.gvDataList.RowTemplate.Height = 23;
			this.gvDataList.Size = new System.Drawing.Size(1231, 567);
			this.gvDataList.TabIndex = 1;
			// 
			// btnAnalysis
			// 
			this.btnAnalysis.Location = new System.Drawing.Point(648, 12);
			this.btnAnalysis.Name = "btnAnalysis";
			this.btnAnalysis.Size = new System.Drawing.Size(63, 23);
			this.btnAnalysis.TabIndex = 2;
			this.btnAnalysis.Text = "解析";
			this.btnAnalysis.UseVisualStyleBackColor = true;
			this.btnAnalysis.Click += new System.EventHandler(this.btnAnalysis_Click);
			// 
			// pTop
			// 
			this.pTop.Controls.Add(this.cbLoadFile);
			this.pTop.Controls.Add(this.btnAnalysis);
			this.pTop.Controls.Add(this.txtPath);
			this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pTop.Location = new System.Drawing.Point(0, 0);
			this.pTop.Name = "pTop";
			this.pTop.Size = new System.Drawing.Size(1231, 45);
			this.pTop.TabIndex = 3;
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(23, 13);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(496, 21);
			this.txtPath.TabIndex = 5;
			this.txtPath.Text = "E:\\ritacc\\Project\\AnalysisJson\\File\\";
			// 
			// cbLoadFile
			// 
			this.cbLoadFile.AutoSize = true;
			this.cbLoadFile.Checked = true;
			this.cbLoadFile.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbLoadFile.Location = new System.Drawing.Point(542, 15);
			this.cbLoadFile.Name = "cbLoadFile";
			this.cbLoadFile.Size = new System.Drawing.Size(72, 16);
			this.cbLoadFile.TabIndex = 6;
			this.cbLoadFile.Text = "下载文件";
			this.cbLoadFile.UseVisualStyleBackColor = true;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1231, 612);
			this.Controls.Add(this.gvDataList);
			this.Controls.Add(this.pTop);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "json题库解析";
			((System.ComponentModel.ISupportInitialize)(this.gvDataList)).EndInit();
			this.pTop.ResumeLayout(false);
			this.pTop.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gvDataList;
		private System.Windows.Forms.Button btnAnalysis;
		private System.Windows.Forms.Panel pTop;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.CheckBox cbLoadFile;
	}
}

