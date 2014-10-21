using DomainModel;
using PROGCS05_Dion.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PROGCS05_Dion.Models {
    public class BookingRepository : IBookingRepository{

        private DatabaseContext dbContext;

        public BookingRepository() {
            dbContext = new DatabaseContext();
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
            return dbContext.Bookingen.ToList();
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
            if (booking != null) { dbContext.Bookingen.Add(booking); }
            dbContext.SaveChanges();
            return booking;
        }

        /*
         * Update booking in dbSet
         * */
        public Booking Update(Booking booking) {
            var b_list = GetAll();

            foreach (var b in b_list) {
                if (b != null) {
                    if (b.Id == booking.Id) {
                        dbContext.Entry(b).CurrentValues.SetValues(booking);
                    }
                }
            }
            dbContext.SaveChanges();

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
                        dbContext.Bookingen.Remove(b);
                    }
                }
            }
            dbContext.SaveChanges();
        }

        public DbSet<Room> GetRooms()
        {
            return dbContext.Rooms;
        }
    }
}