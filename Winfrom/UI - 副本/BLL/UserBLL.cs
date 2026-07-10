using DAL;
using DALFactory;
using System;
using System.Collections.Generic;

namespace 示例
{
    public class UserBLL
    {
       
        public  readonly  IUser user = DataAccess.CreateDAL<IUser>("UserDAL");

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
               if(user==null) return false;
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
            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag )
            {             
                return false;
            }

           
           
            if (user.Delete(uid))
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
        /// 分页查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="page">当前页码</param>
        /// <param name="pageSize">每页显示条数，默认值：10</param>
        /// <param name="orderBy">排序列，默认值：创建时间倒序</param>
        /// <returns></returns>
        public List<User> GetAllUser(string where, int page, out int totalPage, int pageSize = 10, int Id = -1, string Name = "", string orderBy = "Id desc")
        {
            if (page < 1)
            {
                totalPage = 0;
                return null;
            }
            return user.Show(where, page, out totalPage, pageSize, orderBy, Name, Id);
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
        public bool logout()
        {
            if (user.LogOut())
            {

                return true;
            }
            else
            {
                return false;
            }


        }
        /// <summary>
        /// 数据备份
        /// </summary>
        public bool BlackDate()
        {
           
            if (user.DataBackup())
            {
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
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public bool OutputExcel()
        {
            if (user.OutputExcel())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
