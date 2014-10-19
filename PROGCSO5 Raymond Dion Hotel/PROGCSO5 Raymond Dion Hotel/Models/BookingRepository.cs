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

        public Booking CalculatePrice(Booking booking)
        {
            int price = 0;

            DateTime startHoogTarief = new DateTime(2014, 6, 1);
            DateTime eindHoogTarief = new DateTime(2014, 8, 31);

            // als het hoog tarief is is de prijs 90 euro per nacht
            // anders 60 euro per nacht (deze waardes zijn verzonnen)
            for (DateTime date = booking.CheckInDatum; date <= booking.CheckOutDatum; date = date.AddDays(1))
            {
                if (date.Month >= startHoogTarief.Month && date.Month <= eindHoogTarief.Month)
                {
                    price += 90;
                }
                else
                {
                    price += 60;
                }
            }

            booking.Prijs = price;

            return booking;
        }
    }
}