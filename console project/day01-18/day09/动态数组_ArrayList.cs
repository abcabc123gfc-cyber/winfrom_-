using System;
using System.Collections;

namespace day09
{
    public class 动态数组_ArrayList
    {
        public void ArrrayListDemo()
        {
            

            //动态数组 就是不固定长度和数据类型的一个集合， 他的长度是根据存储数据的个数自动推断
            //动态数组 可以存储任意长度的数据u，他的长度会根据内容的增加或者减少而改变
            //格式：ArrayList list = new ArrayList();
            ArrayList list = new ArrayList();

            //获取长度
            //Console.WriteLine(list.Count);

            // 添加数据,向末尾添加数据,多类型
            list.Add(1);
            list.Add("字符");

            //清空集合
            //list.Clear();
            //插入数据. 参数1 插入位置的索引 参数2 添加的数据
            list.Insert(0, "插入数据");

            //将一个集合中的内容,插入到指定位置
            //insterRange() 参数1 插入位置的索引
            list.InsertRange(0, new string[] { "1", "2", "3" });

            //判断动态集合中是否包含某个元素,存在返回true,不存在返回false
            //Contains()
            Console.WriteLine(list.Contains("字符"));

            //从集合中截取对应位置的数据
            //GetRange() 参数1 截取的起始索引 参数2 截取的长度
            Console.WriteLine(list.GetRange(0, 2));

            //查询元素在集合中的索引,集合中没有这个元素则返回-1
            //IndexOf()
            Console.WriteLine(list.IndexOf("字符"));

            //查询元素在集合中的索引,集合中没有这个元素则返回-1
            //LastIndexOf()
            Console.WriteLine(list.LastIndexOf("字符"));

            //移除集合中的元素
            //Remove()
            list.Remove("字符");

            //移除集合中的元素
            //RemoveAt() 移除指定索引的元素
            list.RemoveAt(0);

            //移除集合中的元素
            //RemoveRange() 移除指定索引范围的元素
            list.RemoveRange(0, 2);

            //翻转集合
            //Reverse()
            list.Reverse();

            //排序
            //Sort()
            //list.Sort();

            //复制集合中到另一个集合中, 直接替换对应索引位置的元素
            //SetRange() 参数1 插入位置的索引, 参数2 插入的集合
            Console.WriteLine("-------");
            ArrayList list2 = new ArrayList();
            list2.Add("4");
            list2.Add("3");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("---替换插入之前--");
            list.SetRange(0, new string[] { "1", "2" });


            foreach (string i in list)
            {
                Console.WriteLine(i);
            }
        }
    }
}
