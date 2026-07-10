using System;
using System.Collections.Generic;

namespace 示例.BLL
{
    public class UserBLL
    {
        UserDAL user = new UserDAL();


        /// <summary>
        /// 登录
        /// </summary>
        public bool Login(int username, int password)
        {
            try
            {

                // 输入用户名
                //username = int.Parse(Console.ReadLine());
                //// 输入密码
                //password = int.Parse(Console.ReadLine());

                if (user.Login(username, password))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        public bool AddUser(User user1)
        {

            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag)
            {

                return false;
            }


            if (user1 == null)
            {

                return false;
            }

            if (user.Add(user1, true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        public bool UpdateUser(User u)
        {
            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag)
            {
                Console.WriteLine("权限不足或未登录");
                return false;
            }
            if (user.Update(u))
            {
                return true;

            }
            else
            {
                return false;
            }


        }
        /// <summary>
        /// 删除用户
        /// </summary>
        public bool deleteUser(int uid)
        {
            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag && uid != UserDAL.userId)
            {
                Console.WriteLine("权限不足或未登录");
                return false;
            }

            Console.WriteLine(value: "输入要删除用户的id");
            int id = int.Parse(Console.ReadLine());
            if (user.Delete(id))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 根据用户id查询用户信息
        /// </summary>
        public User GetUser(int id)
        {

            User u = user.GerUser(id);
            if (u != null)
            {
                return u;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 查询用户信息,根据name
        /// </summary>
        public List<User> GetUser(string name)
        {
            List<User> u = user.GetName(name);
            if (u != null)
            {
                return u;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 显示所有用户信息
        /// </summary>
        public void GetAllUser()
        {
            if (!UserDAL.flag)
            {
                Console.WriteLine("请先登录");
                return;
            }
            user.Show();
        }
        /// <summary>
        /// 注册
        /// </summary>
        public bool register(User u)
        {
            if (user.Register(u))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 退出登录
        /// </summary>
        public void logout()
        {
            if (user.LogOut())
            {

                Console.WriteLine("退出登录成功");
            }
            else
            {
                Console.WriteLine("已在退出状态");
            }


        }
        /// <summary>
        /// 数据备份
        /// </summary>
        public void BlackDate()
        {
            if (!UserDAL.flag)
            {
                Console.WriteLine("请先登录");
                return;
            }
            if (user.DataBackup())
            {
                Console.WriteLine("备份成功");
            }
        }
        /// <summary>
        /// 个人信息
        /// </summary>
        public User Personalinfo()
        {
            if (!UserDAL.flag)
            {
                Console.WriteLine("请先登录");
                return null;
            }
            User u = user.Personalinfo();
            if (u != null)
            {
                return u;
            }
            return null;
        }



    }
}
