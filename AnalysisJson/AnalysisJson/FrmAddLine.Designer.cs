namespace AnalysisJson
{
    partial class FrmAddLine
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
            this.rtbContent = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDo = new System.Windows.Forms.Button();
            this.btnSelectFIle = new System.Windows.Forms.Button();
            this.txtFIle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFGF = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbContent
            // 
            this.rtbContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbContent.Location = new System.Drawing.Point(0, 70);
            this.rtbContent.Name = "rtbContent";
            this.rtbContent.Size = new System.Drawing.Size(962, 528);
            this.rtbContent.TabIndex = 0;
            this.rtbContent.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtFGF);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnDo);
            this.panel1.Controls.Add(this.btnSelectFIle);
            this.panel1.Controls.Add(this.txtFIle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(962, 70);
            this.panel1.TabIndex = 1;
            // 
            // btnDo
            // 
            this.btnDo.Location = new System.Drawing.Point(791, 30);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(75, 23);
            this.btnDo.TabIndex = 2;
            this.btnDo.Text = "执行";
            this.btnDo.UseVisualStyleBackColor = true;
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // btnSelectFIle
            // 
            this.btnSelectFIle.Location = new System.Drawing.Point(710, 29);
            this.btnSelectFIle.Name = "btnSelectFIle";
            this.btnSelectFIle.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFIle.TabIndex = 1;
            this.btnSelectFIle.Text = "选择文件";
            this.btnSelectFIle.UseVisualStyleBackColor = true;
            this.btnSelectFIle.Click += new System.EventHandler(this.btnSelectFIle_Click);
            // 
            // txtFIle
            // 
            this.txtFIle.Location = new System.Drawing.Point(68, 37);
            this.txtFIle.Name = "txtFIle";
            this.txtFIle.Size = new System.Drawing.Size(611, 21);
            this.txtFIle.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "分隔符：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "文件：";
            // 
            // txtFGF
            // 
            this.txtFGF.Location = new System.Drawing.Point(68, 10);
            this.txtFGF.Name = "txtFGF";
            this.txtFGF.Size = new System.Drawing.Size(33, 21);
            this.txtFGF.TabIndex = 4;
            this.txtFGF.Text = "}";
            // 
            // FrmAddLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 598);
            this.Controls.Add(this.rtbContent);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAddLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAddLine";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbContent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDo;
        private System.Windows.Forms.Button btnSelectFIle;
        private System.Windows.Forms.TextBox txtFIle;
        private System.Windows.Forms.TextBox txtFGF;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}