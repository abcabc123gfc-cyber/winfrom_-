using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptTool
{
    public static class DESHelper
    {
        private static readonly string keyString = ConfigurationManager.AppSettings["DESKEY"];
        // 它使用56位密钥
        private static readonly byte[] Key = Encoding.UTF8.GetBytes(keyString); // 8字节密钥
        private static readonly byte[] IV = Encoding.UTF8.GetBytes(keyString);  // 8字节初始化向量

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string data)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                MemoryStream ms = new MemoryStream();
                CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
                byte[] toEncrypt = Encoding.UTF8.GetBytes(data);
                encStream.Write(toEncrypt, 0, toEncrypt.Length);
                encStream.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string data)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] dataArray = Convert.FromBase64String(data);
                MemoryStream ms = new MemoryStream();
                CryptoStream decStream = new CryptoStream(ms, des.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
                decStream.Write(dataArray, 0, dataArray.Length);
                decStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
