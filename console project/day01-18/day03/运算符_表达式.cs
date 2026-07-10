using System;

namespace day03
{
    internal class 运算符_表达式
    {
        public void yunsun()
        {
            // +-* /
            Console.WriteLine(1 + 2);
            Console.WriteLine(2 - 3);
            Console.WriteLine(3 * 4);
            Console.WriteLine(4 / 5);

            //取余 % :  是余数
            Console.WriteLine(5 % 2);

            //0 不能作为 除数
            //Console.WriteLine(5 / 0);
            Console.WriteLine(5.0 / 0);

            //+ :  是连接符 和 运算符
            Console.WriteLine("hello" + "world");
            Console.WriteLine("hello" + 1);
            Console.WriteLine(1 + 1);

            #region 赋值运算符
            // =, +=, -=, *=, /=, %=
            // = 赋值
            int a = 10;
            // = 号 左边是变量 右边是值
            //+= 运算符
            a += 5;
            //Console.WriteLine("使用方法调用");
            //Console.WriteLine(a+"_a");
            //Console.WriteLine(a +yunsuanfu());

            #endregion
            {
                //Console.WriteLine("数组遍历");
                //int[] num1 = new[] { 1, 2, 3, 4 };
                //int[][] num = new int[][] { num1 };

                //foreach (var item in num)
                //{

                //    foreach (var item1 in item)
                //    {
                //        Console.WriteLine(item1);
                //    }
                //}

            }
            #region ++ 自增 -- 自减
            //++ 自增 -- 自减
            a = 10;
            //先输出后增
            Console.WriteLine(a++);
            //先增后输出
            Console.WriteLine(++a);
            //先输出后减
            Console.WriteLine(a--);
            //先减后输出
            Console.WriteLine(--a);


            #endregion
        }
        public void BiJiao()
        {
            //> < >= <= == !=
            //关系运算符主要用于比较多个值之间的关系, 会得到一个bool值
            int aa = 3;
            int bb = 5;
            Console.WriteLine($"aa_{aa},bb_{5},之间的关系");
            //Console.WriteLine(aa > bb);
            //Console.WriteLine(aa < bb);
            //Console.WriteLine(aa >= bb);
            //Console.WriteLine(aa <= bb);
            //Console.WriteLine(aa == bb);
            //Console.WriteLine(aa != bb);

            // 三目表达式
            Console.WriteLine(aa > bb ? "aa大于bb" : "aa小于bb");
            #region 逻辑运算符
            // 逻辑与 & 逻辑或 | 逻辑非 !
            Console.WriteLine("逻辑与或非");
            Console.WriteLine(aa > bb & aa < 10);
            Console.WriteLine(aa > bb | aa < 10);
            Console.WriteLine(!(aa > bb));
            // 逻辑运算符的优先级比关系运算符高

            //短路逻辑运算符
            // && 逻辑与 || 逻辑或 
            // && 如果当前的条件为false,则不会执行后面的条件
            // || 如果当前的条件为true,则不会执行后面的条件
            Console.WriteLine(aa > bb && aa < 10);
            Console.WriteLine(aa > bb || aa < 10);
            #endregion
        }
        /// <summary>
        /// 分支结构
        /// </summary>
        public void FenZhi()
        {
            //1 单分支语法结构: if(表达式){
            //   逻辑代码
            //}
            if (true)
            {
                Console.WriteLine("单分支");
            }
            // 2 双分支语法结构: if(表达式){
            //   逻辑代码
            //}else{
            //   逻辑代码
            //}
            if (true)
            {
                Console.WriteLine("多分支");
            }
            else
            {
                Console.WriteLine("多分支");
            }
            // 3 多分支语法结构: if(表达式){
            //   逻辑代码
            //}else if(表达式){
            //   逻辑代码
            //}else{
            //   逻辑代码
            //}

            if (1 > 3)
            {
                Console.WriteLine("1>3");
            }
            else if (2 > 3)
            {
                Console.WriteLine("2>3");
            }
            else
            {
                Console.WriteLine("3>3");
            }

            //4 分支嵌套
            /*if (表达式)
            {
                if (表达式)
                {
                    Console.WriteLine("分支嵌套");
                }
            }
            */
            {
                //练习
                int x = 20, y = 30, z = 23;
                Console.WriteLine(x > y ? x > z ? x : z : y > z ? y : z);
            }
        }


        /// <summary>
        /// 循环结构 go to
        /// </summary>
        public void GoTo()
        {
            //goto 语句 可循环执行代码
            //1 在合适的位置打一个标签 
            //2 使用goto语句跳转到标签
            {
                // 方式:  标签名加冒号 如 '  lable:   '
                // 跳转到标签 
                //方式:  goto 标签名
            }
            /*
                    inputSex:
                        Console.WriteLine("输入性别");
                        //string sex = Console.ReadLine();
                        if (sex == "男")
                        {
                            Console.WriteLine("你是男生");
                        }
                        else
                        {
                            goto inputSex;
                        }
            */
            {
                //1. 小练习

                Console.WriteLine("请输入用户名");
                int i = 0;

            login:
                string name = Console.ReadLine();


                if (i >= 2)
                {
                    Console.WriteLine("输入错误次数过多");
                    return;
                }
                else
                {

                    if (name != "admin")
                    {

                        Console.WriteLine("重新输入用户名");
                        i++;
                        goto login;

                    }
                    Console.WriteLine("请输入密码");
                password:
                    int pwd = Convert.ToInt32(Console.ReadLine());
                    if (pwd != 123456)
                    {
                        Console.WriteLine("冲洗输入密码");
                        i++;
                        goto password;
                    }

                    Console.WriteLine("登录成功");

                }
            }
        }
    }
    class Program11
    {

    }
}
