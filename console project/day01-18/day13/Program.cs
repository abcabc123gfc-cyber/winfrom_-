using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace day13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Parent printer = new Parent();
            //printer.Test();
            //Parent child = new Child();
            //child.Test();
            //    int[] nums = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1, 6, 7, 8, 4, 2 };
            //    int index = Array.IndexOf(nums, 2, 4, 3);
            //    Console.WriteLine(index);
            //    int x = 5;
            //    Console.WriteLine(x++ + ++x);
            //    //A
            //    Console.WriteLine((char)65);
           
            SavingsAccount savingsAccount = new SavingsAccount("123456", "张三", 2000, 0.05);
            CheckingAccount checkingAccount = new CheckingAccount("123456", "张三", 1000, 2000);
            //取款 500
            checkingAccount.Withdraw(500);
            //存款 500
            checkingAccount.Deposit(500);
            //统计利息
            double b= savingsAccount.CalculateInterest(100);
            Console.WriteLine("利息为:{0}",b);
            //显示账户信息
            savingsAccount.DisplayAccountInfo(savingsAccount);
            checkingAccount.DisplayAccountInfo(checkingAccount);
            //向上转型
            Console.WriteLine("向上转型_调用的是子类重写父类的方法");
            Account account = savingsAccount;
            account.DisplayAccountInfo(account);
            account.CalculateInterest(12);
            //------------ 分割---------------
            Console.Clear();
            CreatLibrary();

            
        }
     

        /// <summary>
        /// 创建图书馆并生成默认数据
        /// </summary>
        /// <returns></returns>
        static void  CreatLibrary()
        {
            LibrarySystem library = new LibrarySystem();
          

             // 添加默认图书
             Book book1 = new Book("B001", "C#程序设计", 10, "张三", 9787111);
            Book book2 = new Book("B002", "深入理解CLR", 5, "李四", 9787112);
            Book book3 = new Book("B003", "设计模式", 8, "王五", 9787113);

            // 添加默认期刊s
            Magazine magazine1 = new Magazine("M001", "电脑报", 20, 202401);
            Magazine magazine2 = new Magazine("M002", "程序员杂志", 15, 202402);

            // 添加默认DVD
            DvD dvd1 = new DvD("D001", "肖申克的救赎", 3, "弗兰克·德拉邦特");
            DvD dvd2 = new DvD("D002", "阿甘正传", 4, "罗伯特·泽米吉斯");

         

            LibrarySystem library2 = new LibrarySystem();
            library2.AddCommon(book1);
            library2.AddCommon(book2);
            library2.AddCommon(book3);
            library2.AddCommon(dvd1);
            library2.AddCommon(dvd2);
            library2.AddCommon(magazine1);
            library2.AddCommon(magazine2);


            DateTime dateTime = DateTime.Now;
            BorrowRecord borrowRecord = new BorrowRecord( "B001", dateTime, 1);
            Menber user = new Menber( "张三",123);

            // 入库_用户列表
            library.Borrow(book1, user, borrowRecord);

            foreach (var item in LibrarySystem.listUsers)
            {

                foreach (var record in item.BorrowRecodes)
                {
                    record.Return();
                }
            }
        }


    }
}
