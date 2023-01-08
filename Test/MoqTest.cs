using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Console.Moq;

namespace Test
{
    [TestClass]
    public class MoqTest
    {
        [TestMethod]
        public void SendEmail_ShouldSucceed()
        {
            // create mock
            var mockMailClient = new Mock<IMailClient>(MockBehavior.Strict);
            mockMailClient
                .SetupProperty(client => client.Server, "server.mail.com")
                .SetupProperty(client => client.Port, "1000");
            // set up mock
            mockMailClient.Setup(client =>
                client.SendMail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
                )
                .Callback(new Action<string, string, string, string>((a, b, c, d) =>
                {
                    System.Console.WriteLine($"from: {a} -> to: {b}, subject: {c}, body: {d}");
                }))
                .Returns(true);
            // create mock data
            var mailer = new DefaultMailer() { From = "from@mail.com", To = "to@mail.com", Subject = "Using Moq", Body = "Moq is awesome" };
            // use mock to send email
            var result = mailer.SendMail(mockMailClient.Object);
            // verify result
            mockMailClient.Verify(client => client.SendMail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
            System.Console.WriteLine($"sent result: {result}");
        }
    }
}
