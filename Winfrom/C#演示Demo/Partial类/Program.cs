using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partial类
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbCOntext = new BlogEntities();
            //blog 类是更新, 不想更新可以写一个 Partial 类
            //注意名称和命名空间要一致
            var blog = dbCOntext.Blogs;
            foreach (var item in blog)
            {
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.Report());
            }
        }
    }
}
