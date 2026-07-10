using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Logs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Operator { get; set; }
        /// <summary>
        /// 登录
        /// </summary>
        public DateTime LogOut { get; set; } 
        /// <summary>
        /// 登出
        /// </summary>
        public DateTime? SingIn { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime TradeDate { get; set;}
       
        public string MessageTemplate { get; set; }
        public override string ToString()
        {
            string logoutStr = LogOut.ToString("yyyy-MM-dd HH:mm:ss");
            string singInStr = SingIn?.ToString("yyyy-MM-dd HH:mm:ss") ?? "（未登出）";
            string str = String.Format("用户编号：{0}，用户名：{1}，操作时间：{2}，操作结束日期：{3}，操作内容：{4}",
                                        Id, Name, logoutStr, singInStr, MessageTemplate);



            //---------------
            //string str = $"用户编号：{Id}，用户名：{Name}，操作时间：{LogOut}，操作结束日期: {SingIn}，操作内容：{MessageTemplate}";
            //String str= String.Format("用户编号：{0}，用户名：{1}，操作时间：{2}，操作结束日期: {3},操作内容：{4}", Id, Name, LogOut, SingIn, MessageTemplate);
            return str;
        }
        public string LogOperate()
        {

            string singInStr = TradeDate.ToString("yyyy-MM-dd HH:mm:ss") ?? "（未登出）";
            StringBuilder sr = new StringBuilder($"操作员：{Operator}，客户id: {Id}，客户姓名:{Name}，交易时间：{singInStr}，操作内容：{MessageTemplate}");
            return sr.ToString();

        }
        /// <summary>
        /// 操作日志管理
        /// </summary>
        /// <param name="id">登录人编号</param>
        /// <param name="name">姓名</param>
        /// <param name="logout">登录时间</param>
        /// <param name="singin">退出登录时间</param>
        /// <param name="messageTemplate">操作内容</param>
        public Logs(int id, string name, DateTime logout, DateTime? singin, string messageTemplate)
        {
            Id = id;
            Name = name;
            LogOut = logout;
            SingIn = singin;
            MessageTemplate = messageTemplate;
          

        }
        /// <summary>
        /// 交易记录管理
        /// </summary>
        /// <param name="id">客户id</param>
        /// <param name="name">客户姓名</param>
        /// <param name="TradeDate">交易时间</param>
        /// <param name="messageTemplate">交易内容</param>
        /// <param name="treadClose">交易结束时间</param>
        /// <param name="Operator">操作员姓名</param>
        public Logs(int id, string Operator, string CustomerName,DateTime TradeDate ,string messageTemplate)
        {
            Id = id;
            Name = CustomerName;
            this.Operator = Operator;
            this.TradeDate = TradeDate;
         
            MessageTemplate = messageTemplate;

        }

        public Logs(int Id, string name, string Operator,DateTime logout,DateTime SinIn,DateTime TradeDate, string messageTemplate)
        {
            this.Id = Id;
            this.Name = name;
            this.Operator = Operator;
            this.LogOut = logout;
            this.SingIn = SinIn;
            this.TradeDate = TradeDate;
            this.MessageTemplate = messageTemplate;


        }
        public Logs()
        {

        }
      
    }
}
