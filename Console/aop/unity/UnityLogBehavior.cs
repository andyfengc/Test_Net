using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Console.aop.unity
{
    public class UnityLogBehavior : IInterceptionBehavior // todo
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var method = input.MethodBase;
            var parameters = GetParameterInfo(input);
            var traceId = System.Guid.NewGuid().ToString();
            System.Console.WriteLine(string.Format("{0} - Before Invoking {1}", traceId, input.MethodBase.Name));
            var result = getNext()(input, getNext);
            if (result.Exception != null)
            {
                System.Console.WriteLine(string.Format("{0} - After Invoking Exception {1} - {2}", traceId, input.MethodBase.Name, result.Exception.Message));
            }
            else
            {
                System.Console.WriteLine(string.Format("{0} - After Invoking Exception {1} - {2}", traceId, input.MethodBase.Name, result.ReturnValue));
            }
            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        private string GetParameterInfo(IMethodInvocation input)
        {
            var str = "";
            for (int i = 0; i < input.Arguments.Count; i++)
            {
                str += input.Arguments.GetParameterInfo(i).Name + " - " + input.Arguments[i] + " | ";
            }
            return str;
        }
    }
}
