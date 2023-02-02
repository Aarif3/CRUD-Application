namespace InventryShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCategoryrelation1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryLists",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CategoryId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryLists", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CategoryLists", "CategoryId", "dbo.Categories");
            DropIndex("dbo.CategoryLists", new[] { "CategoryId" });
            DropIndex("dbo.CategoryLists", new[] { "ProductId" });
            DropTable("dbo.CategoryLists");
        }
    }
}
