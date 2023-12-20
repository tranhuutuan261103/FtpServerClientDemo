using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Common
{
    public class MailHelper
    {
        public bool SendMail(string to, string subject, string body)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(to);
                message.Subject = subject;
                message.From = new MailAddress("tranhuutuan26112003@gmail.com");
                message.Body = body;
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("tranhuutuan26112003@gmail.com", "wuwentxxrzwpgsmk");
                smtp.EnableSsl = true;
                smtp.Send(message);
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            } catch
            {
                return false;
            }
        }
    }
}
