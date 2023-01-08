using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Data.Contexts;
using Mvc.Models;
using Mvc.Models.Northwind;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Mvc.Controllers
{
    [RoutePrefix("")]
    public class NorthwindController : Controller
    {
        // GET: Default
        [Route("index")]
        public ActionResult Index()
        {
            return Content("ok");
        }
        [Route("users")]
        public ActionResult GetUsers(string name)
        {
            var users = GetUsers();
            this.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private List<string> GetUsers()
        {
            return new List<string>
            {
                "andy", "kevin", "john"
            };
        }

        [Route("employees")]
        public ActionResult GetEmployees()
        {
            using (var db = new NorthwindContext())
            {
                var employees = db.Employees.ToList();
                return View("EmployeeList", employees);
            }
        }
        [Route("api/employees")]
        public ActionResult GetEmployeesJson()
        {
            var db = new NorthwindContext();

            var employees = db.Employees.ToList();
            var result = JsonConvert.SerializeObject(employees, Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver()});
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        [Route("regions")]
        public ActionResult GetRegions()
        {
            using (var northwindDb = new NorthwindContext())
            {
                var regions = northwindDb.Regions.ToList();
                var territories = northwindDb.Territories.ToList();
                var employees = northwindDb.Employees.ToList();
                return View("RegionList", regions);
            }
        }
        [HttpGet]
        [Route("api/regions")]
        public ActionResult GetRegionsJson()
        {
            var northwindDb = new NorthwindContext();
            var regions = northwindDb.Regions.ToList();
            var territories = northwindDb.Territories.ToList();
            var employees = northwindDb.Employees.ToList();
            return Json(regions, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Route("regions")]
        public ActionResult AddRegion(string regionName)
        {
            using (var db = new NorthwindContext())
            {
                var currentMaxId = db.Regions.Max(r => r.RegionID);
                var region = new Region() { RegionID = currentMaxId + 1, RegionDescription = regionName };
                db.Regions.Add(region);
                db.SaveChanges();
            }
            return RedirectToAction("GetRegions");
        }

        [HttpGet]
        [Route("AddEmployee")]
        public ActionResult AddEmployee()
        {
            return View("AddEmployee");
        }

        [HttpPost]
        [Route("AddEmployee")]
        public ActionResult AddNewEmployee(Employees employee)
        {
            using (var db = new NorthwindContext())
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return HttpNotFound("not implemented yet");
            }
        }
    }
}