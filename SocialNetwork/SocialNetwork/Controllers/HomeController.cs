using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "This can be viewed only by authenticated users only";
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex()
        {
            ViewBag.Message = "This can be viewed only by users in Admin role only";
            return View();
        }

    }
}
