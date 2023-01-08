using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using OwinDemo.Models;

namespace OwinDemo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (model.Username == "admin" && model.Password == "admin")
            {
                var identity = new ClaimsIdentity("ApplicationCookie");
                identity.AddClaims(new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Username),
                    new Claim(ClaimTypes.Name, model.Username)
                });
                HttpContext.GetOwinContext().Authentication.SignIn(identity);
            }
            return View();
        }
    }
}