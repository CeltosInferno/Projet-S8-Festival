namespace FestivalApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Selections",
                c => new
                    {
                        SelectionId = c.Int(nullable: false, identity: true),
                        ProgrammationId = c.Int(nullable: false),
                        FestivalierId = c.Int(nullable: false),
                        PrimaireSecondaire = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SelectionId)
                .ForeignKey("dbo.Festivaliers", t => t.FestivalierId, cascadeDelete: false)
                .ForeignKey("dbo.Programmations", t => t.ProgrammationId, cascadeDelete: false)
                .Index(t => t.ProgrammationId)
                .Index(t => t.FestivalierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Selections", "ProgrammationId", "dbo.Programmations");
            DropForeignKey("dbo.Selections", "FestivalierId", "dbo.Festivaliers");
            DropIndex("dbo.Selections", new[] { "FestivalierId" });
            DropIndex("dbo.Selections", new[] { "ProgrammationId" });
            DropTable("dbo.Selections");
        }
    }
}
