namespace InventryShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRollProvidertbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RollBaseds",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.SignupTbl", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RollBaseds", "UserId", "dbo.SignupTbl");
            DropIndex("dbo.RollBaseds", new[] { "UserId" });
            DropTable("dbo.RollBaseds");
        }
    }
}
