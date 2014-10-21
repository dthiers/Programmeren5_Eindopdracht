namespace PROGCS05_Dion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDatum = c.DateTime(nullable: false),
                        EindDatum = c.DateTime(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Voornaam = c.String(nullable: false),
                        Tussenvoegsel = c.String(),
                        Achternaam = c.String(nullable: false),
                        GeboorteDatum = c.DateTime(nullable: false),
                        ManOfVrouw = c.String(nullable: false),
                        Adres = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                        Woonplaats = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Prijs = c.Int(nullable: false),
                        FactuurNummer = c.Int(nullable: false),
                        BankrekeningNummer = c.String(),
                        Capaciteit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Capaciteit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Bookings", new[] { "RoomId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Bookings");
        }
    }
}
