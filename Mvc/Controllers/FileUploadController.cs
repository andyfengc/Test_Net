using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //// upload one file
        //public ActionResult Index(HttpPostedFileBase file)
        //{
        //    if (file.ContentLength > 0)
        //    {
        //        var fileName = Path.GetFileName(file.FileName);
        //        var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
        //        file.SaveAs(path);
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        // upload multiple files
        public ActionResult Index(IEnumerable<HttpPostedFileBase> files, string name, int age)
        {
            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    string newFilename = Guid.NewGuid().ToString("N") + extension;
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                }
            }
            return RedirectToAction("Index");
        }
    }
}