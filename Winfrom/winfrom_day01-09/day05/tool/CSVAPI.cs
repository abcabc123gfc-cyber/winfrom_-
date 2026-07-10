using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV
{
    public class CSVApi
    {

        public static void SaveData(string name, string age,string sex)
        {

            string path = Directory.GetCurrentDirectory() + "\\Data";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //string fileName = path + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            string fileName = $"{path}\\{DateTime.Now.ToString("yyyy-MM-dd")}.csv";



            if (!File.Exists(fileName))
            {
                //创建文件流，传入文件路径，创建并写入
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                //Encoding.Default 编码格式 允许写入中文内容
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                //创建StringBuilder类型追加写入信息
                StringBuilder sb = new StringBuilder();
                sb.Append("姓名").Append(",").Append("年龄").Append(",").Append("性别");
                sw.WriteLine(sb);
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
            }


            using (StreamWriter sw2 = new StreamWriter(fileName, true, Encoding.Default))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(name).Append(",").Append(age).Append(",").Append(sex);
                sw2.WriteLine(sb);
            }

        }

        public static string ReadData()
        {
            string path = Directory.GetCurrentDirectory() + "\\Data";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //string fileName = path + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            string fileName = $"{path}\\{DateTime.Now.ToString("yyyy-MM-dd")}.csv";

            StreamReader sr = new StreamReader(fileName, Encoding.Default);
            string str = sr.ReadToEnd();
            return str;
        }
    }
}
