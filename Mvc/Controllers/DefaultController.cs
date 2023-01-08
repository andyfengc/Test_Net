using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            // domain validation
            // need install System.DirectoryServices and System.DirectoryServices.AccountManagement in nuget
            var domainContext = new System.DirectoryServices.AccountManagement.PrincipalContext(ContextType.Domain, "domain name");
            var validated = domainContext.ValidateCredentials("andyfeng", "123");
            //
            return View();
        }
        [HttpPost]
        public ActionResult Test(Data obj)
        {
            return Content("abc: " + obj);
        }
    }

    public class Data
    {
        public string name { get; set; }
        public string dept { get; set; }
        public ICollection<Event> Events { get; set; }
    }

    public class Event
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Messagetemplate { get; set; }
        public EventObject Properties { get; set; }
        
    }

    public class EventObject
    {
        public string Email { get; set; }
        public DateTime Time { get; set; }
    }
}
