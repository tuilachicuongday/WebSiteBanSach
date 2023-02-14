namespace BanSach.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageURLAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ImageURL");
        }
    }
}
