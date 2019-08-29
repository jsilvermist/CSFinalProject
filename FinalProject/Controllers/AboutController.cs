using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class AboutController : Controller
    {
        public AboutController() : base()
        {
            ViewData["AboutIsActive"] = "active";
        }

        // GET: About
        public ActionResult Index()
        {
            return View();
        }
    }
}
