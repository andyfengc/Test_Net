using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OwinDemo.Controllers
{
    [RoutePrefix("api")]
    public class WebApiController : System.Web.Http.ApiController
    {
        [Route("hello")]
        [HttpGet]
        public IHttpActionResult Hello()
        {
            return Content(HttpStatusCode.OK, "hello from webapi");
        }
    }
}
