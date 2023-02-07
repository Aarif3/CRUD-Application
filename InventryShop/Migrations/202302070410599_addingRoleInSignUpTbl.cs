namespace InventryShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingRoleInSignUpTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SignupTbl", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SignupTbl", "Role");
        }
    }
}
