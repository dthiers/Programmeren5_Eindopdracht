using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PROGCSO5_Raymond_Dion_Hotel
{
    public class BookingContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
    }
}