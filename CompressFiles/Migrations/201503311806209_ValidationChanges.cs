namespace CompressFiles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UploadedFiles", "FileName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UploadedFiles", "FileName", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
