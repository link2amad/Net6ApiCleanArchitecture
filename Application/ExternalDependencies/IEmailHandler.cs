namespace Application.ExternalDependencies
{
    public interface IEmailHandler
    {
        void SendMail(string toEmail, string subject, string body, bool isBodyHtml = true);
    }
}