using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PROGCS05_Dion.Models {
    public class DatabaseContext : DbContext{
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookingen { get; set; }
        public DbSet<Guest> Guests { get; set; }
    }
}