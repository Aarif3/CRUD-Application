namespace InventryShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogindb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SignupTbl",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        ConfirmPassword = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SignupTbl");
        }
    }
}
