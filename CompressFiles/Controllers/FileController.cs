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
        public ActionResult FileHandler(HttpPostedFileBase originalFile)
        {
            var compressor = new Compressor(originalFile.InputStream);
            //var name = originalFile.FileName;
            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //var random = new Random();
            //var result = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            
            //name += result;
            //Not working yet...
            var output = new FileStream(@"C:\Users\Roger\Desktop\out.cf", FileMode.OpenOrCreate);
            compressor.RunProccess(output);
            output.Close();
            Response.TransmitFile(@"C:\Users\Roger\Desktop\out.cf");
            return Redirect("/home/index");
            //return File(output, "compressed");
            //return new FileStreamResult(output, "compressed");
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