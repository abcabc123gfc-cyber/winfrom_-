
using System;

using BLL;

using 示例;
using 示例.BLL;


internal class DateTest
{
    public  void Date()
    {


        //测试数据
        UserDAL.listUser.Add(new User(1, "z", 1, 1, "管理员","123@qq.com","河南省","一个认真的人"));
        UserDAL.listUser.Add(new User(2, "z", 2, 2, "用户","123@qq.com", "河南省", "一个认真的人呢"));
        //测试数据
        示例.CustomerDAL.list.Add(new Customer(1, "1", "12345678912", 100));
        示例.CustomerDAL.list.Add(new Customer(2, "2", "12345678912", 100));
        //测试数据
        示例.CustomerDAL.Addresses.Add(new Address(1, "地址1"));
        示例.CustomerDAL.Addresses.Add(new Address(2, "地址1"));


        //启动
        //Start();
    }
    static void Start()
    {
        UserBLL userBLL = new UserBLL();
        CustomerBLL customerBLL = new CustomerBLL();
        LogBLL logBLL = new LogBLL();
        Console.Title = "用户管理系统";
        for (; ; )
        {
            Console.Clear();
            Console.WriteLine("1.登录 2.添加用户 3.更新用户 4.删除用户 5.添加客户 6.更新客户 7.删除客户 8.查看所有用户 9.查看所有客户 10.根据id查看用户 11. 根据id查看客户 12.注册 13.备份数据 14.操作日志管理 15.交易记录管理 16.全部日志 17.个人信息管理 18.退出登录 19.退出系统 ");
            Console.WriteLine(DateTime.UtcNow);
            string text = Console.ReadLine();

            switch (text)
            {
                case "1":
                    //userBLL.login();
                    break;
                case "2":
                    //userBLL.AddUser();
                    break;
                case "3":
                    //userBLL.UpdateUser();
                    break;
                case "4":
                    //userBLL.deleteUser();
                    break;
                case "5":
                    //customerBLL.Adddal();
                    break;
                case "6":
                    customerBLL.Updatedal();
                    break;
                case "7":
                    //customerBLL.Deletedal();
                    break;
                case "8":
                    userBLL.GetAllUser();
                    break;
                case "9":
                    customerBLL.GetAlldal();
                    break;
                case "10":
                    //userBLL.GetUser();
                    break;
                case "11":
                    //customerBLL.Getdal();
                    break;
                case "12":
                    //userBLL.register();
                    break;
                case "13":
                    userBLL.BlackDate();
                    break;
                case "14":
                    logBLL.LogOperrate();
                    break;
                case "15":
                    //logBLL.OutputLogTrade();
                    break;
                case "16":
                    logBLL.LogAll();
                    break;
                case "17":
                    userBLL.Personalinfo();
                    break;
                case "18":
                    //userBLL.logout();
                    break;
                case "19":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("输入错误");
                    break;
            }
            Console.ReadKey(true);

        }
    }
}
