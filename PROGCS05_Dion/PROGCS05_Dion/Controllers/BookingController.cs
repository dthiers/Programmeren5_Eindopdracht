using DomainModel;
using PROGCS05_Dion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROGCS05_Dion.Controllers
{
    public class BookingController : Controller
    {
        private BookingRepository bookingRepository;
        public BookingController() {
            bookingRepository = new BookingRepository();
        }


        /*
         * Show all bookings
         * */
        public ActionResult ShowAllBookings() {
            return View(bookingRepository.GetAll());
        }

        /*
         * Create booking
         * */
        public ActionResult CreateBooking()
        {
            //We maken een view model waar de gebruiker zijn eerste informatie in kan vullen
            StartBookingViewModel model = new StartBookingViewModel();
            //Dit geven we mee aan de view
            return View(model);
        }

        public ActionResult ChooseRoom(StartBookingViewModel booking)
        {
            //Als een gebruiker de informatie heeft ingevuld ontvangen we dat model weer in deze action.

            //op basis van de gegevens die de gebruiker heeft ingevoerd, gaan we filteren op de lijst van kamers.
            //We willen een lisjt van kamers die voldoet aan de eisen van de gebruiker.

            //database wordt repository bovenaan.
            var model = dbContext.Rooms
                .Include(m => m.BookingList)
                .Where(k => k.Capaciteit == booking.Capaciteit);

            //Ik geef de begin en eind datum mee aan de view omdat ik deze later in het 'proces' nog wil gebruikern
            ViewBag.BeginDatum = booking.BeginDatum;
            ViewBag.EindDatum = booking.EindDatum;
            ViewBag.Capaciteit = booking.Capaciteit;

            return View(model);
        }

        public ActionResult InsertGuestInfo(int kamerId, DateTime beginDatum, DateTime eindDatum, int capaciteit)
        {
            InformationViewModel model = new InformationViewModel();

            /*
             * dit wouden we eerst doen maar om een of ander reden geeft dit null als ik informatie opvraag bij BookKamer. 
            model.BookingInformation.BeginDatum = beginDatum;
            model.BookingInformation.EindDatum = eindDatum;

             * */
            ViewBag.KamerId = kamerId;
            ViewBag.BeginDatum = beginDatum;
            ViewBag.EindDatum = eindDatum;
            ViewBag.Capaciteit = capaciteit;

            return View(model);
        }

        public ActionResult BookRoom(InformationViewModel informatie, int kamerId, DateTime beginDatum, DateTime eindDatum, int capaciteit)
        {
            //Ik maak een DateTime voor wanneer het hoogtarief start en wanneer het hoogtarief eindigt.
            DateTime hoogTariefBegin = new DateTime(2015, 6, 1);
            DateTime hoogTariefEind = new DateTime(2015, 8, 31);

            //Ik heb nu alle informatie die ik nodig heb om een booking te maken
            //Ik maak mijn object en sla hem op in de database
            var booking = new Booking();

            booking.EindDatum = eindDatum;
            booking.StartDatum = beginDatum;
            booking.RoomId = kamerId;
            booking.Voornaam = informatie.Voornaam;
            booking.Tussenvoegsel = informatie.Tussenvoegsel;
            booking.Achternaam = informatie.Achternaam;
            booking.GeboorteDatum = informatie.GeboorteDatum;
            booking.ManOfVrouw = informatie.ManOfVrouw;
            booking.Adres = informatie.Adres;
            booking.Postcode = informatie.Postcode;
            booking.Woonplaats = informatie.Woonplaats;
            booking.Email = informatie.Email;

            // bereken prijs
            int prijs = 0;

            if (capaciteit == 2)
            {
                prijs = 20;
            }
            else if (capaciteit == 3)
            {
                prijs = 30;
            }
            else
            {
                prijs = 40;
            }

            for (DateTime date = booking.StartDatum; date <= booking.EindDatum; date = date.AddDays(1))
            {
                if (booking.StartDatum >= hoogTariefBegin && booking.EindDatum <= hoogTariefEind)
                {
                    prijs += 90;
                }
                else
                {
                    prijs += 60;
                }
            }

            booking.Prijs = prijs;

            dbContext.Bookingen.Add(booking);
            dbContext.SaveChanges();

            //Op het einde toon ik de opgeslage booking aan de gebruiker
            return View(booking);
        }


        /*
         * Edit booking
         * */
        public ActionResult EditBooking(int id) {
            var b_edit = bookingRepository.GetBookingByID(id);
            return View(b_edit);
        }
        [HttpPost]
        public ActionResult EditBooking(Booking booking) {
            var b_edit = bookingRepository.Update(booking);
            return RedirectToAction("ShowAllBookings");
        }

        /*
         * Show details of booking
         * */
        public ActionResult DetailsBooking(int id) {
            var b_details = bookingRepository.GetBookingByID(id);
            return View(b_details);
        }

        /*
         * Delete booking
         * */
        public ActionResult DeleteBooking(int id) {
            var b_delete = bookingRepository.GetBookingByID(id);
            return View(b_delete);
        }
        [HttpPost]
        [ActionName("DeleteBooking")]
        public ActionResult DeleteBookingPost(int id) {
            var booking = bookingRepository.GetBookingByID(id);
            if (booking != null) {
                bookingRepository.Delete(booking);
            }
            return RedirectToAction("ShowAllBookings");
        }

        /*
         * Show booking within period
         * */
        public ActionResult SelectBookingPeriod() {
            return View();
        }
        [HttpPost]
        public ActionResult SelectBookingPeriod(FormCollection form) {
            DateTime startDate = Convert.ToDateTime(Request["CheckInDatum"]);
            DateTime endDate = Convert.ToDateTime(Request["CheckOutDatum"]);

            List<Booking> bookingListForPeriod = new List<Booking>();

            foreach (Booking b in bookingRepository.GetAll()) {
                //if (b.CheckInDatum >= startDate && b.CheckInDatum <= endDate) {
                //    bookingListForPeriod.Add(b);
                //}
            }
            IEnumerable<Booking> bookings = bookingListForPeriod;
            TempData["PeriodBookings"] = bookings;

            if (bookings != null) {
                return RedirectToAction("ShowBookingsForPeriod");
            }
            return RedirectToAction("ShowAllBookings");
        }

        public ActionResult ShowBookingsForPeriod() {
            var periodBookings = TempData["PeriodBookings"];
            return View(periodBookings);
        }
    }
}
