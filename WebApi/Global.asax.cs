using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using WebApi.Providers;
using WebApi.Schedulers;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // register interceptors
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageInterceptor());
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new HeaderInterceptor());

            new PrintScheduler().Start();
        }
    }
}
