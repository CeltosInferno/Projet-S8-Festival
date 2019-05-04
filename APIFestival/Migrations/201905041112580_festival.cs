namespace APIFestival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class festival : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Festivals", "NbSeats", c => c.Int(nullable: false));
            AddColumn("dbo.Festivals", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Festivals", "Price");
            DropColumn("dbo.Festivals", "NbSeats");
        }
    }
}
