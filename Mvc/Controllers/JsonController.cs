using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class JsonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        // GET: Json
        public ActionResult Hello(string name)
        {
            return Json(
                new {Name = name, Email=name + "@gmail.com", Message="welcome"}, JsonRequestBehavior.AllowGet
                );
        }
    }
}