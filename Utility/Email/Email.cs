using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Email
{
    public class Email
    {
        public string Sender { get; set; }
        public string From { get; set; }
        public string[] To { get; set; }
        public string[] CC { get; set; }
        public string[] Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public MailPriority Priority { get; set; }
        public Attachment[] Attachments { get; set; }
    }
}
