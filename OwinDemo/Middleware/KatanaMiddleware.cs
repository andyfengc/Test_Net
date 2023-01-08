using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OwinDemo.Middleware
{
    public class KatanaMiddleware : OwinMiddleware
    {
        public KatanaMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            Debug.WriteLine("katana midware begins");
            await this.Next.Invoke(context);
            Debug.WriteLine("katana midware ends");
        }
    }
}