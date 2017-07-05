namespace MarketModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialiseDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Symbol = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceUSD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceBTC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vol24USD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarketCapUSD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvailableSupply = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalSupply = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PercentChange24h = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PercentChange1h = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PercentChange7d = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LastUpdated = c.Long(nullable: false),
                        Coin_Id = c.Int(),
                        DataSourceQuery_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coins", t => t.Coin_Id)
                .ForeignKey("dbo.DataSourceQueries", t => t.DataSourceQuery_Id)
                .Index(t => t.Coin_Id)
                .Index(t => t.DataSourceQuery_Id);
            
            CreateTable(
                "dbo.DataSourceQueries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        DataSource_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DataSources", t => t.DataSource_Id)
                .Index(t => t.DataSource_Id);
            
            CreateTable(
                "dbo.DataSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        Scraper = c.String(),
                        PollingFrequency = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickers", "DataSourceQuery_Id", "dbo.DataSourceQueries");
            DropForeignKey("dbo.DataSourceQueries", "DataSource_Id", "dbo.DataSources");
            DropForeignKey("dbo.Tickers", "Coin_Id", "dbo.Coins");
            DropIndex("dbo.DataSourceQueries", new[] { "DataSource_Id" });
            DropIndex("dbo.Tickers", new[] { "DataSourceQuery_Id" });
            DropIndex("dbo.Tickers", new[] { "Coin_Id" });
            DropTable("dbo.DataSources");
            DropTable("dbo.DataSourceQueries");
            DropTable("dbo.Tickers");
            DropTable("dbo.Coins");
        }
    }
}
