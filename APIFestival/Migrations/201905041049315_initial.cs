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
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        LieuName = c.String(),
                        PostalCode = c.Int(nullable: false),
                        IsInscription = c.Boolean(nullable: false),
                        IsPublication = c.Boolean(nullable: false),
                        OrganisateurId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organisateurs", t => t.OrganisateurId, cascadeDelete: true)
                .Index(t => t.OrganisateurId);
            
            CreateTable(
                "dbo.Organisateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scenes",
                c => new
                    {
                        SceneId = c.Int(nullable: false, identity: true),
                        SceneName = c.String(),
                        Capacity = c.Int(nullable: false),
                        Accessibility = c.String(),
                    })
                .PrimaryKey(t => t.SceneId);
            
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
                .ForeignKey("dbo.Festivaliers", t => t.FestivalierId, cascadeDelete: true)
                .ForeignKey("dbo.Programmations", t => t.ProgrammationId, cascadeDelete: true)
                .Index(t => t.ProgrammationId)
                .Index(t => t.FestivalierId);
            
            CreateTable(
                "dbo.Festivaliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Naissance = c.DateTime(nullable: false),
                        Email = c.String(),
                        Mdp = c.String(),
                        Genre = c.String(),
                        Telephone = c.String(),
                        CodePostal = c.String(),
                        Ville = c.String(),
                        Rue = c.String(),
                        Pays = c.String(),
                        FestivalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Festivals", t => t.FestivalId, cascadeDelete: true)
                .Index(t => t.FestivalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Selections", "ProgrammationId", "dbo.Programmations");
            DropForeignKey("dbo.Selections", "FestivalierId", "dbo.Festivaliers");
            DropForeignKey("dbo.Festivaliers", "FestivalId", "dbo.Festivals");
            DropForeignKey("dbo.Programmations", "SceneId", "dbo.Scenes");
            DropForeignKey("dbo.Programmations", "FestivalId", "dbo.Festivals");
            DropForeignKey("dbo.Festivals", "OrganisateurId", "dbo.Organisateurs");
            DropForeignKey("dbo.Programmations", "ArtisteId", "dbo.Artistes");
            DropIndex("dbo.Festivaliers", new[] { "FestivalId" });
            DropIndex("dbo.Selections", new[] { "FestivalierId" });
            DropIndex("dbo.Selections", new[] { "ProgrammationId" });
            DropIndex("dbo.Festivals", new[] { "OrganisateurId" });
            DropIndex("dbo.Programmations", new[] { "ArtisteId" });
            DropIndex("dbo.Programmations", new[] { "SceneId" });
            DropIndex("dbo.Programmations", new[] { "FestivalId" });
            DropTable("dbo.Festivaliers");
            DropTable("dbo.Selections");
            DropTable("dbo.Scenes");
            DropTable("dbo.Organisateurs");
            DropTable("dbo.Festivals");
            DropTable("dbo.Programmations");
            DropTable("dbo.Artistes");
        }
    }
}
