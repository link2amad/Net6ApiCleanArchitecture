using Application.Dto;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces.IGeneralServices;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Application.Services.GeneralServices
{
    public class EmailService : IEmailService
    {
        private readonly IMapper _mapper;
        private readonly ISystemSettingRepository _systemSettingRepository;
        private readonly IEmailTemplateRepository _emailTemplateRepository;

        public EmailService(ISystemSettingRepository systemSettingRepository, IEmailTemplateRepository emailTemplateRepository, IMapper mapper)
        {
            _systemSettingRepository = systemSettingRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _mapper = mapper;
        }

        public bool SendResetPasswordEmail(UserDto userDetail)
        {
            List<SystemSetting> systemSettings = _systemSettingRepository.GetSystemSettings();
            List<SystemSettingDto> systemSettingDtoList = _mapper.Map<List<SystemSetting>, List<SystemSettingDto>>(systemSettings);
            EmailTemplate emailTemplate = _emailTemplateRepository.GetEmailTemplateByName(EmailTemplateTypes.ResetPasswordEmailSentToUser);
            string reseturl = SystemUrls.ResetPassword + userDetail.PasswordRequestHash +
            //string reseturl = "https://Live/auth/set-password?id=" + userDetail.PasswrodRequestHash +
                              "&pageCaption=" + "Reset";
            string emailTemplateBody = emailTemplate.Body.Replace("[First Name]", userDetail.FirstName)
                                                    .Replace("[Last Name]", userDetail.LastName)
                                                    .Replace("[URL]", reseturl);
            return SendEmail(emailTemplateBody, userDetail.Email, emailTemplate.Subject, systemSettingDtoList, null, true);
        }

        public static bool SendFileByEmail(string email, Attachment file, List<SystemSettingDto> appSettings)
        {
            return SendEmail("test", email, "test file", appSettings, null, true, file);
        }

        public static bool SendEmail(string message, string to, string subject, List<SystemSettingDto> systemSettings,
                                     List<string> ccs = null, bool isbodyhtml = false, Attachment file = null)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    var mailFrom = systemSettings.Where(x => x.SettingName == SystemSettingsVariables.FromEmail).FirstOrDefault();
                    var smtpClient = systemSettings.Where(x => x.SettingName == SystemSettingsVariables.SmtpClient).FirstOrDefault();
                    var smtpPort = systemSettings.Where(x => x.SettingName == SystemSettingsVariables.SmtpPort).FirstOrDefault();
                    var smtpUser = systemSettings.Where(x => x.SettingName == SystemSettingsVariables.SmtpUser).FirstOrDefault();
                    var smtpPassword = systemSettings.Where(x => x.SettingName == SystemSettingsVariables.SmtpPassword).FirstOrDefault();

                    mail.From = new MailAddress(mailFrom.SettingValue); //From Mail
                    mail.To.Add(new MailAddress(to));
                    if (ccs != null)
                    {
                        foreach (string cc in ccs)
                        {
                            mail.CC.Add(cc);
                        }
                    }
                    mail.IsBodyHtml = isbodyhtml;
                    mail.Subject = subject;
                    mail.Body = message;
                    if (file != null)
                    {
                        mail.Attachments.Add(file);
                    }
                    using (SmtpClient smtp = new SmtpClient(smtpClient.SettingValue, Convert.ToInt32(smtpPort.SettingValue)))
                    {
                        smtp.Credentials = new NetworkCredential(smtpUser.SettingValue, smtpPassword.SettingValue);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}