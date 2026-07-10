using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptTool
{
    public static class AESHelper
    {
        private static readonly string keyString = ConfigurationManager.AppSettings["AESKEY"];
        // 它使用128、192或256位密钥
        private static readonly byte[] Key = Encoding.UTF8.GetBytes(keyString); // 16字节密钥
        private static readonly byte[] IV = Encoding.UTF8.GetBytes(keyString);  // 16字节初始化向量

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string data)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(data);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string data)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] bytes = Convert.FromBase64String(data);

                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
