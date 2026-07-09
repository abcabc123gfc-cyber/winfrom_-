using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day10
{
    internal class 排序列表SortedList
    {
        public void SortedListDemo()
        {
            //存储一系列按照键进行排序的键值对,可以通过 键/索引 进行访问

            //排序列表是数组和哈希表的组合,可以是使用键或索引进行访问各项的列表
            //如果使用索引去访问各项,那么它就是一个动态数据(ArrayList), 如果使用键去访问各项,那么它就是一个哈希表(hashtable)
            //集合中的各项总是按照值进行排序

            SortedList sl = new SortedList()
            {
                {1,"1" }
            };

            //通过键去访问 :变量名[键]
            Console.WriteLine(sl[1]);

            //通过索引去访问 :变量名[索引]
            //格式:变量名.GetByIndex(索引)
            //通过索引去访问,排序列表会根据 键值 进行排序
            Console.WriteLine(sl.GetByIndex(0));

            //长度
            Console.WriteLine(sl.Count);
            //获取值
            Console.WriteLine(sl.Values);
            //获取键集合
            Console.WriteLine(sl.Keys);

            //查看值是否存在
            Console.WriteLine(sl.ContainsValue("1"));
            //查看键是否存在
            Console.WriteLine(sl.ContainsKey(1));
            //清空
            sl.Clear();
            //根据指定的索引删除
            sl.RemoveAt(0);
            //删除指定键对应的值
            sl.Remove(1);
            //获取指定索引位置的键
            Console.WriteLine(sl.GetKey(0));
            //获取指定索引位置的值
            Console.WriteLine(sl.GetByIndex(0));
        }
    }
}
