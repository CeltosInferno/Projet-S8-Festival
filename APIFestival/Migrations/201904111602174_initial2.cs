namespace APIFestival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lieux", "FestivalId", "dbo.Festivals");
            DropIndex("dbo.Lieux", new[] { "FestivalId" });
            AddColumn("dbo.Festivals", "Lieu_LieuName", c => c.String());
            AddColumn("dbo.Festivals", "Lieu_PostalCode", c => c.Int(nullable: false));
            DropTable("dbo.Lieux");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Lieux",
                c => new
                    {
                        FestivalId = c.Int(nullable: false),
                        LieuName = c.String(),
                        PostalCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FestivalId);
            
            DropColumn("dbo.Festivals", "Lieu_PostalCode");
            DropColumn("dbo.Festivals", "Lieu_LieuName");
            CreateIndex("dbo.Lieux", "FestivalId");
            AddForeignKey("dbo.Lieux", "FestivalId", "dbo.Festivals", "Id");
        }
    }
}
