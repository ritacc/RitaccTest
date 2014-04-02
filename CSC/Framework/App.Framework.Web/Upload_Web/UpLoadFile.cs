using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.Configuration;
using System.Web;

namespace App.Framework.Web.Upload
{
    /// <summary>
    /// 
    /// </summary>
    public class FileUploadResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int error { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class UpLoadFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static List<FileUploadResult> UploadProdcutImages(HttpRequestBase Request)
        {
            string filePath = WebConfigurationManager.AppSettings["AttachmentFilePath"];
            int fileMaxSize = int.Parse(WebConfigurationManager.AppSettings["AttachmentFileMaxSize"]);
            string fileType = WebConfigurationManager.AppSettings["AttachmentFileType"];
            string rootPath = WebConfigurationManager.AppSettings["FILEDOMAIN"];

            var imagePath = new List<FileUploadResult>();
           
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files[i].ContentLength <= 0) continue;

                var result = new FileUploadResult();
                imagePath.Add(result);
                HttpPostedFileBase postFile = Request.Files[i] as HttpPostedFileBase;

                string fileFullNamePath = string.Empty;
                if (postFile.ContentLength < fileMaxSize * 1024)
                {
                    fileFullNamePath = postFile.Save(filePath, null, fileMaxSize, fileType);
                    result.url = rootPath + fileFullNamePath;
                    result.error = 0;
                }
                else
                {
                    result.error = 1;
                    result.message = "文件大小超出范围";
                }
                
            }
            return imagePath;
        }
    }
}
