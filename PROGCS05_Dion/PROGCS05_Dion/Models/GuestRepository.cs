using DomainModel;
using PROGCS05_Dion.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROGCS05_Dion.Models {
    public class GuestRepository : IGuestRepository {
        private DatabaseContext dbContext;

        public GuestRepository() {
            dbContext = new DatabaseContext();
        }

        public Guest Get() {
            return new Guest();
        }

        public List<Guest> GetAll() {
            return dbContext.Guests.ToList();
        }

        public Guest GetGuestByID(int id) {
            return dbContext.Guests.Where(g => g.Id == id).FirstOrDefault();
        }

        public Guest Create(Guest guest) {
            dbContext.Guests.Add(guest);
            dbContext.SaveChanges();
            return guest;
        }

        public Guest Update(Guest guest, int bookingId) {
            Guest g_update = dbContext.Guests.Where(g => g.Id == guest.Id).FirstOrDefault();
            if (g_update != null) {
                dbContext.Entry(g_update).CurrentValues.SetValues(guest);
                dbContext.SaveChanges();
            }
            return guest;
        }

        public void Delete(Guest guest) {
            Guest g_delete = dbContext.Guests.Where(g => g.Id == guest.Id).FirstOrDefault();

            if (g_delete != null) {
                dbContext.Guests.Remove(g_delete);
                dbContext.SaveChanges();
            }
        }

        public void AddBookerAsGuest(Booking booking) {
            Guest g = new Guest();
          
            g.BookingId = booking.Id;
            g.Voornaam = booking.Voornaam;
            g.Tussenvoegsel = booking.Tussenvoegsel;
            g.Achternaam = booking.Achternaam;
            g.GeboorteDatum = booking.GeboorteDatum;
            g.ManOfVrouw = booking.ManOfVrouw;
            g.Adres = booking.Adres;
            g.Postcode = booking.Postcode;
            g.Woonplaats = booking.Woonplaats;
            g.Email = booking.Email;

            dbContext.Guests.Add(g);
            dbContext.SaveChanges();
        }
    }
}