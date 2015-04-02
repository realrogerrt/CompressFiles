namespace CompressFiles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Normalize2ndForm : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UploadedFiles", "Extension");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UploadedFiles", "Extension", c => c.String(maxLength: 5));
        }
    }
}
