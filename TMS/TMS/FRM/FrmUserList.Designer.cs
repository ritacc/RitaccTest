namespace TM.FRM
{
    partial class FrmUserList
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
            this.dgDate = new System.Windows.Forms.DataGridView();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnUserDelete = new System.Windows.Forms.Button();
            this.btnUserEdit = new System.Windows.Forms.Button();
            this.btnUserAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgDate)).BeginInit();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgDate
            // 
            this.dgDate.AllowUserToAddRows = false;
            this.dgDate.AllowUserToDeleteRows = false;
            this.dgDate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDate.Location = new System.Drawing.Point(0, 36);
            this.dgDate.Name = "dgDate";
            this.dgDate.RowTemplate.Height = 23;
            this.dgDate.Size = new System.Drawing.Size(644, 351);
            this.dgDate.TabIndex = 4;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnUserDelete);
            this.panel8.Controls.Add(this.btnUserEdit);
            this.panel8.Controls.Add(this.btnUserAdd);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(644, 36);
            this.panel8.TabIndex = 3;
            // 
            // btnUserDelete
            // 
            this.btnUserDelete.Location = new System.Drawing.Point(147, 7);
            this.btnUserDelete.Name = "btnUserDelete";
            this.btnUserDelete.Size = new System.Drawing.Size(55, 23);
            this.btnUserDelete.TabIndex = 10;
            this.btnUserDelete.Text = "删除";
            this.btnUserDelete.UseVisualStyleBackColor = true;
            this.btnUserDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUserEdit
            // 
            this.btnUserEdit.Location = new System.Drawing.Point(79, 6);
            this.btnUserEdit.Name = "btnUserEdit";
            this.btnUserEdit.Size = new System.Drawing.Size(55, 23);
            this.btnUserEdit.TabIndex = 9;
            this.btnUserEdit.Text = "修改";
            this.btnUserEdit.UseVisualStyleBackColor = true;
            this.btnUserEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnUserAdd
            // 
            this.btnUserAdd.Location = new System.Drawing.Point(14, 6);
            this.btnUserAdd.Name = "btnUserAdd";
            this.btnUserAdd.Size = new System.Drawing.Size(59, 23);
            this.btnUserAdd.TabIndex = 0;
            this.btnUserAdd.Text = "添加";
            this.btnUserAdd.UseVisualStyleBackColor = true;
            this.btnUserAdd.Click += new System.EventHandler(this.btnUserAdd_Click);
            // 
            // FrmUserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 387);
            this.Controls.Add(this.dgDate);
            this.Controls.Add(this.panel8);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUserList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户列表";
            this.Load += new System.EventHandler(this.FrmUserList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgDate)).EndInit();
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgDate;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnUserDelete;
        private System.Windows.Forms.Button btnUserEdit;
        private System.Windows.Forms.Button btnUserAdd;
    }
}