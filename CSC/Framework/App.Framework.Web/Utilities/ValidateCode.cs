//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：ValidateCode.cs
//文件功能：生成验证码
//
//创建标识：鲜红 || 2011-04-16
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;

namespace App.Framework.Web
{
    /// <summary>
    /// 生成验证码
    /// </summary>
    public static class ValidateCode
    {
        #region 方法：生成验证码
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public static byte[] Generate(Action<string> saveCode)
        {
            int width = 35;
            int height = 16;
            int len = 4;

            Bitmap img = new Bitmap(width, height);
            MemoryStream ms = new MemoryStream();
            Graphics g = Graphics.FromImage(img);
            Rectangle r = new Rectangle(0, 0, img.Width, img.Height);
            Font font = new Font("Arial", 11, FontStyle.Bold);
            LinearGradientBrush brush = new LinearGradientBrush(r, Color.Red, Color.Orange, 90);

            Random random = new Random();
            StringBuilder code = new StringBuilder(len);
            for (int i = 0; i < len; i++) code.Append(random.Next(10));
            g.Clear(Color.White);
            g.DrawString(code.ToString(), font, brush, 0, 0);
            saveCode(code.ToString());
            for (int i = 0; i < 20; i++)
            {
                int x = random.Next(img.Width);
                int y = random.Next(img.Height);
                img.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            g.Dispose();
            ms.Dispose();
            img.Dispose();
            return ms.ToArray();
        }

        #endregion
    }
}
