using CompressFiles.Models;
using CompressionCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CompressFiles.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        //public FileController(): base()
        //{
        //    HttpContext.Session.Add("copy", false);
        //    HttpContext.Session.Add("convertion", false);
        //}
        //Modify this controller to make it more reusable and reduce boilerplate code.


        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult FileHandler(HttpPostedFileBase originalFile)
        {
            var fileName = Path.GetFileName(originalFile.FileName);
            var serverData = Server.MapPath("~/App_Data");
            var name = Path.Combine(serverData,fileName);
            //return Json("Good 'til here");
            originalFile.SaveAs(name);
            originalFile.InputStream.Close(); 
            HttpContext.Session.Add("originalFilename", fileName);
            HttpContext.Session.Add("copy", true);
            return Json("OK");
        }

        public JsonResult Converter()
        {
            bool copied = (bool)HttpContext.Session["copy"];
            if (!Request.IsAjaxRequest() || !copied)
            {
                throw new Exception("Invalid Call!");
            }
            var app_data = Server.MapPath("~/App_Data");
            var fileName = (string)HttpContext.Session["originalFilename"];
            var fullName = Path.Combine(app_data,fileName);
            var fileStream = new FileStream(fullName,FileMode.Open);
            var output = new FileStream(fullName + ".cf", FileMode.Create);
            var converter = new Compressor(fileStream);
            converter.RunProccess(output);
            HttpContext.Session.Add("convertion", true);

            //Update Db
            if (Request.IsAuthenticated)
            {
                string ID = User.Identity.GetUserId();
                var ac = new ApplicationDbContext();
                ApplicationUser user = ac.Users.FirstOrDefault(x => x.Id == ID);
                var toSave = new UploadedFile();
                toSave.FileName = fileName;
                toSave.Extension = fileName.Substring(fileName.LastIndexOf("."));
                toSave.OriginalSize = (int)fileStream.Length; //Change to long both o' 'em
                toSave.ConvertedSize = (int)output.Length;
                toSave.DateTime = DateTime.Now;
                toSave.OwnerUser = user;
                ac.AllFiles.Add(toSave);
                //user.Files.Add(toSave);
                try
                {
                    ac.SaveChanges();
                }
                catch (Exception e)
                {
                    var rr = e;
                    throw;
                }
                
            }
            output.Close();
            fileStream.Close();
            return Json("OK");
        }

        public FileResult GetConvertedFile()
        {
            var obj = HttpContext.Session["convertion"];
            bool converted = false;
            if (obj != null) converted = (bool)obj;
            if (!converted)
            {
                throw new InvalidOperationException("File hasn't converted yet!");
            }
            var app_data = Server.MapPath("~/App_Data");
            var fileName = (string)HttpContext.Session["originalFilename"];
            var fullName = Path.Combine(app_data, fileName);
            return File(fullName+".cf", "converted", fileName+".cf");
        }
      
        public ActionResult UnCompress(HttpPostedFileBase compressedFile)
        {
            return View();
        }
       

        [HttpPost]
        public JsonResult PostCompressedFile(HttpPostedFileBase compressedFile)
        {
            var fileName = Path.GetFileName(compressedFile.FileName);
            var serverData = Server.MapPath("~/App_Data");
            var name = Path.Combine(serverData, fileName);
            //return Json("Good 'til here");
            compressedFile.SaveAs(name);
            compressedFile.InputStream.Close();
            HttpContext.Session.Add("compressedFilename", fileName);
            HttpContext.Session.Add("copy_compressed", true);
            return Json("OK");
        }

        public JsonResult UnCompressAsync()
        {
            bool copied = (bool)HttpContext.Session["copy_compressed"];
            if (!Request.IsAjaxRequest() || !copied)
            {
                throw new Exception("Invalid Call!");
            }
            var app_data = Server.MapPath("~/App_Data");
            var fileName = (string)HttpContext.Session["compressedFilename"];
            var fullName = Path.Combine(app_data, fileName);
            var fileStream = new FileStream(fullName, FileMode.Open);
            fullName = fullName.Substring(0, fullName.LastIndexOf('.'));
            var output = new FileStream(fullName, FileMode.Create);
            var converter = new UnCompressor(fileStream);
            converter.RunProccess(output);
            output.Close();
            fileStream.Close();
            HttpContext.Session.Add("convertion_compressed", true);
            return Json("OK");
        }

        public FileResult GetOriginal()
        {
            var obj = HttpContext.Session["convertion_compressed"];
            bool converted = false;
            if (obj != null) converted = (bool)obj;
            if (!converted)
            {
                throw new InvalidOperationException("File hasn't converted yet!");
            }
            var app_data = Server.MapPath("~/App_Data");
            var fileName = (string)HttpContext.Session["compressedFilename"];
            var fullName = Path.Combine(app_data, fileName);
            fullName = fullName.Substring(0, fullName.LastIndexOf('.'));
            fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
            return File(fullName, "converted", fileName);
        }
    }
}