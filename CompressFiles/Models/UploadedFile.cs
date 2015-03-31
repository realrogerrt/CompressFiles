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
        [StringLength(15)]
        [Required]
        public string FileName { get; set; }
        [StringLength(3)]
        public string Extension { get; set; }
        public DateTime DateTime { get; set; }
        [Required]
        public int OriginalSize { get; set; }
        [Required]
        public int ConvertedSize { get; set; }

        public string OwnerUserID { get; set; }
        public ApplicationUser OwnerUser { get; set; }
    }

    //public class UpLoadedFileContext : DbContext
    //{
    //    public virtual DbSet<UploadedFile> Files { get; set; }

    //    public UpLoadedFileContext() : base("DefaultConnection") { }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //    }
    //}
}