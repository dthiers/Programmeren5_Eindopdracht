using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROGCS05_Dion.Models
{
    public class GuestRepository : IGuestRepository
    {
        private DatabaseContext dbContext;

        public GuestRepository()
        {
            dbContext = new DatabaseContext();
        }

        public Guest Get()
        {
            return new Guest();
        }

        public List<Guest> GetAll()
        {
            return dbContext.Guests.ToList();
        }

        public List<Guest> GetGuestByBookingId(int bookingId)
        {
            List<Guest> g_BookingId = new List<Guest>();
            foreach (Guest g in GetAll())
            {
                if (g.BookingId == bookingId)
                {
                    g_BookingId.Add(g);
                }
            }
            return g_BookingId;
        }

        public Guest GetGuestByID(int id)
        {
           return dbContext.Guests.Where(g => g.Id == id).FirstOrDefault();
        }

        public Guest Create(Guest guest)
        {
            dbContext.Guests.Add(guest);
            dbContext.SaveChanges();
            return guest;
        }

        public Guest Update(Guest guest)
        {
            Guest g_update = dbContext.Guests.Where(g => g.Id == guest.Id).FirstOrDefault();
            if (g_update != null)
            {
                dbContext.Entry(g_update).CurrentValues.SetValues(guest);
                dbContext.SaveChanges();
            }
            return guest;
        }

        public void Delete(Guest guest)
        {
            Guest g_delete = dbContext.Guests.Where(g => g.Id == guest.Id).FirstOrDefault();

            if (g_delete != null)
            {
                dbContext.Guests.Remove(g_delete);
                dbContext.SaveChanges();
            }
        }

        public Booking AddBookerAsGuest(InformationViewModel guest, Booking booking)
        {
            Guest g = new Guest();

            g.BookingId = booking.Id;
            g.Voornaam = guest.Voornaam;
            g.Tussenvoegsel = guest.Tussenvoegsel;
            g.Achternaam = guest.Achternaam;
            g.GeboorteDatum = guest.GeboorteDatum;
            g.ManOfVrouw = guest.ManOfVrouw;
            g.Adres = guest.Adres;
            g.Postcode = guest.Postcode;
            g.Woonplaats = guest.Woonplaats;
            g.Email = guest.Email;

            dbContext.Guests.Add(g);
            dbContext.SaveChanges();

            booking.GuestId = g.Id;

            return booking;
        }
    }
}