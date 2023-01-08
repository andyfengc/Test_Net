using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AppFunc = System.Func<
    System.Collections.Generic.IDictionary<string, object>,
    System.Threading.Tasks.Task>;

namespace OwinDemo.Middleware
{
    public class GenericMiddleware 
    {
        AppFunc _next;
        public GenericMiddleware(AppFunc next) {
            _next = next;
        }
        
        public async Task Invoke(IDictionary<string, object> environment)
        {
            Debug.WriteLine("generic middleware begins");
            await this._next(environment);
            Debug.WriteLine("generic middleware ends");
        }
    }
}