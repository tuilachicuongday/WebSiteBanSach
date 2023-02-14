namespace BanSach.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Category_ID", "dbo.Categories");
            DropIndex("dbo.Books", new[] { "Category_ID" });
            RenameColumn(table: "dbo.Books", name: "Category_ID", newName: "CategoryID");
            AlterColumn("dbo.Books", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "CategoryID");
            AddForeignKey("dbo.Books", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Books", new[] { "CategoryID" });
            AlterColumn("dbo.Books", "CategoryID", c => c.Int());
            RenameColumn(table: "dbo.Books", name: "CategoryID", newName: "Category_ID");
            CreateIndex("dbo.Books", "Category_ID");
            AddForeignKey("dbo.Books", "Category_ID", "dbo.Categories", "ID");
        }
    }
}
