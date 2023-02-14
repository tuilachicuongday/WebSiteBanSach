namespace BanSach.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bansach : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Description", c => c.String());
            AlterColumn("dbo.Categories", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Books", "Description", c => c.String(maxLength: 500));
        }
    }
}
