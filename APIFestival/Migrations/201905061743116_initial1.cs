namespace APIFestival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Interets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Interesser = c.Int(nullable: false),
                        FestivalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Festivals", t => t.FestivalId, cascadeDelete: true)
                .Index(t => t.FestivalId);
            
            AddColumn("dbo.Festivals", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Festivals", "NbSeats");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Festivals", "NbSeats", c => c.Int(nullable: false));
            DropForeignKey("dbo.Interets", "FestivalId", "dbo.Festivals");
            DropIndex("dbo.Interets", new[] { "FestivalId" });
            DropColumn("dbo.Festivals", "UserId");
            DropTable("dbo.Interets");
        }
    }
}
