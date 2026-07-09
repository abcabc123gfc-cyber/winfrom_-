using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day10
{
    internal class hashTable
    {
        public void HashTableDemo()
        {
            //hashtable  hash 表 表示一系列与键和值组成的数据,使用键进行访问
            // hash 表中添加键值对,键必须是唯一的,数据类型不限制
            Hashtable hash = new Hashtable()
            {
                { "1", 1 } 
                
            };

            //添加数据
            hash.Add("2", 2);
            //获取: 变量名[键]
            Console.WriteLine(hash["1"]);

            //获取所有键的集合
            Console.WriteLine(hash.Keys);
            //获取所有值的集合
            Console.WriteLine(hash.Values);

            //使用goreach遍历
           /* foreach (var item in hash)
            {
                Console.WriteLine(item);
            }*/

            //删除, 根据键删除
            hash.Remove("2");
            //是否只读
            bool b=hash.IsReadOnly;
            
            //清空
            //hash.Clear();
            //判断键是否存在
            Console.WriteLine(hash.ContainsKey("1"));
            //判断值是否存在
            Console.WriteLine(hash.ContainsValue(1));
        }
    }
}
