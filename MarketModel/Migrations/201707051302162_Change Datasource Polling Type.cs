namespace MarketModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDatasourcePollingType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DataSources", "PollingFrequency", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DataSources", "PollingFrequency", c => c.String());
        }
    }
}
