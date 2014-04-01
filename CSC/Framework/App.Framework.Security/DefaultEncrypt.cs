using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace App.Framework.Security
{
    public class DefaultEncrypt:IEncrypt
    {
        private DefaultEncrypt() { }
        private static string _Key = "DD5FEF9C1C1DA1394D6D34B248C51BE2AD740840";
        private static string _IV = "DD5FEF9C1C1DA1394D6D34B248C51BE2AD740840";

        private static readonly DefaultEncrypt instance = new DefaultEncrypt();
        public static DefaultEncrypt Instance {
            get { return instance; }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encrypt(string content)
        {
            return CipherHelper.SymmetricEncrypt(content.ToUpper()/*密码转换成大写后保存，以使大小写通行*/, CipherHelper.SymmetricFormat.DES, _Key, _IV);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encrpted"></param>
        /// <returns></returns>
        public string Decrypt(string encrpted)
        {
            DESCryptoServiceProvider DESalg = new DESCryptoServiceProvider();
            return CipherHelper.SymmetricDecrypt(encrpted, CipherHelper.SymmetricFormat.DES, _Key, _IV);
        }

        /// <summary>
        /// 判断明文加密后是否等于密文
        /// </summary>
        /// <param name="encrpted">密文</param>
        /// <param name="original">明文</param>
        /// <returns></returns>
        public bool IsEqual(string encrpted, string original)
        {
            return encrpted == Encrypt(original);
        }
    }
}
