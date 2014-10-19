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


        public Booking GetBookingByID(int p_id) {
            var b_list = GetAll();

            foreach (var booking in b_list) {
                if (booking.Sleutel == p_id) {
                    return booking;
                }
            }
            return null;
        }

        public void EditBooking(Booking booking) {
            Booking bookingToEdit = bookingContext.Bookings.Where(b => b.Sleutel == booking.Sleutel).FirstOrDefault();

            if (bookingToEdit != null) {
                bookingContext.Entry(bookingToEdit).CurrentValues.SetValues(booking);
            }
            bookingContext.SaveChanges();
        }

        public void DeleteBooking(Booking booking) {
            Booking bookingToDelete = bookingContext.Bookings.Where(b => b.Sleutel == booking.Sleutel).FirstOrDefault();

            if (bookingToDelete != null) {
                bookingContext.Bookings.Remove(bookingToDelete);
            }
            bookingContext.SaveChanges();
        }
    }
}