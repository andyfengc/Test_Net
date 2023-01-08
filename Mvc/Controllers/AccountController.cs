using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mvc.Data.Dao;
using Mvc.Data.Entity;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        [Authorize]
        // GET: Account
        public ActionResult Index()
        {
            return View("SimpleMain");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(User user, string returnUrl)
        {
            var dao = new AccountDao();
            var users = dao.GetAllUsers();
            var match = users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (match == null)
            {
                ViewBag.Message = "Invalid User";
                ModelState.AddModelError("invalid", "invalid user");
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("SimpleMain", "Account");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }

        [Authorize(Roles ="employer, admin")]
        public ActionResult SimpleMain()
        {
            ViewBag.Message = "Login as " + HttpContext.User.Identity.Name;
            return View();
        }
    }
}