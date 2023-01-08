using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Console.aop;
using Console.aop.autofac;
using Console.aop.unity;
using Console.Async;
using Console.Context;
using Console.Convert;
using Console.Csv;
using Console.Db;
using Console.IOC;
using Console.Json;
using Console.Log;
using Console.Lottery;
using Console.Misc;
using Console.Moq;
using Console.Network;
using Console.Pdf;
using Console.Scheduler;
using Console.Security;
using Console.Serializer;
using Console.Text;
using Console.Utils;
using Console.Zebra;
using Console.Zip;
using CsvHelper;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Serilog;
using Serilog.Sinks.PeriodicBatching;
using Spring.Aop.Framework;
using Spring.Context;
using Spring.Context.Support;
using System.Diagnostics;
using Console.attribute;
using Console.Facebook;
using Console.Image;
using Console.Threads;
using Console.Qrcode;
using Console.Jwt;
using Console.Email;
using Console.Office;
using System.Reflection;
using Console.Sort;
using Microsoft.Practices.ObjectBuilder2;

namespace Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            //StudentDbContext db = new StudentDbContext();
            //int a = db.Students.Count();
            ////int b = db.Blogs.Count();
            //System.Console.WriteLine("done");
            //del d = new Calculator().Add;

            // serilog
            //SerilogTest.Test();
            //SerilogTest.WriteToConsole();

            //unity
            //UnityTest ut = new UnityTest();
            //ut.Test();

            // encoding
            //Utf8Test.CharaterToUtf8("上海");
            //Utf8Test.CharaterToUtf8("Rosenstraße");

            //json
            //JsonTest.Serialize();

            //schedule
            //QuartzTest.SchedulePrintJob();
            //QuartzPreview.HelloQuartz();

            // object init order
            //new InitOrder();

            //async test
            //AsyncTest.Test();

            //System.Console.Write(DateTime.Parse("Thu 08-Oct-2015 01:58"));
            //System.Console.Write("98012-9014".Replace("-", ""));

            //ZebraDemo.GetALabel();
            //ZebraHelper.ToImage();

            //SftpTest.ListDirectory();
            //SftpTest.UploadFile();
            //SftpTest.DownloadFile();
            //SftpTest.MoveFile();
            //SftpTest.ReadXmlFile();

            //RegularExpressionTest.TestXmlFileName();

            //GuidTest.Test();

            //// elmah
            //ElmahTest.ToXml();
            //ElmahTest.ToEmail();

            //pdf
            //ITextSharpTest.ExtractTextFromPdf("");
            //ITextSharpTest.CreatePdf("This is a paragraph", "C:/delete");
            //ITextSharpTest.CreatePdfFromImage("C:\\Users\\afeng\\Downloads\\freeformatter-decoded.gif", "c:/delete/doc2.pdf");
            //ITextSharpTest.CreateMergedPdfInSeparatePages("This is some text...", "c:/delete/doc6.pdf");
            //ITextSharpTest.GenerateBarCode("c:/delete/barcode1.gif");
            //ITextSharpTest.CreatePdfWithFont();
            //ITextSharpTest.GenerateRmaLabel("c:/delete/rma_test.pdf");
            //ITextSharpTest.Merge2PdfIn1Page("c:/delete/rotate4.pdf");
            //PDFBoxTest.ExtractTextFromPdf("");
            //ITextSharpTest.GenerateCompleteLabelFromImage("c:/delete/ups-label.gif", "c:/delete/label_with_image.pdf");

            // asc
            //Asc.TestNonprintableChars();
            //Asc.ReadAsc();
            //Asc.WriteAsc();

            // zip file - .net built-in
            //ZipTest.ZipFiles("c:/delete/1.zip", "c:\\Delete\\rma\\cp_label.pdf", "c:\\Delete\\rma\\rma_label.pdf");
            //ZipTest.DisplayZip("c:/delete/1.zip");
            //ZipTest.ExtractZip("c:/delete/1.zip", "c:/delete/1/output");

            // zip file - dotnetzip lib
            //DotNetZipTest.ZipFiles("c:/delete/1.zip", "c:\\Delete\\rma\\cp_label.pdf", "c:\\Delete\\rma\\rma_label.pdf");
            //DotNetZipTest.ExtractZip("c:/delete/1.zip", "c:/delete/1/output");

            // zip stream - .net built-in, buggy
            //var inByte1 = File.ReadAllBytes("c:\\Delete\\rma\\cp_label.pdf");
            //var inByte2 = File.ReadAllBytes("c:\\Delete\\rma\\rma_label.pdf");
            //var compressedBytes = ZipTest.ZipStreams(inByte1, inByte2);
            //File.WriteAllBytes("c:/delete/zip.zip", compressedBytes);
            ////
            //var decompressedBytes = ZipTest.UnzipStreams(compressedBytes);
            //var count = 0;
            //foreach (var decompressedByte in decompressedBytes)
            //{
            //    File.WriteAllBytes("c:/delete/decompressed" + count++ + ".pdf", decompressedByte);
            //}

            //// zip stream - by dotnetzip lib
            //var inByte1 = File.ReadAllBytes("c:\\Delete\\rma\\cp_label.pdf");
            //var inByte2 = File.ReadAllBytes("c:\\Delete\\rma\\rma_label.pdf");
            //var compressedBytes = DotNetZipTest.ZipStreams(inByte1, inByte2);
            //File.WriteAllBytes("c:/delete/zip.zip", compressedBytes);
            ////
            //var decompressedBytes = DotNetZipTest.UnZipStreams(compressedBytes);
            //var count = 0;
            //foreach (var decompressedByte in decompressedBytes)
            //{
            //    File.WriteAllBytes("c:/delete/decompressed" + count++ + ".pdf", decompressedByte);
            //}

            // serilog write to kibana

            //SerializerTest.Serialize();
            //SerializerTest.Deserialize();

            // read lottery excel
            //var lotteryManager = new LotteryManager();
            ////lotteryManager.L649ExcelToConsole("c:\\Users\\andyf\\Dropbox\\Workplace\\lottery_649.xlsx");
            //lotteryManager.L649ExcelToDb("c:\\Users\\andyf\\Dropbox\\Workplace\\lottery_649.xlsx");

            // spring.net aop, way1
            //IApplicationContext context = ContextRegistry.GetContext();
            //IOrderService command = (IOrderService)context["orderService"];
            //command.GrabNewOrders(5);

            //var order = new Order()
            //{
            //    orderId = "001",
            //    createdTime = DateTime.Now,
            //    totalAmount = 99.99
            //};
            // spring.net aop, way2
            //ProxyFactory factory = new ProxyFactory(new OrderService());
            ////factory.AddAdvice(new LogAroundService());
            //factory.AddAdvice(new ValidationService());
            //IOrderService service = (IOrderService)factory.GetProxy();
            ////service.GrabNewOrders(3);
            //service.ShipOrder(order);

            // unit aop, todo
            //UnityContainer container = new UnityContainer();
            //container.AddNewExtension<Interception>();
            //container
            //    .RegisterType<IOrderService, OrderService>(
            //        new Interceptor<InterfaceInterceptor>(),
            //        new InterceptionBehavior<UnityLogBehavior>()
            //    );
            //.Configure<Interception>()
            //.SetInterceptorFor<IOrderService>(new InterfaceInterceptor());
            //IOrderService service = container.Resolve<IOrderService>();
            //service.ShipOrder(order);

            //          container.RegisterType<IOrderService, OrderService>(
            //new Interceptor<InterfaceInterceptor>(),
            //new InterceptionBehavior<LoggingInterceptionBehavior>());
            //          IOrderService service = container.Resolve<IOrderService>();
            //          service.ShipOrder(order);

            // autofac aop
            // create ioc container builder
            //var builder = new ContainerBuilder();
            //// register service
            //builder.RegisterType<OrderService>()
            //    .As<IOrderService>()
            //    .AsSelf()
            //    .EnableClassInterceptors();
            //builder.Register(c => new LogInterceptor("c:/delete/aop"))
            //    .As<Castle.DynamicProxy.IInterceptor>()
            //    .AsSelf();
            // generate container
            //var container = builder.Build();
            // resolve service
            //var orderService = container.Resolve<IOrderService>();
            //orderService.GrabNewOrders(10);
            //orderService.ShipOrder(new Order() { orderId = "#001", totalAmount = 100, createdTime = DateTime.Now });

            // csv parser
            //CsvTest.GetAllRecords();

            // calc hash
            //HashTest.CalculateHash();
            //var md5 = HashTest.CreateMD5("ttt");

            // facebook
            //FacebookTest.GetPosts();

            // thread
            //ThreadTest.RunWithoutThread();
            //ThreadTest.RunWithTask();

            //qrcode
            //QrCodeTest.CreateQrCode();

            // image
            //ImageTest.CreateImage();
            //ImageTest.TransformImageWithRedFilter();
            //ImageTest.ResizeImage();

            //jwt
            //var jwtToken = JwtTest.IssueToken();
            //JwtTest.ValidateToken(jwtToken);

            // EmailTest.SendEmail();

            //PoiTest.GenerateExcel(@"c:/delete/newbook.core.xlsx");
            //PoiTest.GenerateWord(@"c:/delete/newbook.core.docx");
            //using (FileStream file = new FileStream(@"c:/delete/newbook.core2.xlsx", FileMode.Create, System.IO.FileAccess.Write))
            //{
            //    byte[] bytes = PoiTest.GenerateExcel2();
            //    file.Write(bytes, 0, bytes.Length);
            //}

            // attribute
            //HelpClass help = new HelpClass("Vidya Vrat", "India");
            //System.Console.WriteLine("Result:{0}", help.Details());
            // way1, get attribute from class
            //MemberInfo info = typeof(HelpClass);
            //var attributes = info.GetCustomAttributes(typeof(HelpAttribute), false);
            //foreach (var attribute in attributes)
            //{
            //    HelpAttribute a = (HelpAttribute)attribute;
            //    System.Console.WriteLine("Company: {0}", a.Company);
            //}
            // way2, get attribute from class
            //MemberInfo info = typeof(HelpClass);
            //var attributes = info.GetCustomAttributes(true);
            //foreach (var attribute in attributes)
            //{
            //    HelpAttribute a = attribute as HelpAttribute;
            //    if (a != null) System.Console.WriteLine("Company: {0}", a.Company);
            //}
            // way1, get attribute from property
            //foreach (var propertyInfo in help.GetType().GetProperties())
            //{
            //    var attrs = propertyInfo.GetCustomAttributes(typeof(HelpAttribute), true);
            //    foreach (var attribute in attrs)
            //    {
            //        HelpAttribute a = (HelpAttribute)attribute;
            //        System.Console.WriteLine("Company: {0}", a.Company);
            //    }
            //}

            // sort algothm
            var numbers = new int[] {9, 3, 15, -5, 77, 36, 39, -11, 101, 88, 99};
            var sortedNumbers = BubbleSortTest.SortV2(numbers);
            sortedNumbers.ForEach(number => System.Console.Write(number+ ", ") );
        }

        private delegate int del(int a, int b);

        private class Calculator
        {
            public int Add(int a, int b)
            {
                return a + b;
            }
        }
    }
}
