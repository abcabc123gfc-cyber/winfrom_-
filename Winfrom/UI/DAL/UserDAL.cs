using CustomerDAL;
using Model;
using System;
using System.Collections.Generic;

namespace 示例
{
    // Token: 0x0200000A RID: 10
    public class UserDAL : IUser
    {
        // Token: 0x0600002C RID: 44 RVA: 0x00002760 File Offset: 0x00000960
        public bool Add(User user, bool flag1)
        {
            if (!flag1)
            {

                string messageTemplate = $"用户{GerUser(userId).Name}添加用户{user.Name}";
                logDAL.LogOperrate(new Log(userId, GerUser(userId).Name, DateTime.Now, null, messageTemplate));
            }


            foreach (User item in UserDAL.listUser)
            {
                bool flag = item.Id == user.Id;
                if (flag)
                {

                    return false;
                }
            }
            UserDAL.listUser.Add(user);

            return true;
        }

        // Token: 0x0600002D RID: 45 RVA: 0x00002804 File Offset: 0x00000A04
        public bool Delete(int id)
        {

            string messageTemplate = $"用户{GerUser(userId).Name}删除用户{id}";
            logDAL.LogOperrate(new Log(userId, GerUser(userId).Name, DateTime.Now, null, messageTemplate));
            foreach (User item in UserDAL.listUser)
            {
                bool flag = item.Id == id;
                if (flag)
                {
                    UserDAL.listUser.Remove(item);
                    return true;
                }
            }
            return false;
        }

        //根据名字查询
        public List<User> GetName(string name)
        {
            List<User> list = new List<User>();
            foreach (User item in UserDAL.listUser)
            {
                bool flag = item.Name.Contains(name);
                if (flag)
                {

                    list.Add(item);
                }
            }
            return list;
        }



        // Token: 0x0600002E RID: 46 RVA: 0x00002878 File Offset: 0x00000A78
        public User GerUser(int id)
        {
            if (!flag)
            {

                return null;
            }
            foreach (User item in UserDAL.listUser)
            {
                bool flag = item.Id == id;
                if (flag)
                {

                    return item;
                }
            }
            return null;
        }

        // Token: 0x0600002F RID: 47 RVA: 0x000028E0 File Offset: 0x00000AE0
        public bool Login(int account, int password)
        {


            foreach (User item in UserDAL.listUser)
            {
                bool flag3 = (item.Password == password) && (item.Account == account);
                if (flag3)
                {
                    userId = item.Id;
                    //log记录
                    string messageTemplate = $"用户{item.Name}登录成功";
                    logDAL.LogOperrate(new Log(item.Id, item.Name, DateTime.Now, null, messageTemplate));
                    Console.WriteLine(userId);
                    flag = true;
                    return true;
                }
            }

            return flag;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update(User user)
        {
            try
            {

                string messageTemplate = $"用户{GerUser(userId).Name}修改用户信息";
                logDAL.LogOperrate(new Log(userId, GerUser(userId).Name, DateTime.Now, null, messageTemplate));


                int temp = listUser.FindIndex(item => item.Id == user.Id);
                listUser[temp] = user;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        // Token: 0x06000031 RID: 49 RVA: 0x00002A18 File Offset: 0x00000C18
        public void Show()
        {
            if (!flag)
            {
                Console.WriteLine("未登录");
                return;
            }
            string messageTemplate = $"用户{GerUser(userId).Name}查看所有用户信息";
            logDAL.LogOperrate(new Log(userId, GerUser(userId).Name, DateTime.Now, null, messageTemplate));
            foreach (User item in UserDAL.listUser)
            {
                item.ToString();
            }
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Register(User user)
        {
            if (Add(user, false))
            {
                string messageTemplate = $"用户{user.Name}注册成功";

                logDAL.LogOperrate(new Log(user.Id, user.Name, DateTime.Now, null, messageTemplate));
                return true;
            }
            else
            {
                return false;

            }

        }

        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User IsGrade(int id)
        {
            return GerUser(id);

        }
        /// <summary>
        /// 数据备份
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DataBackup()
        {
            string messageTemplate = $"用户{GerUser(userId).Name}数据备份成功";
            logDAL.LogOperrate(new Log(userId, GerUser(userId).Name, DateTime.Now, null, messageTemplate));

            blackupUser = listUser;
            blackupCustom = CustomerDAL.list;
            blackListAddress = CustomerDAL.Addresses;
            return true;
        }
        /// <summary>
        /// 登出 注:Log类中LogOut为登录时间
        /// </summary>
        /// <returns></returns>
        public bool LogOut()
        {
            if (flag)
            {
                string messageTemplate = $"用户{GerUser(userId).Name}登出";
                logDAL.LogOperrate(new Log(userId, GerUser(userId).Name, DateTime.Now, DateTime.Now, messageTemplate));
                UserDAL.flag = false;
                userId = -1;
                return true;
            }
            else
            {
                return false;
            }


        }
        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public User Personalinfo()
        {
            try
            {

                return GerUser(userId);

            }
            catch (Exception)
            {


                return null;
            }
        }




        #region 实例化日志
        LogDAL logDAL = new LogDAL();
        #endregion
        #region 静态变量
        // Token: 0x0400000E RID: 14
        public static List<User> listUser = new List<User>();
        public static List<User> blackupUser = new List<User>();
        public static List<Customer> blackupCustom = new List<Customer>();
        public static List<Address> blackListAddress = new List<Address>();
        // Token: 0x0400000F RID: 15
        public static bool flag = false;
        public static int userId = -1;
        #endregion
    }
}
