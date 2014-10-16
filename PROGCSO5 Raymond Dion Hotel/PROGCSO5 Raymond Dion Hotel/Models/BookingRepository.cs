using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROGCSO5_Raymond_Dion_Hotel.Models
{
    public class BookingRepository
    {
        private BookingContext bookingContext;

        public BookingRepository()
        {
            bookingContext = new BookingContext();
        }

        public IEnumerable<Booking> GetAll()
        {
            return bookingContext.Bookings.ToList();
        }
        
        public void AddBooking(Booking booking)
        {
            bookingContext.Bookings.Add(booking);
            bookingContext.SaveChanges();
        }
    }
}