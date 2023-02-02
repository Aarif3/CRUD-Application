namespace InventryShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingunknow : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reports", "price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reports", "price", c => c.String());
        }
    }
}
