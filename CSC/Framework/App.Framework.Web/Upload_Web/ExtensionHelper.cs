using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace App.Framework.Web.Upload
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtensionHelper
    {
        /// <summary>
        /// 输出图片的完整路径
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="fileName">图片的完整路径和名称，若为空，则自动从配置文件的 “EMPTYPICTURE” 获取。若未指定，则出现异常。不包含域。</param>
        /// <returns>图片的完整路径，包含域。</returns>
        public static string ShowPicture(this HtmlHelper html, string fileName)
        {
            return GetPictureName(fileName, null, EnumPictureSizeType.Default);
        }

        /// <summary>
        /// 输出图片的完整路径
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="fileName">图片的完整路径和名称，若为空，则自动从配置文件的 “EMPTYPICTURE” 获取。若未指定，则出现异常。不包含域。</param>
        /// <param name="domain">图片存储的服务器域名，若为空，则自动从配置文件的 “FileDomain” 获取。若未指定，则默认为当前域。</param>
        /// <returns>图片的完整路径，包含域。</returns>
        public static string ShowPicture(this HtmlHelper html, string fileName, string domain)
        {
            return GetPictureName(fileName, domain, EnumPictureSizeType.Default);
        }

        /// <summary>
        /// 输出图片的完整路径
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="fileName">图片的完整路径和名称，若为空，则自动从配置文件的 “EMPTYPICTURE” 获取。若未指定，则出现异常。不包含域。</param>
        /// <param name="sizeType">图片的显示尺寸</param>
        /// <returns>图片的完整路径，包含域。</returns>
        public static string ShowPicture(this HtmlHelper html, string fileName, EnumPictureSizeType sizeType)
        {
            return GetPictureName(fileName, null, sizeType);
        }

        /// <summary>
        /// 输出图片的完整路径
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="fileName">图片的完整路径和名称，若为空，则自动从配置文件的 “EMPTYPICTURE” 获取。若未指定，则出现异常。不包含域。</param>
        /// <param name="domain">图片存储的服务器域名，若为空，则自动从配置文件的 “FileDomain” 获取。若未指定，则默认为当前域。</param>
        /// <param name="sizeType">图片的显示尺寸</param>
        /// <returns>图片的完整路径，包含域。</returns>
        public static string ShowPicture(this HtmlHelper html, string fileName, string domain, EnumPictureSizeType sizeType)
        {
            return GetPictureName(fileName, domain, sizeType);
        }

        /// <summary>
        /// 输出图片的完整路径
        /// </summary>
        /// <param name="fileName">图片的完整路径和名称，若为空，则自动从配置文件的 “EMPTYPICTURE” 获取。若未指定，则出现异常。不包含域。</param>
        /// <param name="domain">图片存储的服务器域名，若为空，则自动从配置文件的 “FileDomain” 获取。若未指定，则默认为当前域。</param>
        /// <param name="sizeType">图片的显示尺寸</param>
        /// <returns>图片的完整路径，包含域。</returns>
        public static string GetPictureName(string fileName, string domain, EnumPictureSizeType sizeType)
        {
            if (string.IsNullOrEmpty(fileName)) fileName = WebConfigurationManager.AppSettings["EMPTYPICTURE"];

            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

            if (string.IsNullOrEmpty(domain)) domain = WebConfigurationManager.AppSettings["FileDomain"];

            if (!domain.EndsWith("/")) domain += "/";

            if (!fileName.StartsWith("/")) fileName = "/" + fileName;

            if (sizeType == EnumPictureSizeType.Default) return domain + fileName.Substring(1);

            string type = Enum.GetName(typeof(EnumPictureSizeType), sizeType);

            string thumbnailStr = "/" + ThumbnailIdentify.Identify + type;

            fileName = fileName.Insert(fileName.LastIndexOf("/"), thumbnailStr);

            return domain + fileName.Substring(1);
        }

        /// <summary>
        /// 输出文件的完整路径
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="fileName">文件的完整路径和名称，不允许为空。不包含域。</param>
        /// <returns>文件的完整路径，包含域。</returns>
        public static string ShowFile(this HtmlHelper html, string fileName)
        {
            return GetFileName(fileName, null);
        }

        /// <summary>
        /// 输出文件的完整路径
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="fileName">文件的完整路径和名称，不允许为空。不包含域。</param>
        /// <param name="domain">文件存储的服务器域名，若为空，则自动从配置文件的 “FileDomain” 获取。若未指定，则默认为当前域。</param>
        /// <returns>文件的完整路径，包含域。</returns>
        public static string ShowFile(this HtmlHelper html, string fileName, string domain)
        {
            return GetFileName(fileName, domain);
        }

        /// <summary>
        /// 输出文件的完整路径
        /// </summary>
        /// <param name="fileName">文件的完整路径和名称，不允许为空。不包含域。</param>
        /// <param name="domain">文件存储的服务器域名，若为空，则自动从配置文件的 “FileDomain” 获取。若未指定，则默认为当前域。</param>
        /// <returns>文件的完整路径，包含域。</returns>
        public static string GetFileName(string fileName, string domain)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

            if (string.IsNullOrEmpty(domain)) domain = WebConfigurationManager.AppSettings["FileDomain"];

            if (!domain.EndsWith("/")) domain += "/";

            if (fileName.StartsWith("/")) fileName = fileName.Substring(1);

            return domain + fileName;
        }

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="file">HttpPostedFileBase</param>
        /// <returns>若保存成功，则返回文件存储相对路径及名称。如：“/Files/Photo/20100101/12345.PNG”。</returns>
        public static string Save(this HttpPostedFileBase file)
        {
            return SaveFile(file, null, null, null, null);
        }

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="file">HttpPostedFileBase</param>
        /// <param name="path">要保存的文件路径，不包含名称。若为空，则保存到根目录。</param>
        /// <returns>若保存成功，则返回文件存储相对路径及名称。如：“/Files/Photo/20100101/12345.PNG”。</returns>
        public static string Save(this HttpPostedFileBase file, string path)
        {
            return SaveFile(file, path, null, null, null);
        }

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="file">HttpPostedFileBase</param>
        /// <param name="path">要保存的文件路径，不包含名称。若为空，则保存到根目录。</param>
        /// <param name="name">要保存的文件名称，不包含路径。若为空，则自动生成随机路径和名称。默认按照时间分文件夹进行存储。</param>
        /// <returns>若保存成功，则返回文件存储相对路径及名称。如：“/Files/Photo/20100101/12345.PNG”。</returns>
        public static string Save(this HttpPostedFileBase file, string path, string name)
        {
            return SaveFile(file, path, name, null, null);
        }

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="file">HttpPostedFileBase</param>
        /// <param name="path">要保存的文件路径，不包含名称。若为空，则保存到根目录。</param>
        /// <param name="name">要保存的文件名称，不包含路径。若为空，则自动生成随机路径和名称。默认按照时间分文件夹进行存储。</param>
        /// <param name="allowMaxLength">允许上传最大文件大小，单位：KB.</param>
        /// <param name="allowFileType">允许上传文件的类型扩展名，不包含点，不区分大小写。以“,”或“|”分隔开。默认：“JPG|JPEG|PNG|GIF|BMP”。</param>
        /// <returns>若保存成功，则返回文件存储相对路径及名称。如：“/Files/Photo/20100101/12345.PNG”。</returns>
        public static string Save(this HttpPostedFileBase file, string path, string name, int allowMaxLength, string allowFileType)
        {
            return SaveFile(file, path, name, allowMaxLength, allowFileType);
        }

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="file">HttpPostedFileBase</param>
        /// <param name="path">要保存的文件路径，不包含名称。若为空，则保存到根目录。</param>
        /// <param name="name">要保存的文件名称，不包含路径。若为空，则自动生成随机路径和名称。默认按照时间分文件夹进行存储。</param>
        /// <param name="allowMaxLength">允许上传最大文件大小，默认值：2048，单位：KB.</param>
        /// <param name="allowFileType">允许上传文件的类型扩展名，不包含点，不区分大小写。以“,”或“|”分隔开。默认：“JPG|JPEG|PNG|GIF|BMP”。</param>
        /// <returns>若保存成功，则返回文件存储相对路径及名称。如：“/Files/Photo/20100101/12345.PNG”。</returns>
        public static string SaveFile(HttpPostedFileBase file, string path, string name, int? allowMaxLength, string allowFileType)
        {
            if (file == null) throw new ArgumentNullException("file");

            if (file.ContentLength == 0) throw new Exception("请选择上传文件...");

            string fileName = GetSaveFileName(path, name, file);

            if (!allowMaxLength.HasValue) allowMaxLength = 2048;

            if (string.IsNullOrEmpty(allowFileType)) allowFileType = "JPG|JPEG|PNG|GIF|BMP";

            if (file.ContentLength > allowMaxLength.Value * 1024) throw new Exception(string.Format("你选择的上传文件过大，最多不能超过 {0} KB.", allowMaxLength.Value));

            string[] fileTypes = allowFileType.Split(new char[] { '|', ',' }, StringSplitOptions.RemoveEmptyEntries);

            bool isValidFile = false;

            for (int i = 0; i < fileTypes.Length; i++)
            {
                if (string.Compare(Path.GetExtension(file.FileName).Substring(1), fileTypes[i], true) == 0) isValidFile = true;
            }

            if (!isValidFile) throw new Exception(string.Format("上传文件类型错误，仅允许上传扩展名为 {0} 的文件。", allowFileType.ToUpper()));

            try
            {
                UploadFile(file, fileName);
            }
            catch
            {
                throw new Exception("上传文件错误，请稍后重试。");
            }

            return fileName;
        }

        /// <summary>
        /// 删除 FTP 上的图片及其缩略图
        /// </summary>
        /// <param name="fileName">图片完整名称</param>
        public static void DeletePicture(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            string url = WebConfigurationManager.AppSettings["FTPURL"];
            string user = WebConfigurationManager.AppSettings["FTPUSER"];
            string pass = WebConfigurationManager.AppSettings["FTPPASS"];

            if (!url.EndsWith("/")) url += "/";

            FtpWebRequest request = null;
            FtpWebResponse response = null;

            try
            {
                response = Connect(ref request, url + fileName, WebRequestMethods.Ftp.DeleteFile, user, pass); // 删除原始文件
            }
            catch
            {
                // 若原始文件不存在，或删除错误，则忽略，继续按照规则删除缩略图
            }

            try
            {
                response = Connect(ref request, url + fileName.Substring(0, fileName.LastIndexOf("/")), WebRequestMethods.Ftp.ListDirectory, user, pass); // 获取目录及文件列表

                StreamReader reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();

                while (line != null) // 无目录及文件时结束
                {
                    // 不是缩略图目录，则退出循环。
                    // 理论上来说，应该循环读取直到流结束。
                    // 但考虑到 ListDirectory 返回的顺序是先目录，后文件，且按名称排序，所以一旦达到非缩略图目录时，就不需要再继续遍历了。
                    if (line.IndexOf(ThumbnailIdentify.Identify) == -1) break;

                    string newFileName = fileName.Insert(fileName.LastIndexOf("/"), "/" + line);

                    try
                    {
                        response = Connect(ref request, url + newFileName, WebRequestMethods.Ftp.DeleteFile, user, pass); // 删除当前目录的缩略图
                    }
                    catch
                    {
                        // 若缩略图不存在，则忽略，继续删除下一个目录的缩略图
                    }

                    line = reader.ReadLine();
                }
            }
            catch
            {
                return; // 获取目录及文件列表失败，返回。
            }
        }

        /// <summary>
        /// 连接FTP并执行命令
        /// </summary>
        /// <param name="request"></param>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        private static FtpWebResponse Connect(ref FtpWebRequest request, string url, string method, string user, string pass)
        {
            request = (FtpWebRequest)FtpWebRequest.Create(url);

            request.UseBinary = true;
            request.Method = method;
            request.Credentials = new NetworkCredential(user, pass);

            return (FtpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// 创建FTP文件夹
        /// </summary>
        /// <param name="request"></param>
        /// <param name="url"></param>
        /// <param name="fileName"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        private static void MakeDirectory(ref FtpWebRequest request, string url, string fileName, string user, string pass)
        {
            if (!url.EndsWith("/")) url += "/";

            string path = Path.GetDirectoryName(fileName);

            if (string.IsNullOrEmpty(path)) return;

            string[] arr = path.Split('\\');

            for (int i = 0; i < arr.Length; i++)
            {
                url = url + arr[i] + "/";

                try
                {
                    Connect(ref request, url, WebRequestMethods.Ftp.MakeDirectory, user, pass);
                }
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        private static void UploadFile(HttpPostedFileBase file, string fileName)
        {
            string url = WebConfigurationManager.AppSettings["FTPURL"];
            string user = WebConfigurationManager.AppSettings["FTPUSER"];
            string pass = WebConfigurationManager.AppSettings["FTPPASS"];

            FtpWebRequest request = null;

            MakeDirectory(ref request, url, fileName, user, pass);

            FtpWebResponse response = Connect(ref request, url + fileName, WebRequestMethods.Ftp.UploadFile, user, pass);

            Stream requestStream = request.GetRequestStream();

            byte[] buffer = new byte[1024];

            int bytesRead = 0;

            while (true)
            {
                bytesRead = file.InputStream.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0) break;

                requestStream.Write(buffer, 0, bytesRead);
            }

            requestStream.Close();

            response = (FtpWebResponse)request.GetResponse();

            //return response.StatusCode.ToString();
        }

        /// <summary>
        /// 获取要保存的文件名称
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        private static string GetSaveFileName(string path, string name, HttpPostedFileBase file)
        {
            string fileName = string.Empty;

            if (string.IsNullOrEmpty(path)) path = "/";

            if (!path.StartsWith("/")) path = "/" + path;

            if (!path.EndsWith("/")) path = path + "/";

            if (!string.IsNullOrEmpty(name))
            {
                while (name.StartsWith("/"))
                {
                    name = name.Substring(1, name.Length);
                }

                fileName = path + name;
            }
            else
            {
                fileName = GetRandomFileNameByDate(path, Path.GetExtension(file.FileName));
            }

            return fileName;
        }

        /// <summary>
        /// 根据当前日期、指定根路径和扩展名随机获得一个文件完整存储路径和名称。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        private static string GetRandomFileNameByDate(string path, string extension)
        {
            if (string.IsNullOrEmpty(extension)) throw new ArgumentNullException("extension");

            if (!extension.StartsWith(".")) extension = "." + extension;

            StringBuilder fileName = new StringBuilder(path);

            fileName.AppendFormat("{0}/{1}", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"));

            Random random = new Random();

            fileName.Append(random.Next(1000, 10000));

            return fileName.ToString() + extension;
        }
    }
}