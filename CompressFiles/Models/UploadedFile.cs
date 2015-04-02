using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CompressFiles.Models
{
    public class UploadedFile
    {
        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        [Required]
        public string FileName { get; set; }
        
        public DateTime DateTime { get; set; }
        
        [Required]
        public int OriginalSize { get; set; }
        
        [Required]
        public int ConvertedSize { get; set; }

        public string OwnerUserID { get; set; }
        
        public ApplicationUser OwnerUser { get; set; }
    }
}