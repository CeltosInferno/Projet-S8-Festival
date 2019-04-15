namespace APIFestival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Festivals", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Festivals", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Festivals", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Festivals", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
