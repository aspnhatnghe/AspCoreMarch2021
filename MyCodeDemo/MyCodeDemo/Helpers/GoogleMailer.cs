using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyCodeDemo.Helpers
{
    public class GoogleMailer
    {
        static String email = "hocaspcore@gmail.com";//vui lòng sửa mail của bạn
        static String password = "abcDEF";//vui lòng sửa password của bạn

        // Gửi mail từ hệ thống
        public static void Send(String to, String subject, String body)
        {
            String from = "eShop K5 <" + email + ">";
            GoogleMailer.Send(from, to, "", "", subject, body, "");
        }

        // Gửi mail đơn giản
        public static void Send(String from, String to, String subject, String body)
        {
            GoogleMailer.Send(from, to, "", "", subject, body, "");
        }

        // Gửi mail nhiều lựa chọn đầy đủ
        public static void Send(String from, String to, String cc, String bcc, String subject, String body, String attachments)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.ReplyTo = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            if (!String.IsNullOrEmpty(cc))
            {
                mail.CC.Add(cc);
            }

            if (!String.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(bcc);
            }

            if (!String.IsNullOrEmpty(attachments))
            {
                String[] fileNames = attachments.Split(";,".ToCharArray());
                foreach (String fileName in fileNames)
                {
                    if (fileName.Trim().Length > 0)
                    {
                        mail.Attachments.Add(new Attachment(fileName.Trim()));
                    }
                }
            }

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(email, password);
            client.Send(mail);
        }
    }
}
