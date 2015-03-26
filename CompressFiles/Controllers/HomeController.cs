using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompressFiles.Controllers
{
    public class HomeController : Controller
    {
        public class InnerController : Controller
        {
            public string roger()
            {
                return "Roger!";
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult UnCompress()
        {
            return View();
        }
    }
}