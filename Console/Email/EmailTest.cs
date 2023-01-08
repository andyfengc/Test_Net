using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.Email
{
    public class EmailTest
    {
        public static void SendEmail()
        {
            var email = new Utility.Email.Email();
            email.To = new String[]{"andyinbox3@gmail.com"};
            email.Subject = "test a email";
            var emailManager = new Utility.Email.GmailManager();
            emailManager.Send(email);
        }
    }
}
