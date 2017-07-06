namespace MarketModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLastUpdateType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickers", "LastUpdated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickers", "LastUpdated", c => c.Long(nullable: false));
        }
    }
}
