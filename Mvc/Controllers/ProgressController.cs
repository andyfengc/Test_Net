using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class ProgressController : Controller
    {
        // GET: Progress
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpProgressBar()
        {
            return View();
        }
    }
}