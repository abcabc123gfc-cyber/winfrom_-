using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace 反射
{
    internal class BabyStroller_App
    {
        //会从程序裕兴的地方 下 Animals 文件夹下 把第三方的插件加载进来
        //调用 Voice 方法 接受参数允许叫声的次数
        public static void Main(string[] args)
        {
            //当前程序的文件夹
            //Console.WriteLine(Environment.CurrentDirectory);
            //当前程序所在目录 中程序本身文件
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            var folder =  Path.Combine(Environment.CurrentDirectory, "/Animals");
            var files = Directory.GetFiles(folder);
            var animalsTypes = new List<Type>();
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                //var types = assembly.GetTypes();
                //animalsTypes.AddRange(types);
            }
        }
    }
}
