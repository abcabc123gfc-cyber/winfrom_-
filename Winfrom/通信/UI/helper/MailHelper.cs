using System.Net;
using System.Net.Mail;

namespace modbus.helper
{
    public class MailHelper
    {
        /// <summary>
        /// 发送邮件的方法
        /// </summary>
        public static void SendMail(string mailTo, string mailTitle, string mailContent)
        {
            string stmpServer = "smtp.qq.com";//邮件服务器地址
            string MailAccount = "3244331389@qq.com";//发送人邮箱账号
            string pwd = "okefrnfuhlxjcaed";//发送人邮箱授权码

            //声明邮件服务
            //邮件服务端, 可以借助类实现发送邮件的功能
            SmtpClient smtp = new SmtpClient();
            //通过网络发送到smtp服务器
            smtp.DeliveryMethod=SmtpDeliveryMethod.Network;
            //设置邮件服务器地址
            smtp.Host = stmpServer;
            //使用安全加密连接
            smtp.EnableSsl=true;
            //使用默认平局,不喝请求的凭据相关联
            smtp.UseDefaultCredentials = true;
            //设置账号授权码 / 密码
            smtp.Credentials = new NetworkCredential(MailAccount, "snlmaimawtuochbj");
         
            //邮件消息
            //实例化邮件信息实体
            // 发送人 接收人
            MailMessage mail = new MailMessage(MailAccount, mailTo);
            //邮件内容
            mail.Subject = mailTitle;
            //邮件内容
            mail.Body = mailContent;
            //是否是html 格式显示
            mail.IsBodyHtml = false;
            //邮件优先级
            mail.Priority = MailPriority.Normal;

            try
            {
                smtp.Send(mail);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("发送邮件失败"+ex);
               
            }
        }
    }
}
 