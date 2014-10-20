using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PROGCS05_Dion.Controllers {
    public class BookingContext : DbContext{
        public DbSet<Booking> Bookings { get; set; }
    }
}