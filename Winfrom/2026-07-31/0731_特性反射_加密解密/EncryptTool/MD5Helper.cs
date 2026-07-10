using System.Security.Cryptography;
using System.Text;

namespace EncryptTool
{
    public static class MD5Helper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string data)
        {
            // MD5它将任意长度的数据转换为128位的哈希值。
            using (var md5 = MD5.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(data);
                byte[] hashBytes = md5.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }
}
