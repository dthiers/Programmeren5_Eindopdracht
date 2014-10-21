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

        // 2e methode om prijs uit te rekenen omdat je hier niet meer kijkt naar de capaciteit omdat dat eenmalige kosten zijn.
        public int ReCalculatePrice(int capacity, Nullable<System.DateTime> startDatum, Nullable<System.DateTime> eindDatum)
        {
            //Ik maak een DateTime voor wanneer het hoogtarief start en wanneer het hoogtarief eindigt.
            DateTime hoogTariefBegin = new DateTime(2015, 6, 1);
            DateTime hoogTariefEind = new DateTime(2015, 8, 31);

            // ik maak een DateTime die de value pakt van het Nonnullable startDatum.
            DateTime deStartDatum = startDatum.Value;

            int prijs = 0;

            for (DateTime date = deStartDatum; date <= eindDatum; date = date.AddDays(1))
            {
                if (startDatum >= hoogTariefBegin && eindDatum <= hoogTariefEind)
                {
                    prijs += 90;
                }
                else
                {
                    prijs += 60;
                }
            }
            return prijs;
        }
    }
}