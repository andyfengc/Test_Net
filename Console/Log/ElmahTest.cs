using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Elmah;

namespace Console.Log
{
    public class ElmahTest
    {
        public static void ToXml()
        {
            ErrorLog errorLog = ErrorLog.GetDefault(null);
            errorLog.ApplicationName = "/LM/W3SVC/1/ROOT/AppName";
            errorLog.Log(new Error(new Exception("I am an tricky exception")));
        }

        public static void ToEmail()
        {
            var mail = new ElmahEmailHandler();
            mail.Log(new Error(new NullReferenceException()));//whatever exception u want to log
        }

        public static void ToSqlServer()
        {
            //to test
            //ErrorLog.GetDefault(null).Log(new Error(error));
        }
    }

    public class ElmahEmailHandler : ErrorMailModule
    {
        public ElmahEmailHandler()
        {
            //this basically just gets config from errorMail  (app.config)

            System.Console.WriteLine("Create ElmahEmailHandler object...");
            base.OnInit(new HttpApplication());
        }
        public void Log(Error error)
        {
            //just send the email pls
            System.Console.WriteLine("elmah is logging exceptions...");
            base.ReportError(error);
        }
    }
}
