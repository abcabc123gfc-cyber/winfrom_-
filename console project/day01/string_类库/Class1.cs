using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace string_类库
{
    public  class Practice
    {
        /// <summary>
        /// 拼接字符串
        /// </summary>
        /// <param name="strs"></param>
        public void Concat(params string[] strs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in strs)
            {
                sb.Append(item);
            }
            Console.WriteLine(sb);
        }
        /// <summary>
        /// 判断参数字符串是否出现在源字符串中  
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        public void Contains(string str1, string str2)
        {
            int i = 0;
            int j = 0;
            foreach (var item in str1)
            {
                j++;
                if (item == str2[i])
                {
                    i++;
                    if (i == str2.Length - 1)
                    {
                        IndexOF = j - i;
                        Console.WriteLine("包含");
                        return;
                    }
                    continue;
                }
                i = 0;
            }
            Console.WriteLine("不包含");
        }
        /// <summary>
        /// 复制字符串
        /// </summary>
        /// <param name="start"></param>
        /// <param name="char1"></param>
        /// <param name="Index"></param>
        /// <param name="str"></param>
        public void CopyTo(int start, char[] char1, int Index, string str)
        {
            int Num = char1.Length;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Index; i++)
            {
                sb.Append(str[i]);
            }

            for (int i = start; i < Num; i++)
            {
                sb.Append(char1[i]);

            }
            for (int i = Index; i < str.Length; i++)
            {
                sb.Append(str[i]);
            }
            Console.WriteLine(sb);
        }
        private int IndexOF = 0;

        /// <summary>
        /// 查找字符串 
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        public void IndexOf(string str1, string str2)
        {
            Contains(str1, str2);
            Console.WriteLine(IndexOF);
        }

        /// <summary>
        /// c从后向前查找字符 返回索引
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        public void LastIndexOf(string str1, string str2)
        {
            int i = 0;

            int Index = 0;
            for (int i1 = str1.Length - 1; i1 < str1.Length; i1--)
            {

                if (str1[i1] == str2[i])
                {
                    i++;
                    if (i == str2.Length - 1)
                    {

                        //-1 原因: i1 个字符 判断完成条件还未更新
                        Index = i1 + i - 1;
                        Console.WriteLine(Index);
                        return;
                    }

                }
            }
            Console.WriteLine("不包含");
        }


        /// <summary>
        /// 判断字符串是否以指定字符串开头
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        public void StatrtWith(string str1, string str2)
        {
            int i = 0;
            foreach (char item in str1)
            {
                if (item != str2[0])
                {
                    Console.WriteLine("false_StatrtWith");
                    return;
                }

                if (item == str2[i])
                {
                    i++;
                    if (i == str2.Length - 1)
                    {
                        Console.WriteLine(true);
                        return;
                    }

                }
            }
            Console.WriteLine("false_StatrtWith");
        }
        /// <summary>
        /// 判断字符串是否以指定字符串结尾
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        public void EndsWith(string str1, string str2)
        {
            int i1 = 0;
            for (int i = str1.Length - 1; i >= 0; i--)
            {
                if (str1[str1.Length - 1] != str2[str2.Length - 1])
                {
                    Console.WriteLine("false_EndsWith");
                    return;
                }

                if (str1[i] == str2[str2.Length - 1 - i1])
                {

                    if (i1 == str2.Length - 1)
                    {
                        Console.WriteLine(true);
                        return;
                    }
                    i1++;
                }

            }
            Console.WriteLine("false_EndsWith");
        }


        /// <summary>
        /// 判断字符串是否相等
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        public void Equals(string str1, string str2)
        {
            if (str1.Length != str2.Length)
            {
                Console.WriteLine("false_Equals");
                return;
            }
            else
            {
                Console.WriteLine("true_Equals");
            }
        }

        /// <summary>
        /// 插入字符串 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        public void Insert(int index, string str1, string str2)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < index; i++)
            {
                sb.Append(str1[i]);
            }
            sb.Append(str2);
            for (int i = index; i <= str1.Length - 1; i++)
            {
                sb.Append(str1[i]);
            }
            Console.WriteLine(sb);
        }

        /// <summary>
        /// 删除字符串
        /// </summary>
        public void Remove(int Start, int End, string str1)
        {
            if (Start > End || str1.Length <= End)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str1.Length; i++)
            {
                if (i >= Start && i <= End)
                {
                    continue;
                }
                sb.Append(str1[i]);
            }
            Console.WriteLine(sb);
        }

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        public void IsEmpty(string str)
        {
            if (str == null || str.Length == 0)
            {
                Console.WriteLine(true);
            }
        }

        // <summary>
        /// 替换字符串
        /// </summary>
        public void Replace(string str1, string str2, string str3)
        {
            int i1 = 0;
            bool flag = true;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] == str2[i1] && flag)
                {


                    if (i1 == str2.Length - 1)
                    {
                        flag = false;
                        sb.Append(str3);
                        continue;
                    }
                    i1++;
                    continue;
                }
                sb.Append(str1[i]);
            }
            Console.WriteLine(sb);

        }

        /// <summary>
        /// 转大写
        /// </summary>
        public void ToUpper(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 'a' && str[i] <= 'z')
                {
                    sb[i] = (char)(str[i] - 32);
                }
                else
                {
                    sb[i] = str[i];
                }
            }
            Console.WriteLine(sb);

        }
        /// <summary>
        /// 转小写
        /// </summary>
        public void ToLower(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 'A' && str[i] <= 'Z')
                {
                    sb[i] = (char)(str[i] + 32);
                }
                else
                {
                    sb[i] = str[i];
                }
            }
            Console.WriteLine(sb);
        }
    }
}
