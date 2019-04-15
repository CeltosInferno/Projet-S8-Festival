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
                        ArtisteId = c.Int(nullable: false, identity: true),
                        ArtisteName = c.String(),
                        Photo = c.String(),
                        Style = c.String(),
                        Comment = c.String(),
                        Nationality = c.String(),
                        MusicExtract = c.String(),
                    })
                .PrimaryKey(t => t.ArtisteId);
            
            CreateTable(
                "dbo.Programmations",
                c => new
                    {
                        ProgrammationId = c.Int(nullable: false, identity: true),
                        ProgrammationName = c.String(),
                        FestivalId = c.Int(nullable: false),
                        SceneId = c.Int(nullable: false),
                        ArtisteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProgrammationId)
                .ForeignKey("dbo.Artistes", t => t.ArtisteId, cascadeDelete: true)
                .ForeignKey("dbo.Festivals", t => t.FestivalId, cascadeDelete: true)
                .ForeignKey("dbo.Scenes", t => t.SceneId, cascadeDelete: true)
                .Index(t => t.FestivalId)
                .Index(t => t.SceneId)
                .Index(t => t.ArtisteId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Programmations", "SceneId", "dbo.Scenes");
            DropForeignKey("dbo.Programmations", "FestivalId", "dbo.Festivals");
            DropForeignKey("dbo.Lieux", "FestivalId", "dbo.Festivals");
            DropForeignKey("dbo.Programmations", "ArtisteId", "dbo.Artistes");
            DropIndex("dbo.Lieux", new[] { "FestivalId" });
            DropIndex("dbo.Programmations", new[] { "ArtisteId" });
            DropIndex("dbo.Programmations", new[] { "SceneId" });
            DropIndex("dbo.Programmations", new[] { "FestivalId" });
            DropTable("dbo.Festivaliers");
            DropTable("dbo.Scenes");
            DropTable("dbo.Lieux");
            DropTable("dbo.Festivals");
            DropTable("dbo.Programmations");
            DropTable("dbo.Artistes");
        }
    }
}
