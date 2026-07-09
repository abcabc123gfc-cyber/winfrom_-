using System;
using System.Runtime.InteropServices;
namespace day04
{
    internal class Function
    {
        /// <summary>
        /// 方法的声明 格式 参数值类型与引用类型 访问修饰符 调用
        /// </summary>
        public void TestFunction()
        {
            // 方法也叫做函数
            // 作用: 对逻辑相关的代码进行封装, 提高代码的复用性
            // 方法分为两个部分: 方法声明和方法体


            //方法的声明:
            {
                //方法声明包含:
                //访问修饰符
                //返回值类型
                //方法名
                //参数列表
                //方法体
                //方法声明结束  
            }
            //格式:
            /*访问修饰符 返回值类型 方法名(参数列表)
             {
                 //方法体
             }*/
            //权限修饰符
            {
                //访问修饰符: public private protected internal
                //public: 公有方法,可以在任何地方调用
                // private: 私有方法, 只能在本类中调用
                // protected: 保护方法, 只能在本类和子类中调用
                // internal: 内部方法, 只能在同一个程序集中调用
                // protected internal :可以在同一程序集内的任何地方访问，或者在任何地方的子类中访问
                // private protected: 私有保护方法, 只能在本类和子类中访问

            }
            #region 参数
            //参数: 形参 与实参
            //形参: 方法声明时定义的参数 写在方法声明中()
            //实参: 方法调用时传递的参数 写在方法调用中()

            // 方法参数列表分为两类:
            // 值参数: 值参数传递的是参数的值, 值参数在方法调用结束后, 会被销毁

            // 引用传递: 使用 ref out in 关键字修饰的参数
            //in 修饰的参数，在方法内部严禁被修改
            // 引用参数传递的是参数的地址, 引用参数在方法调用结束后, 不会被销毁
            // 值参数和引用参数的区别
            #endregion

            #region 方法的调用
            // 方法在调用的时候,璀璨输入的是表达式或者变量都会先计算出结果,再传递给方法
            //传入的是值本身,在方法内部会将值复制一份即使是实例对象
            //示例
            //Example(12, 5);
            //Example(12);

            Student Stu = new Student();
            Example(ref Stu);
            #endregion
        }

        /// <summary>
        /// 参数数组
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        static int MaxValue(params int[] nums)
        {
            int max = nums[0];
            foreach (int n in nums)
            {
                if (n > max) max = n;
            }
            return max;
        }
        /// <summary>
        /// 方法的返回值
        /// </summary>
        /// <returns></returns>
        public string TestFunctonRuturn()
        {
            #region 方法的返回值

            // 方法的返回值
            // 方法声明:
            // 访问修饰符 返回值类型 方法名(参数列表)
            // {
            //     //方法体
            //     return 返回值;
            // }
            // 方法调用:
            // 方法名(参数列表);
            // 方法调用的结果,会自动保存到一个变量中

            //方法声明类型 需要和返回值类型一致
            //无返回值 使用 return 结束方法
            #endregion


            return default;
        }
        
        public void Example(int a, int b)
        {
            Console.WriteLine(a + b);

        }
        public void Example(int a)
        {
            Console.WriteLine(a);
        }

        public void Example(Student stu)
        {
            //传递的是实例对象的副本,指向同一个堆内存块
            stu.Show();
        }
        public void Example(ref Student stu)
        {
            //传递的是实例对象的地址
            stu.Show();

            unsafe
            {
                // 固定对象，防止 GC 移动
                GCHandle handle = GCHandle.Alloc(stu, GCHandleType.Pinned);
                try
                {
                    IntPtr address = handle.AddrOfPinnedObject();
                    Console.WriteLine($"对象 stu 的当前内存地址: {address.ToInt64():X}");

                    // 注意：此时 stu 对象被固定，可以安全地传递地址给非托管代码
                }
                finally
                {
                    handle.Free(); // 必须释放，否则内存泄漏
                }
            }
        }
    }
    public class Student
    {
        public int Name { get; set; }
        public int Age { get; set; }

        public void Show()
        {
            Console.WriteLine(Name + " " + Age);
        }
    }
}
