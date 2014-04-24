using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AnalysisJson
{
    public partial class FrmAddLine : Form
    {
        public FrmAddLine()
        {
            InitializeComponent();
        }

        private void btnSelectFIle_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogTxtAccount = new OpenFileDialog();
            openFileDialogTxtAccount.FileName = "";
            openFileDialogTxtAccount.Filter = "css css.txt|*.*";
            //int number = 0;
            
            if (openFileDialogTxtAccount.ShowDialog() == DialogResult.OK)
            {
                txtFIle.Text = openFileDialogTxtAccount.FileName;
                //StreamReader reader = new StreamReader(openFileDialogTxtAccount.FileName);
                //string strLine = "";
                //while (!reader.EndOfStream)
                //{
                //    strLine = reader.ReadLine();
                //    if (strLine.Trim() == "")
                //        continue;
                //    LyOR mlyor = lyCon.InitLyOR(strLine);
                //    if (mlyor == null)
                //    {
                //        ShowMsg(string.Format("{0}:Init=null请检查", strLine));
                //        continue;
                //    }
                //    AddLyOR(mlyor);
                //}
                //reader.Close();
                //btnInportLyTarget.Text = string.Format("导入({0})", listLyTarget.Count);
            }
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            if(!File.Exists(txtFIle.Text))
            {
                MessageBox.Show("文件不存在。");
                return ;
            }
            StreamReader reader = new StreamReader(txtFIle.Text);
           string strContent= reader.ReadToEnd();
           StringBuilder sb = new StringBuilder();
           int StartIndex = 0;
           int EndIndex = 0;

           string splitStr =txtFGF.Text;
           int AddElen = splitStr.Length;
           if (AddElen < 1)
               AddElen = 1;
           do
           {
               EndIndex = strContent.IndexOf(splitStr, StartIndex);
               if (EndIndex > 0)
               {
                   sb.AppendLine(strContent.Substring(StartIndex, EndIndex - StartIndex + AddElen));
                   StartIndex = EndIndex + AddElen;
               }
               //if (EndIndex > 1000)
               //    break;
           } while (EndIndex > 0);
           rtbContent.Text = sb.ToString();
        }
    }
}
