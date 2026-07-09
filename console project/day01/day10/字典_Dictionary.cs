using System;
using System.Collections.Generic;

namespace day10
{
    internal class 字典_Dictionary
    {
        public void DictionaryDemo()
        {
            //字典: 类似于List集合,只能存储固定的的数据,长度不固定
            //字典是由键值对组成,字典里面的数据是根据 键 去获取
            //键:值; 键不能重复,不能为null
            //格式: Dictionary<键的类型, 值的类型> dict = new Dictionary<键的类型, 值的类型>();

            Dictionary<int, string> keys = new Dictionary<int, string>()
            {
                { 0, "值1" },
                { 1,"值2"}

            };
            //添加数据
            keys.Add(2, "值3");
            //获取数据
            //Console.WriteLine(keys[0]);
            //修改
            keys[0] = "值1_修改";

            //获取字典长度
            int i = keys.Count;

            //删除数据, 删除的是键
            keys.Remove(0);
            //判断字典中是否包含某个键
            bool b = keys.ContainsKey(0);
            //判断字典中是否存在某个指定的值
            b=keys.ContainsValue("值1_修改");

            //用字典存储数据的作用: 一般英语信息的存储, 用字典存储数据,可以加快数据查询的速度

            //新闻数据的存储:
            /*
                日期:2021-07-01{
                    8:00 "新闻1",
                    9:00 "新闻2",
                    10:00 "新闻3"
                }
                
             */
          Dictionary<string,Dictionary<string,string>> pairs = new Dictionary<string, Dictionary<string, string>>();
            pairs.Add("2021-07-01", new Dictionary<string, string>()
            {
                { "8:00", "新闻1" },
                { "9:00", "新闻2" },
                { "10:00", "新闻3" }
            });
            string str = pairs["2021-07-01"]["8:00"];
            Console.WriteLine(str);

            //遍历 嵌套字典
            foreach (var item in pairs)
            {
                Console.WriteLine(item.Key);
                foreach (var item2 in item.Value)
                {
                    Console.WriteLine(item2);
                }
            }
        }
    }
}
