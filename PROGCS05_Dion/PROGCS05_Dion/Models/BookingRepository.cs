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
            return (dbContext.Bookingen.Where(b => b.Id == id).FirstOrDefault());
                }

        public Booking SetRoomIdForBooking(int roomId, int bookingId) {
            Booking changeRoomId = GetBookingByID(bookingId);
            changeRoomId.RoomId = roomId;
            dbContext.SaveChanges();
            return changeRoomId;
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
        public Booking Update(Booking booking, int roomId) {
            Booking b_update = dbContext.Bookingen.Where(b => b.Id == booking.Id).FirstOrDefault();
            if (b_update != null) {
                dbContext.Entry(b_update).CurrentValues.SetValues(booking);
                    }
            dbContext.SaveChanges();

            return new Booking();
        }
        
        /*
         * Delete booking from DbSet
         * */
        public void Delete(Booking booking) {
            var b_delete = dbContext.Bookingen.Where(b => b.Id == booking.Id).FirstOrDefault();

            if (b_delete != null){
                dbContext.Bookingen.Remove(b_delete);
            }
            dbContext.SaveChanges();
        }

        public DbSet<Room> GetRooms(){
            return dbContext.Rooms;
        }

        public int CalculatePrice(int capacity, Nullable<System.DateTime> startDatum, Nullable<System.DateTime> eindDatum)
        {
            //Ik maak een DateTime voor wanneer het hoogtarief start en wanneer het hoogtarief eindigt.
            DateTime hoogTariefBegin = new DateTime(2015, 6, 1);
            DateTime hoogTariefEind = new DateTime(2015, 8, 31);

            int prijs = 0;

            if (capacity == 2) {
                prijs = 20;
            }
            else if (capacity == 3) {
                prijs = 30;
            }
            else {
                prijs = 40;
            }

            // ik maak een DateTime die de value pakt van het Nonnullable startDatum.
            DateTime deStartDatum = startDatum.Value;

            for (DateTime date = deStartDatum; date <= eindDatum; date = date.AddDays(1))
            {
                if (startDatum >= hoogTariefBegin && eindDatum <= hoogTariefEind) {
                    prijs += 90;
                }
                else {
                    prijs += 60;
                }
            }
            return prijs;
        }

        public List<Room> GetEmptyRooms() {
            // Ik heb alle kamers (id en capaciteit)
            var allRooms = dbContext.Rooms.ToList();
            var allBookings = dbContext.Bookingen.ToList();

            // allRooms wordt nu eigenlijk emptyRooms, dus ik rename 'm wel jonge
            foreach (Room r in allRooms.ToList()) {
                foreach (Booking b in allBookings) {
                    if (b.Id == r.Id) {
                        allRooms.Remove(r);
                    }
                }
            }
            // Overschrijven ivm logische naamgeving.
            var emptyRooms = allRooms;
            return emptyRooms;
        }

        public Boolean DatesOverlapForRoom(DateTime startDatum, DateTime eindDatum, int capaciteit, int id, int roomId) {
            // We willen kijken of het kamernummer dat aan deze boeking gekoppeld is, vrij is van startDatum tm eindDatum
            Booking b = GetBookingByID(id);

            Boolean before = false;
            Boolean after = false;
            if (b.Capaciteit == capaciteit) {
                if (startDatum < b.StartDatum && eindDatum < b.StartDatum) {
                    before = true;
                }
                if (startDatum > b.StartDatum && eindDatum > b.EindDatum) {
                    after = true;
                }
            }
            return before && after;
            // hier verder gaan ofzo
        }
    }
}