namespace StockUpdate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockReference = c.Int(nullable: false, identity: true),
                        Ticker = c.String(maxLength: 20),
                        StockName = c.String(maxLength: 200),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StockReference);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stocks");
        }
    }
}
