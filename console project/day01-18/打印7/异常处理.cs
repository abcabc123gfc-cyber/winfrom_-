using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 打印7
{
    internal class 异常处理
    {
        //语法: try{
        //      }catch(Exception e){
        //      }
        //finally{
        //      }

        public void Print()
        {
            try
            {
                //可能会出现异常的代码
                int a = 10;
                int b = 0;
                int c = a / b;
                Console.WriteLine(c);
            }
            catch (Exception e)
            {
                //捕获异常
                //只有出现异常的时候才会执行
                //thirow 抛出异常
                //一般情况下,都会把异常记录到日志中
                Console.WriteLine(e.Message);
            }
            finally
            {
                //释放资源
                //无论是否出现异常都会执行
                Console.WriteLine("finally");
            }
        }
    }
}
