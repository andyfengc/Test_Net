using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AopAlliance.Intercept;

namespace Console.aop
{
    public class LogAroundService : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            System.Console.Out.WriteLine("Advice executing; calling the advised method..."); 

            //object returnValue = invocation.Proceed(); 
            //System.Console.Out.WriteLine("Advice executed; advised method returned " + returnValue); 
            //return returnValue;

            System.Console.Out.WriteLine("Advice executed; advised method returned " );
            return null;
        }
    }
}
