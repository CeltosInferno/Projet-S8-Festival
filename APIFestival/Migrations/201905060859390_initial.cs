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
                        ArtisteID = c.Int(nullable: false, identity: true),
                        ArtisteNom = c.String(),
                        Photo = c.String(),
                        Style = c.String(),
                        Comment = c.String(),
                        Nationality = c.String(),
                        MusicExtract = c.String(),
                    })
                .PrimaryKey(t => t.ArtisteID);
            
            CreateTable(
                "dbo.Programmations",
                c => new
                    {
                        ProgrammationId = c.Int(nullable: false, identity: true),
                        ProgrammationName = c.String(),
                        FestivalID = c.Int(nullable: false),
                        SceneID = c.Int(nullable: false),
                        ArtisteID = c.Int(nullable: false),
                        DateDebutConcert = c.DateTime(nullable: false),
                        DateFinConcert = c.DateTime(nullable: false),
                        OrganisateurID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProgrammationId)
                .ForeignKey("dbo.Artistes", t => t.ArtisteID, cascadeDelete: true)
                .ForeignKey("dbo.Festivals", t => t.FestivalID, cascadeDelete: true)
                .ForeignKey("dbo.Organisateurs", t => t.OrganisateurID, cascadeDelete: true)
                .ForeignKey("dbo.Scenes", t => t.SceneID, cascadeDelete: true)
                .Index(t => t.FestivalID)
                .Index(t => t.SceneID)
                .Index(t => t.ArtisteID)
                .Index(t => t.OrganisateurID);
            
            CreateTable(
                "dbo.Festivals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                        DateDebut = c.DateTime(),
                        DateFin = c.DateTime(),
                        Lieu = c.String(),
                        CodePostal = c.Int(nullable: false),
                        IsInscription = c.Boolean(nullable: false),
                        IsPublication = c.Boolean(nullable: false),
                        OrganisateurId = c.Int(nullable: false),
                        Prix = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organisateurs", t => t.OrganisateurId, cascadeDelete: false)
                .Index(t => t.OrganisateurId);
            
            CreateTable(
                "dbo.Organisateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Mdp = c.String(),
                        Nom = c.String(),
                        Prenom = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scenes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Capacite = c.Int(nullable: false),
                        Accessibilite = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Festivaliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Genre = c.String(),
                        Naissance = c.DateTime(nullable: false),
                        Telephone = c.Int(nullable: false),
                        Email = c.String(),
                        Mdp = c.String(),
                        Ville = c.String(),
                        Rue = c.String(),
                        Pays = c.String(),
                        CodePostal = c.String(),
                        FestivalId = c.Int(nullable: false),
                        Prenom = c.String(),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Festivals", t => t.FestivalId, cascadeDelete: true)
                .Index(t => t.FestivalId);
            
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
                .ForeignKey("dbo.Programmations", t => t.ProgrammationId, cascadeDelete: false)
                .Index(t => t.ProgrammationId)
                .Index(t => t.FestivalierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Selections", "ProgrammationId", "dbo.Programmations");
            DropForeignKey("dbo.Selections", "FestivalierId", "dbo.Festivaliers");
            DropForeignKey("dbo.Festivaliers", "FestivalId", "dbo.Festivals");
            DropForeignKey("dbo.Programmations", "SceneID", "dbo.Scenes");
            DropForeignKey("dbo.Programmations", "OrganisateurID", "dbo.Organisateurs");
            DropForeignKey("dbo.Programmations", "FestivalID", "dbo.Festivals");
            DropForeignKey("dbo.Festivals", "OrganisateurId", "dbo.Organisateurs");
            DropForeignKey("dbo.Programmations", "ArtisteID", "dbo.Artistes");
            DropIndex("dbo.Selections", new[] { "FestivalierId" });
            DropIndex("dbo.Selections", new[] { "ProgrammationId" });
            DropIndex("dbo.Festivaliers", new[] { "FestivalId" });
            DropIndex("dbo.Festivals", new[] { "OrganisateurId" });
            DropIndex("dbo.Programmations", new[] { "OrganisateurID" });
            DropIndex("dbo.Programmations", new[] { "ArtisteID" });
            DropIndex("dbo.Programmations", new[] { "SceneID" });
            DropIndex("dbo.Programmations", new[] { "FestivalID" });
            DropTable("dbo.Selections");
            DropTable("dbo.Festivaliers");
            DropTable("dbo.Scenes");
            DropTable("dbo.Organisateurs");
            DropTable("dbo.Festivals");
            DropTable("dbo.Programmations");
            DropTable("dbo.Artistes");
        }
    }
}
