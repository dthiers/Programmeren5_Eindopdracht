namespace PROGCS05_Dion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailCheck : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Guests", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Guests", "Email", c => c.String());
        }
    }
}
