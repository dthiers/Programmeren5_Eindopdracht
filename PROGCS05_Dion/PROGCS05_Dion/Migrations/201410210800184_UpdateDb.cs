namespace PROGCS05_Dion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "BankrekeningNummer", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "BankrekeningNummer", c => c.String());
        }
    }
}
