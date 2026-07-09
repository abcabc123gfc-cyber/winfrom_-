using System;

namespace day05
{
    internal class 随机数
    {
        public void TestRandom()
        {
            /*
                随机数 Random 
                概念:计算机当中没有绝对随机数, 是算法生成的随机数队列
                场景: 抽奖 点名 注册账号 唯一标识符

                步骤: 1.创建Random对象 2.调用Next方法 3.获取随机数 

             */
            //创建Random对象,并制定种子
            {
                //区间: 左闭右开
                //种子 3 : [0,3)
                Random random = new Random(3);

                Console.WriteLine(random.Next());
            }

            //随机数种子介绍
            {
                //关于种子介绍: 随机队列是根据种子进行生成的, 种子相同, 队列相同, 随机数相同
                //在进行循环时,需要将随机数外置置, 避免每次循环都创建新的Random对象
                //原因: 避免随机队列重复
                //不指定种子: 通常会使用时间戳作为种子
            }
            //练习 随机验证码 4位
            {
                Random random = new Random();
                int R= random.Next(1000,9999);
                Console.WriteLine(R);
                //获取控制台背景颜色
                
                var bl= Console.BackgroundColor;
                
            }

            

        }
    }
}
