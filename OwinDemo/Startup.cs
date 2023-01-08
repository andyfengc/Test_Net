using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Web.Http;
using Microsoft.Owin;
using OwinDemo.Middleware;

namespace OwinDemo
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // cookie authentication 
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/account/login")
            });
            // register webapi
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
            // inline middleware
            app.Use(async (context, next) =>
            {
                Debug.WriteLine("Incoming request: " + context.Request.Path);
                await next();
                Debug.WriteLine("Output response: " + context.Request.Path);
            });
            // inline middleware
            //app.Use(new Func<AppFunc, AppFunc>(next => (async context =>
            //{
            //    using (var writer = new StreamWriter(context["owin.ResponseBody"] as Stream))
            //    {
            //        await writer.WriteAsync("hello from inline Method middleware");
            //    }
            //    await next.Invoke(context);
            //})));
            // external middleware
            app.Use<GenericMiddleware>();
            // external middleware
            app.Use<KatanaMiddleware>();
            //
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello !");
            //});
        }
    }
}