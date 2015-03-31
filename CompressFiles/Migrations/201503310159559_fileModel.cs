namespace CompressFiles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UploadedFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 15),
                        Extension = c.String(maxLength: 3),
                        DateTime = c.DateTime(nullable: false),
                        OriginalSize = c.Int(nullable: false),
                        ConvertedSize = c.Int(nullable: false),
                        OwnerUserID = c.Int(nullable: false),
                        OwnerUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerUser_Id)
                .Index(t => t.OwnerUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UploadedFiles", "OwnerUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UploadedFiles", new[] { "OwnerUser_Id" });
            DropTable("dbo.UploadedFiles");
        }
    }
}
