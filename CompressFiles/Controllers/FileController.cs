using CompressionCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompressFiles.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult FileHandler(HttpPostedFileBase originalFile)
        {
            var fileName = Path.GetFileName(originalFile.FileName);
            var serverData = Server.MapPath("~/App_Data");
            var name = Path.Combine(@"C:\Users\admin\Desktop",fileName);
            return Json("Good 'til here");
            originalFile.SaveAs(name);
            var fileStream = new FileStream(name, FileMode.Open);
            return Json("OK");
        }

        public ActionResult UnCompress(HttpPostedFileBase compressedFile)
        {
            return View();
        }
        //Get the information of the logged user from the log in manager
        public ActionResult Historial()
        {
            return View();
        }
    }
}