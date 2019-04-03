namespace APIFestival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artistes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.String(),
                        Style = c.String(),
                        Comment = c.String(),
                        Nationality = c.String(),
                        MusicExtract = c.String(),
                        ProgrammationId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Programmations", t => t.ProgrammationId, cascadeDelete: true)
                .Index(t => t.ProgrammationId);
            
            CreateTable(
                "dbo.Programmations",
                c => new
                    {
                        ProgrammationId = c.Int(nullable: false, identity: true),
                        ProgrammationName = c.String(),
                        FestivalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProgrammationId)
                .ForeignKey("dbo.Festivals", t => t.FestivalId, cascadeDelete: true)
                .Index(t => t.FestivalId);
            
            CreateTable(
                "dbo.Festivals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lieux",
                c => new
                    {
                        FestivalId = c.Int(nullable: false),
                        LieuName = c.String(),
                        PostalCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FestivalId)
                .ForeignKey("dbo.Festivals", t => t.FestivalId)
                .Index(t => t.FestivalId);
            
            CreateTable(
                "dbo.Scenes",
                c => new
                    {
                        SceneId = c.Int(nullable: false, identity: true),
                        SceneName = c.String(),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SceneId);
            
            CreateTable(
                "dbo.Festivaliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sex = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Telephone = c.Int(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SceneProgrammations",
                c => new
                    {
                        Scene_SceneId = c.Int(nullable: false),
                        Programmation_ProgrammationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Scene_SceneId, t.Programmation_ProgrammationId })
                .ForeignKey("dbo.Scenes", t => t.Scene_SceneId, cascadeDelete: true)
                .ForeignKey("dbo.Programmations", t => t.Programmation_ProgrammationId, cascadeDelete: true)
                .Index(t => t.Scene_SceneId)
                .Index(t => t.Programmation_ProgrammationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artistes", "ProgrammationId", "dbo.Programmations");
            DropForeignKey("dbo.SceneProgrammations", "Programmation_ProgrammationId", "dbo.Programmations");
            DropForeignKey("dbo.SceneProgrammations", "Scene_SceneId", "dbo.Scenes");
            DropForeignKey("dbo.Programmations", "FestivalId", "dbo.Festivals");
            DropForeignKey("dbo.Lieux", "FestivalId", "dbo.Festivals");
            DropIndex("dbo.SceneProgrammations", new[] { "Programmation_ProgrammationId" });
            DropIndex("dbo.SceneProgrammations", new[] { "Scene_SceneId" });
            DropIndex("dbo.Lieux", new[] { "FestivalId" });
            DropIndex("dbo.Programmations", new[] { "FestivalId" });
            DropIndex("dbo.Artistes", new[] { "ProgrammationId" });
            DropTable("dbo.SceneProgrammations");
            DropTable("dbo.Festivaliers");
            DropTable("dbo.Scenes");
            DropTable("dbo.Lieux");
            DropTable("dbo.Festivals");
            DropTable("dbo.Programmations");
            DropTable("dbo.Artistes");
        }
    }
}
