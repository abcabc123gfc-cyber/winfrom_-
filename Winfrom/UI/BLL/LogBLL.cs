using CustomerDAL;
using Model;
using System;
using System.Collections.Generic;
using 示例;

namespace BLL
{
    public class LogBLL
    {
        LogDAL logDAL = new LogDAL();
        /// <summary>
        /// 查看所有日志
        /// </summary>
        public void LogAll()
        {
            logDAL.LogAll();
        }
        /// <summary>
        /// 查看操作日志
        /// </summary>
        public List<Log> LogOperrate()
        {
            if (!IsGrad())
            {
                Console.WriteLine("权限不足或未登录");
                return null;
            }
            List<Log> list = logDAL.OutputLogOperate();
            //logDAL.OutputLogOperate();
            try
            {

                if (list != null && list.Count > 0)
                {
                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// 查看交易日志
        /// </summary>
        public List<Log> LogTrade()
        {
            if (!IsGrad())
            {
                Console.WriteLine("权限不足或未登录");
                return null;
            }
            List<Log> list = logDAL.OutputLogTrade(); ;
            if (list != null && list.Count > 0)
            {
                return list;
            }
            return null;

        }
        #region 判断权限
        private bool IsGrad()
        {
            UserDAL user = new UserDAL();
            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag)
            {
                Console.WriteLine(user.IsGrade(UserDAL.userId).Grade);
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }

}
