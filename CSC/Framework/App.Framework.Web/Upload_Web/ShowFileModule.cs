using System;
using System.IO;
using System.Web;

namespace App.Framework.Web.Upload
{
    /// <summary>
    /// 
    /// </summary>
    public class ShowFileModule : IHttpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Init(HttpApplication app)
        {
            app.BeginRequest += new EventHandler(app_BeginRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void app_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;

            string fileName = app.Request.PhysicalPath;

            if (string.IsNullOrEmpty(fileName)) return;

            fileName = fileName.ToUpper();

            if (File.Exists(fileName)) return; // 文件已经存在，不做处理。

            if (fileName.IndexOf(ThumbnailIdentify.Identify) < 0) return; // 非特定格式的图片文件，不做处理。

            string[] nameArr = fileName.Split(new string[] { ThumbnailIdentify.Identify }, StringSplitOptions.RemoveEmptyEntries);

            if (nameArr.Length != 2) return; // 非特定格式的图片文件，不做处理。

            if (!nameArr[0].EndsWith("\\")) return; // 非特定格式的图片文件，不做处理。

            if (nameArr[1].IndexOf("\\") < 0) return; // 非特定格式的图片文件，不做处理。

            string originalFileName = nameArr[0] + nameArr[1].Substring(nameArr[1].IndexOf("\\"));

            if (!File.Exists(originalFileName)) return; // 原始图片不存在，不做处理。

            string sizeStr = nameArr[1].Substring(0, nameArr[1].IndexOf("\\"));

            EnumPictureSizeType sizeType;

            if (!Enum.TryParse<EnumPictureSizeType>(sizeStr, true, out sizeType)) return; // 非既定的缩略图尺寸，不做处理。

            try
            {
                int w = int.Parse(sizeStr.Substring(1, sizeStr.IndexOf("H") - 1));

                int h = int.Parse(sizeStr.Substring(sizeStr.IndexOf("H") + 1));

                if (!Directory.Exists(Path.GetDirectoryName(fileName))) Directory.CreateDirectory(Path.GetDirectoryName(fileName)); // 目标目录不存在，创建。

                ThumbnailHelper.MakeThumbnail(originalFileName, fileName, w, h);
            }
            catch
            {
                return;
            }
        }
    }
}