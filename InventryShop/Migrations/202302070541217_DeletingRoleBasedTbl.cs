namespace InventryShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletingRoleBasedTbl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RollBaseds", "UserId", "dbo.SignupTbl");
            DropIndex("dbo.RollBaseds", new[] { "UserId" });
            DropTable("dbo.RollBaseds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RollBaseds",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.RollBaseds", "UserId");
            AddForeignKey("dbo.RollBaseds", "UserId", "dbo.SignupTbl", "id", cascadeDelete: true);
        }
    }
}
