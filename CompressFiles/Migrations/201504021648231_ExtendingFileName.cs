namespace CompressFiles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendingFileName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UploadedFiles", "FileName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UploadedFiles", "FileName", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
