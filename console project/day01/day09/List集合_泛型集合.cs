using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day09
{
    internal class List集合_泛型集合
    {
        public void ListDemo()
        {
            //List集合 他也是一个集合,只不过只能存储相同的数据类型,长度可改变
            //通过索引访问集合中的数据
            //格式：List<数据类型> list = new List<数据类型>();
            List<string> list = new List<string>(); 
            list.Add("张三");
            //获取集合中的数据
            Console.WriteLine( list[0]);
            //修改
            list[0] = "lisi";

            //获取长度
            //Console.WriteLine(list.Count);

            //添加数据
            //参数1 添加的数据,不会被替换
            list.AddRange(new string[] { "1", "2", "3" });

            list.RemoveAll(x => x == "1");

        }
    }
}

