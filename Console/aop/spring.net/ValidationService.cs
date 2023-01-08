using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AopAlliance.Intercept;

namespace Console.aop
{
    public class ValidationService : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            var days = invocation.Arguments;
            object returnValue = invocation.Proceed();
            return returnValue;
        }
    }
}
