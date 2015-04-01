using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompressFiles.Models
{
    public class FileViewModel
    {
        [FileValidator]
        public HttpPostedFileBase file { get; set; }
    }

    public class FileValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var f = value as HttpPostedFile;
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format("{0} is not valid", name);
        }
    }
}