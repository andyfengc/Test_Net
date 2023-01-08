using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OwinDemo.Controllers
{
    public class MvcController : Controller
    {
        // GET: Mvc
        public ActionResult Index()
        {
            return View();
        }
    }
}