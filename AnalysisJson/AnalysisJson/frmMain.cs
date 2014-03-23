using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;

namespace AnalysisJson
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}
		List<Q> ListObj = new List<Q>();
		private void btnAnalysis_Click(object sender, EventArgs e)
		{
			//FileStream fs=new FileStream("",FileMode.
			if (!Directory.Exists(txtPath.Text))
			{
				MessageBox.Show("文件不存在。");
				return;
			}
			ListObj.Clear();
			string[] filses = Directory.GetFiles(txtPath.Text);

			foreach (string str in filses)
			{

				StreamReader sr = new StreamReader(str, Encoding.GetEncoding("gb2312"));
				string Textinfo = sr.ReadToEnd();
				var obj = JsonConvert.DeserializeObject<QList>(Textinfo);
				if (obj != null)
				{
					ListObj.InsertRange(ListObj.Count, obj.Rows);
				}
			}
			ListObj= ListObj.OrderBy(a => a.ID).ToList();
			gvDataList.DataSource = ListObj;
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (Q item in ListObj)
                {
                    if (!string.IsNullOrEmpty(item.ImagePath))
                    {
                        if (cbLoadFile.Checked)
                        {
                            LoadFile(item.ImagePath);
                        }
                        item.ImagePath = HeadleImgPath(item.ImagePath);
                    }
                    sb.Append(string.Format("listTi.add(new TI({0},{1},\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\"));"
                        , item.ID-970
                        , item.subjectType
                        , item.IssueSubject
                        , item.EIssueSubject
                        , item.IssueType_ID
                        , item.Answer
                        , (string.IsNullOrEmpty(item.IssueResult) ? "" : item.IssueResult.Replace("&nbsp;", " ").Replace("\r\n", "").Replace("\n", ""))
                        , item.EIssueResult
                        , item.ImagePath));

                    sb.AppendLine();
                }
                rtb.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		}

		public void LoadFile(string ImagePath)
		{
			string url=string.Format("http://ks.33331111.com{0}",ImagePath.Replace("\\","/"));
			HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			// Sends the HttpWebRequest and waits for the response. 
			HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
			// Gets the stream associated with the response.
			Stream receiveStream = myHttpWebResponse.GetResponseStream();

			int numBytesToRead = (int)myHttpWebResponse.ContentLength;

			byte[] buffer = new byte[1024*1000];
			int numBytesRead = 1;
            
			string path = GetFileInfo(ImagePath);
			FileStream fs = new FileStream(path, FileMode.Create);
			int n = 0;
			do
			{
				// Read may return anything from 0 to numBytesToRead.
				  n = receiveStream.Read(buffer, 0, 1024 * 1000);
				// The end of the file is reached.
				if (n == 0)
					break;
				fs.Write(buffer, 0, n);
				 
				numBytesRead += n;
				numBytesToRead -= n;
			} while (n > 0);

			fs.Flush();
			fs.Close();
			fs.Dispose();
			receiveStream.Close();

		}

		public string GetFileInfo(string imgPath)
		{

            string FilePath = System.Environment.CurrentDirectory + "\\File\\" + HeadleImgPath(imgPath);

			FileInfo fInfo = new FileInfo(FilePath);
			string path = fInfo.Directory.FullName;
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			return FilePath;
		}

        public string HeadleImgPath(string imgPath)
        {
            imgPath = imgPath.Replace("/", "\\").Replace("\\", "_").Replace("图片", "img").Replace("图", "img").Replace("动画", "avi");
            if (imgPath.StartsWith("_"))
                imgPath = imgPath.Substring(1);
            return imgPath.ToLower();
        }
	}
}
