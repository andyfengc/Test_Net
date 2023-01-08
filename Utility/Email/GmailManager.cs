using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Email
{
    public class GmailManager : IEmailManager
    {
        public string Host { get { return "smtp.gmail.com"; } }
        public int Port { get { return 587; } }
        public string UserName { get { return "service@tweebaa.com"; } }
        public string Password { get { return "222"; } }

        private SmtpClient smtpClient;

        public GmailManager()
        {
            this.smtpClient = new SmtpClient();
            smtpClient.Host = Host;
            smtpClient.Port = Port;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential nc = new NetworkCredential(UserName, Password);
            smtpClient.Credentials = nc;
            smtpClient.EnableSsl = true;
        }

        public void Send(Email email)
        {
            var mm = ToMailMessage(email);
            smtpClient.Send(mm);
        }

        private MailMessage ToMailMessage(Email email)
        {
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress(UserName);
            foreach (var to in email.To)
            {
                mm.To.Add(new MailAddress(to));
            }
            mm.Subject = email.Subject;
            mm.SubjectEncoding = Encoding.UTF8;
            mm.Body = email.Body;
            mm.IsBodyHtml = email.IsBodyHtml;
            mm.BodyEncoding = Encoding.UTF8;
            return mm;
        }
    }
}
