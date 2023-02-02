namespace InventryShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultValue : DbMigration
    {
        public override void Up()
        {
            Sql("alter table CategoryLists add constraint DF_CatagoryLists_IsActive default 1 for IsActive");
        }
        
        public override void Down()
        {
        }
    }
}
