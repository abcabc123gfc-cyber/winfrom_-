using IDAL;
using Model;
using System;
using System.Collections.Generic;

namespace CustomerDAL
{
    public class LogDAL : ILog
    {
        public static List<Log> LogsOperator = new List<Log>();
        #region 持久存储_用户退出时存储日志
        public static List<Log> LogsTrade = new List<Log>();
        public static List<Log> OperatorStatic = new List<Log>();
        public static string str11 = "";
        #endregion

        /// <summary>
        /// 输出所有日志
        /// </summary>
        public void LogAll()
        {
            if (LogsOperator.Count == 0 || LogsTrade.Count == 0)
            {
                return;
            }

            foreach (var log in LogsOperator)
            {
                log.ToString();
            }
            foreach (var log in LogsTrade)
            {
                log.LogOperate();
            }
        }
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool LogOperrate(Log log)
        {
            if (log == null)
            {
                return false;

            }
            int index = LogsOperator.FindIndex(x => x.Id == log.Id);
            if (index == -1)
            {
                LogsOperator.Add(log);
            }
            else
            {
                log.LogOut = LogsOperator[index].LogOut;
                log.MessageTemplate += LogsOperator[index].MessageTemplate + "    ";
                LogsOperator[index] = log;
                //当用户退出后存储日志
                if (LogsOperator[index].SingIn != null)
                {
                    OperatorStatic.Add(log);
                    str11 = (OperatorStatic[0].SingIn.ToString()) + OperatorStatic[0].MessageTemplate;
                    LogsOperator.Clear();
                }
            }
            return true;
        }
        /// <summary>
        /// 添加交易日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool LogTrade(Log log)
        {
            if (log == null)
            {
                return false;

            }
            LogsTrade.Add(log);

            return true;

        }
        /// <summary>
        /// 输出操作日志
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public List<Log> OutputLogOperate()
        {
            if (OperatorStatic.Count == 0)
            {
                return null;
            }

            return OperatorStatic;
        }
        // <summary>
        /// 输出交易日志
        /// </summary>
        public List<Log> OutputLogTrade()
        {
            if (LogsTrade.Count == 0)
            {
                return null;
            }

            return LogsTrade;
        }
    }
}
