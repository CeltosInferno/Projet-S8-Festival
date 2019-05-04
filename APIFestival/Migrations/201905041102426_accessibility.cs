namespace APIFestival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accessibility : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scenes", "Accessibility", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Scenes", "Accessibility");
        }
    }
}
