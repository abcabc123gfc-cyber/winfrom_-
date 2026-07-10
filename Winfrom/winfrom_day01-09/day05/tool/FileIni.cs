using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace _08_INi配置文件.Tool
{
    public class FileIni
    {

        //读取 App.config文件中的数据

        //1.右键添加引用  System.Configuration
        //2.using System.Configuration

     private static  string  filePath=     ConfigurationManager.AppSettings["IniFilePath"].ToString();

        //复制的----------------------------------------
        /// <summary>
        /// 读取INI文件中指定的Key的值
        /// </summary>
        /// <param name="lpAppName">节点名称。如果为null,则读取INI中所有节点名称,每个节点名称之间用\0分隔</param>
        /// <param name="lpKeyName">Key名称。如果为null,则读取INI中指定节点中的所有KEY,每个KEY之间用\0分隔</param>
        /// <param name="lpDefault">读取失败时的默认值</param>
        /// <param name="lpReturnedString">读取的内容缓冲区，读取之后，多余的地方使用\0填充</param>
        /// <param name="nSize">内容缓冲区的长度</param>
        /// <param name="lpFileName">INI文件名</param>
        /// <returns>实际读取到的长度</returns>


        //DllImport 特性  用来调用动态链接库
        //kernel32.dll 动态连接库文件 就是一个程序集 window操作系统的内核文件(自带的) 用来进行读写操作
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, uint nSize, string lpFileName);




        /// <summary>
        /// 将指定的键和值写到指定的节点，如果已经存在则替换
        /// </summary>
        /// <param name="lpAppName">节点名称</param>
        /// <param name="lpKeyName">键名称。如果为null，则删除指定的节点及其所有的项目</param>
        /// <param name="lpString">值内容。如果为null，则删除指定节点中指定的键。</param>
        /// <param name="lpFileName">INI文件</param>
        /// <returns>操作是否成功</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);



        // ----------------------------------------

        //自己封装写入的方法
        public static bool Write(string section,string key,string value)
        {
            Console.WriteLine(filePath);
          return  WritePrivateProfileString(section, key, value, filePath);
        }

        //public static string Read(string section,string key)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    GetPrivateProfileString(section, key, "", sb, 255, filePath);
        //    Console.WriteLine(sb + "---");
        //    return sb.ToString();
        //}

        //public static string Read(string section,string key)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    GetPrivateProfileString(section, key, "", sb, 255, filePath);
        //    Console.WriteLine(sb + "---");
        //    return sb.ToString();
        //}

        public static string Read(string section, string key)
        {
            String str = new String(new char[255]);
            GetPrivateProfileString(section, key, "", str, 255, filePath);
            return str;
        }

    }
}
