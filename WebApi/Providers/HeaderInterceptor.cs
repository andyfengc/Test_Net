using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApi.Providers
{
    public class HeaderInterceptor : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // work on the request 
            Trace.WriteLine(request.RequestUri.ToString());

            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add("X-Dummy-Header", Guid.NewGuid().ToString());
            return response;
        }
    }
}