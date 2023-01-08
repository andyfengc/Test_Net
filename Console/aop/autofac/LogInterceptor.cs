using Castle.DynamicProxy;
using System;
using System.Linq;
using Console.Log;

namespace Console.aop.autofac
{
    public class LogInterceptor : IInterceptor
    {
        private string logPath;
        public LogInterceptor(string logPath)
        {
            this.logPath = logPath;
        }
        public void Intercept(IInvocation invocation)
        {
            var enterMessage =
                $"{invocation.Method.Name} method invoked with parameters: {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())}";
            Logger.GetInstance(logPath).Log(enterMessage);
            System.Console.WriteLine(enterMessage);
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error has occured: {ex.Message}";
                Logger.GetInstance(logPath).Log(errorMessage);
                System.Console.WriteLine(errorMessage);
            }
            var exitMessage = $"{invocation.Method.Name} method finished: result was {invocation.ReturnValue}";
            Logger.GetInstance(logPath).Log(exitMessage);
            System.Console.WriteLine(exitMessage);
        }
    }
}
