using CompressFiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CompressFiles.Controllers
{
    public class HomeController : Controller
    {
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

        //Get the information of the logged user from the log in manager
        public ActionResult Historial()
        {
            string ID = User.Identity.GetUserId();
            var ac = new ApplicationDbContext();
            //var user = ac.Users.FirstOrDefault(x => x.Id == ID);
            var files = from f in ac.AllFiles where f.OwnerUser.Id == ID select f;
            return View(files.ToList());
        }
    }
}