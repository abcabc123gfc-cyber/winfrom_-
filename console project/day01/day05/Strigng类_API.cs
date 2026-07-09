using System;

namespace day05
{
    internal class Strigng类_API
    {
        public void TestString()
        {
            // 字符串属性
            string str = "hello world";
            // 1 length 可以获取字符串长度
            Console.WriteLine(str.Length);
            // 2 通过索引获取某一个字符 从0开始
            Console.WriteLine(str[0]);

            #region 字符串方法

            str = "hello world";
            string str1 = "hello world";

            //1 拼接字符串
            string.Concat(str, str1);
            //2 判断参数字符串是否出现在源字符串中    
            str.Contains(str1);
            //3 复制字符串
            char[] chars = new char[str.Length];
            str.CopyTo(0, chars, 0, str.Length);
            //4 判断源字符是否以某字符串开头
            str.StartsWith(str1);
            //5 判断源字符是否以某字符串结尾
            str.EndsWith(str1);
            //6 判断某字符串是否相等
            str.Equals(str1);
            //7 从前先后查询字符串出现的位置,如果没有则返回-1
            str.IndexOf(str1);
            //8 从后向前查询字符串出现的位置,如果没有则返回-1
            str.LastIndexOf(str1);
            // 忽略大小写进行查询,从前向后查询
            str.IndexOf("C", StringComparison.OrdinalIgnoreCase);
            // 忽略大小写进行查询,从后向前查询
            str.LastIndexOf("C", StringComparison.OrdinalIgnoreCase);
            //从指定位置查询,忽略大小写
            str.IndexOf("C", 0, StringComparison.OrdinalIgnoreCase);

            //9 从指定位置插入字符串
            str.Insert(0, str1);

            //10 删除指定位置的字符
            str.Remove(0, 1);
            //11 判断是否为空
            bool bs = string.IsNullOrEmpty(str1);
            //12 替换字符串
            str.Replace("hello", "hi");
            //13 将小写转大写
            str.ToUpper();
            //14 将大写转小写
            str.ToLower();

            //15 截取字符串
            str.Substring(0, 5);

            //字符串格式化
            Console.WriteLine(string.Format("{0} {1}", "hello", "world"));

            //  :Cn :以货币的格式并显示,并保留n位小数
            Console.WriteLine(string.Format("{0:C2}", 123.456));
            //可以对时间进行格式化操作处理,用法与 tostring() 相同
            Console.WriteLine(string.Format("{0:D}", DateTime.Now));


            //补充
            str = "abcde133";
            //1 indexofAny() 从前向后查询,返回字符串中首次出现的字符数组中任意字符的索引,如果不存在则返回-1
            str.IndexOfAny(new char[] { 'a', 'b', 'c' });
            //2 lastIndexOfAny() 从后向前查询,返回字符串中首次出现的字符数组中任意字符的索引,如果不存在则返回-1
            str.LastIndexOfAny(new char[] { 'a', 'b', 'c' });
            //3 Join() 将字符数组或者字符串数组 按照给定的字符串连接起来
            string.Join("-", new string[] { "hello", "world" });
            //4 PadLeft() 左填充 参数1:填充的字符串长度,参数2:填充的字符
            str.PadLeft(10, '0');
            //5 PadRight() 右填充 参数1:填充的字符串长度,参数2:填充的字符
            str.PadRight(10, '0');
            //6 Trim() 去掉字符串首尾的空格
            str.Trim();
            //7 TrimStart() 去掉字符串开头的空格
            str.TrimStart();
            //8 TrimEnd() 去掉字符串末尾的空格
            str.TrimEnd();
            //9 Split() 将字符串按照指定的字符串进行切割,返回字符串数组
            str.Split('-');
            //10 ToCharArray() 将字符串转换成字符数组
            str.ToCharArray();
           
            #endregion
        }
    }
}
