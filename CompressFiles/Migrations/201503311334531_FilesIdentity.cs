namespace CompressFiles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilesIdentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UploadedFiles", "OwnerUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UploadedFiles", new[] { "OwnerUser_Id" });
            DropTable("dbo.UploadedFiles");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.UploadedFiles", "OwnerUser_Id");
            AddForeignKey("dbo.UploadedFiles", "OwnerUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
