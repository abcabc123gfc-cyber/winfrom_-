using IDAL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DAL
{
    public class LogDAL : ILog
    {
        public static List<Logs> LogsOperator = new List<Logs>();

        public static List<Logs> LogsTrade { get; set; }
        public static List<Logs> OperatorStatic = new List<Logs>();


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
                //log.LogOperate();
            }
        }
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool LogOperrate(Logs log)
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

                if (LogsOperator[index].SingIn != null)
                {
                    LogDAL_Operator(log);

                    LogsOperator.RemoveAt(index);
                }
            }
            return true;
        }
        /// <summary>
        /// 日志写入文件_操作日志
        /// </summary>
        /// <param name="log"></param>
        private void LogDAL_Operator(Logs log)
        {
            string str = GetDebugDirectory();
            string fileDate = "log_" + "操作日志_" + ".json";
            string pathFile = Path.Combine(str, fileDate);
            if (!File.Exists(pathFile))
            {
                using (File.Create(pathFile)) { }
            }
            string json = JsonConvert.SerializeObject(log, Formatting.Indented) + "\n";
            File.AppendAllText(pathFile, json);

        }


        /// <summary>
        /// 异常日志写入文件_操作日志
        /// </summary>
        /// <param name="OperationClass">操作的类</param>
        /// <param name="OperateAction">操作的行为</param>
        /// <param name="message">错误信息</param>
        /// <returns>bool 判断是否添加成功</returns>
        public bool LogOperateException(string OperationClass, string OperateAction, Exception message)
        {
            if (message == null || string.IsNullOrEmpty(OperateAction) || string.IsNullOrEmpty(OperationClass)) return false;
            string str = GetDebugDirectory();
            string fileDate = "log_" + "操作异常日志_" + ".json";
            string pathFile = Path.Combine(str, fileDate);
            if (!File.Exists(pathFile))
            {
                using (File.Create(pathFile)) { }
            }
            string join = "操作类:" + OperationClass + "    " + "操作方法:" + OperateAction + "    " + "操作时间:" + DateTime.Now + "    " + "操作结果:" + message;
            string json = JsonConvert.SerializeObject(join, Formatting.Indented) + "\n";
            File.AppendAllText(pathFile, json);
            return true;
        }
        /// <summary>
        /// 添加交易日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool LogTrade(Logs log)
        {
            if (log == null)
            {
                return false;

            }
            LogDAL_Trade(log);

            return true;

        }
        /// <summary>
        /// 日志写入文件_交易日志
        /// </summary>
        /// <param name="log"></param>
        private void LogDAL_Trade(Logs log)
        {
            //" + DateTime.Now.ToString("yyyy - MM - dd") + ".txt"

            //string str = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//log";
            string str = GetDebugDirectory();
            string fileDate = "log_" + "交易日志_" + ".json";
            string pathFile = Path.Combine(str, fileDate);
            if (!File.Exists(pathFile))
            {
                using (File.Create(pathFile)) { }
            }
            string json = JsonConvert.SerializeObject(log, Formatting.Indented) + "\n";
            File.AppendAllText(pathFile, json);
        }
        #region  返回日志存储地址
        private  string GetDebugDirectory()
        {
            string str = AppDomain.CurrentDomain.BaseDirectory + "//log";
            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
            }
            return str;
        }
        #endregion
        /// <summary>
        /// 输出操作日志
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public List<Logs> OutputLogOperate()
        {
            return ParseLogOperate();
        }
        // <summary>
        /// 输出交易日志
        /// </summary>
        public List<Logs> OutputLogTrade()
        {

            return ParseLogTrade();
        }
        #region 数据解析
        private List<Logs> ParseLogOperate()
        {
            List<Logs> logList = new List<Logs>();
            string str = GetDebugDirectory();
            string fileDate = "log_" + "操作日志_" + ".json";
            string pathFile = Path.Combine(str, fileDate);
            if (!File.Exists(pathFile))
            {
                return null;
            }

            using (FileStream fileStream = new FileStream(pathFile, FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
            //Newtonsoft.Json 的 JsonTextReader 提供了 SupportMultipleContent 属性，可直接流式读取多个顶级 JSON 对象：
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                jsonTextReader.SupportMultipleContent = true;
                var serializer = new JsonSerializer();
                while (jsonTextReader.Read())
                {
                    if (jsonTextReader.TokenType == JsonToken.StartObject)
                    {
                        Logs log = serializer.Deserialize<Logs>(jsonTextReader);
                        logList.Add(log);

                    }
                }
            }
            return logList;
        }
        private List<Logs> ParseLogTrade()
        {
            List<Logs> logList = new List<Logs>();
            string str = GetDebugDirectory();
            string fileDate = "log_" + "交易日志_" + ".json";
            string pathFile = Path.Combine(str, fileDate);
            if (!File.Exists(pathFile))
            {
                return null;
            }

            using (FileStream fileStream = new FileStream(pathFile, FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
            //Newtonsoft.Json 的 JsonTextReader 提供了 SupportMultipleContent 属性，可直接流式读取多个顶级 JSON 对象：
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                jsonTextReader.SupportMultipleContent = true;
                var serializer = new JsonSerializer();
                while (jsonTextReader.Read())
                {
                    if (jsonTextReader.TokenType == JsonToken.StartObject)
                    {
                        Logs log = serializer.Deserialize<Logs>(jsonTextReader);
                        logList.Add(log);
                    }
                }
            }


            return logList;
        }

        #endregion
    }
}
