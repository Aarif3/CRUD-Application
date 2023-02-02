namespace InventryShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnablingMigrationFirsttime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CatName = c.String(),
                        ProName = c.String(),
                        price = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reports");
        }
    }
}
