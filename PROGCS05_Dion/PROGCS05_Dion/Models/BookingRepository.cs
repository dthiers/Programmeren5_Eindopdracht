using DomainModel;
using PROGCS05_Dion.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROGCS05_Dion.Models {
    public class BookingRepository : IBookingRepository{

        private BookingContext bookingContext;

        public BookingRepository() {
            bookingContext = new BookingContext();
        }
        /*
         * Return booking
         * */
        public Booking Get() {
            return new Booking();
        }

        /*
         * Return list of all bookings (in DbSet)
         * */
        public List<Booking> GetAll() {
            return bookingContext.Bookings.ToList();
        }

        /*
         * Return booking by ID
         * */
        public Booking GetBookingByID(int id) {
            var b_list = GetAll();

            foreach (var booking in b_list) {
                if (booking.Id  ==id) {
                    return booking;
                }
            }
            return null;
        }

        /*
         * Create booking and save to DbSet
         * */
        public Booking Create(Booking booking) {
            if (booking != null) {
                bookingContext.Bookings.Add(booking);
            }
            bookingContext.SaveChanges();
            return booking;
        }

        /*
         * Update booking
         * */
        public Booking Update(Booking booking) {
            var b_list = GetAll();

            foreach (var b in b_list) {
                if (b != null) {
                    if (b.Id == booking.Id) {
                        bookingContext.Entry(b).CurrentValues.SetValues(booking);
                    }
                }
            }
            bookingContext.SaveChanges();

            return new Booking();
        }
        
        /*
         * Delete booking from DbSet
         * */
        public void Delete(Booking booking) {
            var b_list = GetAll();

            foreach (var b in b_list) {
                if (b != null) {
                    if (b.Id == booking.Id) {
                        bookingContext.Bookings.Remove(b);
                    }
                }
            }
            bookingContext.SaveChanges();
        }
    }
}