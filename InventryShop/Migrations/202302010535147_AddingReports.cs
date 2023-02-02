namespace InventryShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingReports : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Reports");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CatName = c.String(),
                        ProName = c.String(),
                        price = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}
