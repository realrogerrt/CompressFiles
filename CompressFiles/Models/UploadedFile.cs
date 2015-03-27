using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompressFiles.Models
{
    public class UploadedFile
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public DateTime DateTime { get; set; }
        public int OriginalSize { get; set; }
        public int ConvertedSize { get; set; }
    }
}