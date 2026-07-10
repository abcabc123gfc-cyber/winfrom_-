
using DAL;
using Model;
using System;
using System.Collections.Generic;
using 示例;

namespace 示例
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
        public List<Logs> LogOperrate()
        {
            if (!IsGrad())
            {
            
                return null;
            }
           return logDAL.OutputLogOperate();
           
        }
        /// <summary>
        /// 查看交易日志
        /// </summary>
        public List<Logs> LogTrade()
        {
            if (!IsGrad())
            {             
                return null;
            }
            List<Logs> list = logDAL.OutputLogTrade(); ;
            if (list != null && list.Count > 0)
            {
                return list;
            }
            return null;

        }
  
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
    
    }

}
