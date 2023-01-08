using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class ResponseController : Controller
    {
        // GET: Response
        public ActionResult Header()
        {
            this.Response.AppendHeader("header-c#", "header-value-C#");
            return Content("append header");
        }
    }
}