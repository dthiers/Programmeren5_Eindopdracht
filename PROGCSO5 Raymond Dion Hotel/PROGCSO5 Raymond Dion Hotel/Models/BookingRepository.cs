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

        // geef de gekozen boeking terug. Boekingen zijn uniek door een sleutel dus geef de boeking als de sleutel matched.
        public Booking GetBooking(int sleutel)
        {
            Booking retBooking = null;

            foreach (Booking b in bookingContext.Bookings)
            {
                if (b.Sleutel == sleutel)
                {
                    retBooking = b;
                }
            }
            return retBooking;
        }

        // wijzig de gegevens van de gekozen boeking.
        public void EditBooking(Booking booking)
        {
            foreach (Booking b in bookingContext.Bookings)
            {
                if (b.Sleutel == booking.Sleutel)
                {
                    bookingContext.Entry(b).CurrentValues.SetValues(booking);
                }
            }
            bookingContext.SaveChanges();
        }

        // verwijder de gekozen boeking.
        public void DeleteBooking(Booking booking)
        {
            foreach (Booking b in bookingContext.Bookings)
            {
                if (b.Sleutel == booking.Sleutel)
                {
                    bookingContext.Bookings.Remove(b);
                }
            }
            bookingContext.SaveChanges();
        }
    }
}