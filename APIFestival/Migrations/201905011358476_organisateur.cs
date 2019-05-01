namespace APIFestival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class organisateur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Organisateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Festivals", "OrganisateurId", c => c.Int(nullable: false));
            CreateIndex("dbo.Festivals", "OrganisateurId");
            AddForeignKey("dbo.Festivals", "OrganisateurId", "dbo.Organisateurs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Festivals", "OrganisateurId", "dbo.Organisateurs");
            DropIndex("dbo.Festivals", new[] { "OrganisateurId" });
            DropColumn("dbo.Festivals", "OrganisateurId");
            DropTable("dbo.Organisateurs");
        }
    }
}
