using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Email;


namespace Console.Log
{
    public class SerilogTest
    {
        public static void Test()
        {
            //SerilogTest.WriteToRollingFile();
            //SerilogTest.WriteToSqlServerAdv();
            SerilogTest.WriteToConsoleAdv();
        }
        public static void WriteToSqlServer()
        {
            var log = new LoggerConfiguration()
                    .WriteTo.MSSqlServer(@"Server=.;Database=LogEvents;Trusted_Connection=True;", "Logs")
                    .CreateLogger();
            log.Information("I am an information log");
            log.Error("I am an error log");
            log.Error(new Exception("I am an exception"), "Exception occurs");
        }

        public static void WriteToSqlServerAdv()
        {
            string outputTemplate = "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{Exception} UserName:{UserName} Action:{Action}{NewLine}";
            var log = new LoggerConfiguration()
                    .WriteTo.MSSqlServer(@"Server=.;Database=LogEvents;Trusted_Connection=True;", "Logs").Enrich.With(new ThreadIdEnricher(), new MyEnricher())
                    .CreateLogger();
            log.Information("I am an information log");
            log.Error("I am an error log");
            log.Error(new Exception("I am an exception"), "Exception occurs");
        }

        public static void WriteToConsole()
        {
            var log = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();
            log.Information("I am an information log");
            log.Error(new Exception("test exception"), "error occur");
        }

        public static void WriteToConsoleAdv()
        {
            string outputTemplate = "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{Exception} UserName:{UserName} Action:{Action}{NewLine}";

            var log = new LoggerConfiguration()
                .Enrich.With(new ThreadIdEnricher(), new MyEnricher())
                .WriteTo.ColoredConsole(outputTemplate: outputTemplate)
                .CreateLogger();
            log.Information("I am an information log");
            log.Error(new Exception("test exception"), "error occur");
        }

        public static void WriteToFile()
        {
            var log = new LoggerConfiguration()
                    .WriteTo.File("log.txt", LogEventLevel.Error)
                    .CreateLogger();
            log.Information("I am an information log");
            log.Error("I am an error log");
        }

        public static void WriteToRollingFile()
        {
            var log = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.RollingFile("Log-{Date}.txt")
                .CreateLogger();
            log.Information("I am an information log");
            log.Error("I am an error log");
        }

        public static void WriteToEmail()
        {
            var log = new LoggerConfiguration()
                .WriteTo.Email(connectionInfo: new EmailConnectionInfo()
                {
                    EmailSubject = "Log Email",
                    FromEmail = "donotreply@kebe.com",
                    ToEmail = "afeng@kebe.com;afeng@kebe.com",
                    MailServer = "relay.kebe.corp",
                    NetworkCredentials = new NetworkCredential("donotreply@kebe.com", String.Empty)

                })
                .CreateLogger();
            log.Information("I am an information log");
            log.Error("I am an error log");
        }

        public static void WriteToHttp()
        {
            
        }

        class ThreadIdEnricher : ILogEventEnricher
        {
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddPropertyIfAbsent(
                    propertyFactory.CreateProperty("ThreadId", Thread.CurrentThread.ManagedThreadId));
            }
        }

        class MyEnricher : ILogEventEnricher
        {
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserName", "CATO"));
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Action", "Cancel shipment"));
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Order Id", "1xxxxxxxx"));
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Delivery No", "1xxxxxxxx"));
            }
        }
    }
}
