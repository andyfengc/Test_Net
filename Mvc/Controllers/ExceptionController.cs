using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class ExceptionController : Controller
    {
        // GET: Exception
        public ActionResult Elmah()
        {
            throw new Exception("this is an expection on purpose");
        }
    }
}