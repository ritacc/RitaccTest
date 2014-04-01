using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace App.Framework.Security
{
 /// <summary>
    /// 加密和解密
    /// </summary>
    public class CipherHelper
    {
        #region "Hash 加密采用的算法"
        /// <summary>
        /// Hash 加密采用的算法
        /// </summary>
        public enum HashFormat
        {
            /// <summary>
            /// MD516
            /// </summary>
            MD516,
            /// <summary>
            /// MD532
            /// </summary>
            MD532,
            /// <summary>
            /// RIPEMD160
            /// </summary>
            RIPEMD160,
            /// <summary>
            /// SHA1
            /// </summary>
            SHA1,
            /// <summary>
            /// SHA256
            /// </summary>
            SHA256,
            /// <summary>
            /// SHA384
            /// </summary>
            SHA384,
            /// <summary>
            /// SHA512
            /// </summary>
            SHA512
        };
        #endregion

        #region "基于密钥的 Hash 加密采用的算法"
        /// <summary>
        /// 基于密钥的 Hash 加密采用的算法
        /// </summary>
        public enum HmacFormat
        {
            /// <summary>
            /// HMACMD5
            /// </summary>
            HMACMD5,
            /// <summary>
            /// HMACRIPEMD160
            /// </summary>
            HMACRIPEMD160,
            /// <summary>
            /// HMACSHA1
            /// </summary>
            HMACSHA1,
            /// <summary>
            /// HMACSHA256
            /// </summary>
            HMACSHA256,
            /// <summary>
            /// HMACSHA384
            /// </summary>
            HMACSHA384,
            /// <summary>
            /// HMACSHA512
            /// </summary>
            HMACSHA512
        }
        #endregion

        #region "对称加密采用的算法"
        /// <summary>
        /// 对称加密采用的算法
        /// </summary>
        public enum SymmetricFormat
        {
            /// <summary>
            /// DES
            /// </summary>
            DES,
            /// <summary>
            /// TripleDES
            /// </summary>
            TripleDES,
            /// <summary>
            /// RC2
            /// </summary>
            RC2,
            /// <summary>
            /// Rijindael
            /// </summary>
            Rijindael,
            /// <summary>
            /// AES
            /// </summary>
            AES
        };
        #endregion

        //==================================================================================

        #region "Hash --- 对字符串进行 Hash 加密"
        /// <summary>
        /// 对字符串进行 Hash 加密
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="hashFormat"></param>
        /// <returns></returns>
        public static string Hash(string inputString, HashFormat hashFormat)
        {
            HashAlgorithm algorithm = GetHashAlgorithm(hashFormat);
            algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            if (hashFormat == HashFormat.MD516) return BitConverter.ToString(algorithm.Hash).Replace("-", "").Substring(8, 16).ToUpper();

            return BitConverter.ToString(algorithm.Hash).Replace("-", "").ToUpper();
        }
        #endregion

        #region "Hmac --- 对字符串进行基于密钥的 Hash 加密"
        /// <summary>
        /// 对字符串进行基于密钥的 Hash 加密
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="key"></param>
        /// <param name="hashFormat"></param>
        /// <returns></returns>
        public static string Hmac(string inputString, string key, HmacFormat hashFormat)
        {
            HMAC algorithm = GetHmac(hashFormat, Encoding.UTF8.GetBytes(key));
            algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            return BitConverter.ToString(algorithm.Hash).Replace("-", "").ToUpper();
        }
        #endregion

        #region "SymmetricEncrypt --- 对字符串进行对称加密"
        /// <summary>
        /// 对字符串进行对称加密
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="symmetricFormat"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string SymmetricEncrypt(string inputString, SymmetricFormat symmetricFormat, string key, string iv)
        {
            SymmetricAlgorithm algorithm = GetSymmetricAlgorithm(symmetricFormat);

            byte[] desString = Encoding.UTF8.GetBytes(inputString);
            byte[] desKey = Encoding.UTF8.GetBytes(key.Substring(0, algorithm.Key.Length));
            byte[] desIV = Encoding.UTF8.GetBytes(iv.Substring(0, algorithm.IV.Length));

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, algorithm.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);
            cStream.Write(desString, 0, desString.Length);
            cStream.FlushFinalBlock();
            cStream.Close();

            return Convert.ToBase64String(mStream.ToArray());
        }
        #endregion

        #region "SymmetricDecrypt --- 对字符串进行对称解密"
        /// <summary>
        /// 对字符串进行对称解密
        /// </summary>
        public static string SymmetricDecrypt(string inputString, SymmetricFormat symmetricFormat, string key, string iv)
        {
            SymmetricAlgorithm algorithm = GetSymmetricAlgorithm(symmetricFormat);

            byte[] desString = Convert.FromBase64String(inputString);
            byte[] desKey = Encoding.UTF8.GetBytes(key.Substring(0, algorithm.Key.Length));
            byte[] desIV = Encoding.UTF8.GetBytes(iv.Substring(0, algorithm.IV.Length));

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, algorithm.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);
            cStream.Write(desString, 0, desString.Length);
            cStream.FlushFinalBlock();
            cStream.Close();

            return Encoding.UTF8.GetString(mStream.ToArray());
        }
        #endregion

        #region "RsaEncrypt --- 使用 RSA 公钥加密"
        /// <summary>
        /// 使用 RSA 公钥加密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="publicXml"></param>
        /// <returns></returns>
        public static string RsaEncrypt(string message, string publicXml)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicXml);

                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] resultBytes = rsa.Encrypt(messageBytes, false);

                return Convert.ToBase64String(resultBytes);
            }
        }
        #endregion

        #region "RsaDecrypt --- 使用 RSA 私钥解密"
        /// <summary>
        /// 使用 RSA 私钥解密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="privateXml"></param>
        /// <returns></returns>
        public static string RsaDecrypt(string message, string privateXml)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateXml);

                byte[] messageBytes = Convert.FromBase64String(message);
                byte[] resultBytes = rsa.Decrypt(messageBytes, false);

                return Encoding.UTF8.GetString(resultBytes);
            }
        }
        #endregion

        #region "RsaSignature --- 使用 RSA 私钥签名"
        /// <summary>
        /// 使用 RSA 私钥签名
        /// </summary>
        /// <param name="message"></param>
        /// <param name="privateXml"></param>
        /// <returns></returns>
        public static string RsaSignature(string message, string privateXml)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateXml);

                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] resultBytes = rsa.SignData(messageBytes, "SHA1");

                return BitConverter.ToString(resultBytes).Replace("-", "");
            }
        }
        #endregion

        #region "RsaVerifySign --- 使用 RSA 公钥验证签名"
        /// <summary>
        /// 使用 RSA 公钥验证签名
        /// </summary>
        /// <param name="message"></param>
        /// <param name="signature"></param>
        /// <param name="publicXml"></param>
        /// <returns></returns>
        public static bool RsaVerifySign(string message, string signature, string publicXml)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicXml);

                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] signatureBytes = new byte[signature.Length / 2];

                for (int i = 0; i < signature.Length; i += 2)
                {
                    signatureBytes[i / 2] = byte.Parse(signature.Substring(i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                }

                return rsa.VerifyData(messageBytes, "SHA1", signatureBytes);
            }
        }
        #endregion

        #region "CreateKeyFileForAsymmetricAlgorithm --- 为非对称加密生成密钥对，并存储到文件"
        /// <summary>
        /// 为非对称加密生成密钥对，并存储到文件
        /// </summary>
        /// <param name="asymmetricAlgorithm"></param>
        /// <param name="fileName"></param>
        /// <param name="isPrivate"></param>
        public static void CreateKeyFileForAsymmetricAlgorithm(AsymmetricAlgorithm asymmetricAlgorithm, string fileName, bool isPrivate)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

            string content = asymmetricAlgorithm.ToXmlString(isPrivate);

            File.WriteAllText(fileName, Convert.ToBase64String(Encoding.UTF8.GetBytes(content)));
        }
        #endregion

        #region "GetKeyFromFileForAsymmetricAlgorithm --- 为非对称加密从文件读取密钥"
        /// <summary>
        /// 为非对称加密从文件读取密钥
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetKeyFromFileForAsymmetricAlgorithm(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

            string content = File.ReadAllText(fileName);

            return Encoding.UTF8.GetString(Convert.FromBase64String(content));
        }
        #endregion

        //==================================================================================

        #region "GetHashAlgorithm --- 获取加密方法"
        /// <summary>
        /// 获取加密方法
        /// </summary>
        /// <param name="hashFormat"></param>
        /// <returns></returns>
        private static HashAlgorithm GetHashAlgorithm(HashFormat hashFormat)
        {
            HashAlgorithm algorithm = null;

            switch (hashFormat)
            {
                case HashFormat.MD516:
                    algorithm = MD5.Create();
                    break;

                case HashFormat.MD532:
                    algorithm = MD5.Create();
                    break;

                case HashFormat.RIPEMD160:
                    algorithm = RIPEMD160.Create();
                    break;

                case HashFormat.SHA1:
                    algorithm = SHA1.Create();
                    break;

                case HashFormat.SHA256:
                    algorithm = SHA256.Create();
                    break;

                case HashFormat.SHA384:
                    algorithm = SHA384.Create();
                    break;

                case HashFormat.SHA512:
                    algorithm = SHA512.Create();
                    break;
            }

            return algorithm;
        }
        #endregion

        #region "GetHmac --- 获取加密方法"
        /// <summary>
        /// 获取加密方法
        /// </summary>
        /// <param name="hmacFormat"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static HMAC GetHmac(HmacFormat hmacFormat, byte[] key)
        {
            HMAC hmac = null;

            switch (hmacFormat)
            {
                case HmacFormat.HMACMD5:
                    hmac = new HMACMD5(key);
                    break;

                case HmacFormat.HMACRIPEMD160:
                    hmac = new HMACRIPEMD160(key);
                    break;

                case HmacFormat.HMACSHA1:
                    hmac = new HMACSHA1(key);
                    break;

                case HmacFormat.HMACSHA256:
                    hmac = new HMACSHA256(key);
                    break;

                case HmacFormat.HMACSHA384:
                    hmac = new HMACSHA384(key);
                    break;

                case HmacFormat.HMACSHA512:
                    hmac = new HMACSHA512(key);
                    break;
            }

            return hmac;
        }
        #endregion

        #region "SymmetricAlgorithm --- 获取加密方法"
        /// <summary>
        /// 获取加密方法
        /// </summary>
        /// <param name="symmetricFormat"></param>
        /// <returns></returns>
        private static SymmetricAlgorithm GetSymmetricAlgorithm(SymmetricFormat symmetricFormat)
        {
            SymmetricAlgorithm algorithm = null;

            switch (symmetricFormat)
            {
                case SymmetricFormat.DES:
                    algorithm = DES.Create();
                    break;

                case SymmetricFormat.TripleDES:
                    algorithm = TripleDES.Create();
                    break;

                case SymmetricFormat.RC2:
                    algorithm = RC2.Create();
                    break;

                case SymmetricFormat.Rijindael:
                    algorithm = Rijndael.Create();
                    break;

                case SymmetricFormat.AES:
                    algorithm = Aes.Create();
                    break;
            }

            return algorithm;
        }
        #endregion
    }
}
