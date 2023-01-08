using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Spring.Aop;

namespace Console.aop
{
    public class LogBeforeService : IMethodBeforeAdvice
    {
        public void Before(MethodInfo method, object[] args, object target)
        {
            System.Console.WriteLine("intercept method name—>" + method.Name);
            System.Console.WriteLine("target—>" + target);
            System.Console.WriteLine("parameter—>");
            if (args != null)
            {
                foreach (object arg in args)
                {
                    System.Console.WriteLine("\t: " + arg);
                }
            }
        }
    }
}
