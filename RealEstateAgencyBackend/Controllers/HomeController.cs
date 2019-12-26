using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateAgencyBackend.Controllers
{
    // Do we need this controller?
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        /*public ActionResult HandleStatic(string filename)
        {
            Response.ContentType = "image/jpeg"; // for JPEG file
            string physicalFileName = @"C:\Program Files\Adrenalin\Adrenalin\UploadedFiles\TemplateFile\abc.jpg";
            Response.WriteFile(physicalFileName);
            return 
        }*/
    }
}
