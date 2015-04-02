namespace CompressFiles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LargingExtension : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UploadedFiles", "Extension", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UploadedFiles", "Extension", c => c.String(maxLength: 4));
        }
    }
}
