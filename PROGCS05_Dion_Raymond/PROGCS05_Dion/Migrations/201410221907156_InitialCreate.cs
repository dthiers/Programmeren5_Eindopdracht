namespace PROGCS05_Dion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDatum = c.DateTime(),
                        EindDatum = c.DateTime(),
                        RoomId = c.Int(nullable: false),
                        Voornaam = c.String(nullable: false),
                        Tussenvoegsel = c.String(),
                        Achternaam = c.String(nullable: false),
                        GeboorteDatum = c.DateTime(nullable: false),
                        ManOfVrouw = c.String(nullable: false),
                        Adres = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                        Woonplaats = c.String(nullable: false),
                        Email = c.String(),
                        Prijs = c.Int(nullable: false),
                        FactuurNummer = c.Int(nullable: false),
                        BankrekeningNummer = c.String(),
                        Capaciteit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        Voornaam = c.String(nullable: false),
                        Tussenvoegsel = c.String(),
                        Achternaam = c.String(nullable: false),
                        GeboorteDatum = c.DateTime(nullable: false),
                        ManOfVrouw = c.String(nullable: false),
                        Adres = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                        Woonplaats = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .Index(t => t.BookingId);
            
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
            DropForeignKey("dbo.Guests", "BookingId", "dbo.Bookings");
            DropIndex("dbo.Guests", new[] { "BookingId" });
            DropIndex("dbo.Bookings", new[] { "RoomId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Guests");
            DropTable("dbo.Bookings");
        }
    }
}
