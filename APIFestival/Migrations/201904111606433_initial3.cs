namespace APIFestival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Festivals", "LieuName", c => c.String());
            AddColumn("dbo.Festivals", "PostalCode", c => c.Int(nullable: false));
            DropColumn("dbo.Festivals", "Lieu_LieuName");
            DropColumn("dbo.Festivals", "Lieu_PostalCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Festivals", "Lieu_PostalCode", c => c.Int(nullable: false));
            AddColumn("dbo.Festivals", "Lieu_LieuName", c => c.String());
            DropColumn("dbo.Festivals", "PostalCode");
            DropColumn("dbo.Festivals", "LieuName");
        }
    }
}
