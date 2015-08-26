using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace Didox.Business
{
    public class MailBox
    {
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public bool Send()
        {
            try
            {
                if (string.IsNullOrEmpty(EmailTo)) return false;
                MailMessage oEmail = new MailMessage();

                if (string.IsNullOrEmpty(EmailFrom))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SiteEmailDe"]))
                        EmailFrom = ConfigurationManager.AppSettings["SiteEmailDe"].ToString();
                    else EmailFrom = "TradeVision@digitalgest.com.br";
                }

                oEmail.To.Add(EmailTo);
                oEmail.From = new MailAddress(EmailFrom);
                oEmail.Priority = MailPriority.Normal;
                oEmail.IsBodyHtml = true;
                oEmail.Subject = Subject;
                oEmail.Body = Body;
                SmtpClient oEnviar = new SmtpClient();
                oEnviar.Host = "200.162.50.194";
                oEnviar.Credentials = new System.Net.NetworkCredential("SMTP.AmBev", "z1c@rd");
                oEnviar.Send(oEmail);
                oEmail.Dispose();

                return true;
            }
            catch { return false; }
        }
    }
}
