using Application.ExternalDependencies;
using System;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.ExternalDependenciesImplementation
{
    public class EmailHandler : IEmailHandler
    {
        public void SendMail(string toEmail, string subject, string body, bool isBodyHtml = true)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("", "ABC Company");
                    mail.To.Add(new MailAddress(toEmail));
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = isBodyHtml;
                    using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("", "");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}