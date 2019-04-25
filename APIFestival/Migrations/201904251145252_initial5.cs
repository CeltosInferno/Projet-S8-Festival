namespace APIFestival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Festivals", "IsInscription", c => c.Boolean(nullable: false));
            AddColumn("dbo.Festivals", "IsPublication", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Festivals", "IsPublication");
            DropColumn("dbo.Festivals", "IsInscription");
        }
    }
}
