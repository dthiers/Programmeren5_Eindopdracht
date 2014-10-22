using DomainModel;
using PROGCS05_Dion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PROGCS05_Dion.Controllers
{
    public class BookingController : Controller
    {
        private BookingRepository bookingRepository;
        private GuestRepository guestRepository;

        public BookingController() {
            bookingRepository = new BookingRepository();
            guestRepository = new GuestRepository();
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
        public ActionResult CreateBooking(string errorMessage)
        {
            //We maken een view model waar de gebruiker zijn eerste informatie in kan vullen
            StartBookingViewModel model = new StartBookingViewModel();
            //Errormessage als er een verkeerde capaciteit is gekozen
            ViewBag.ErrorMessage = errorMessage;
            //Dit geven we mee aan de view
            return View(model);
        }

        public ActionResult ChooseRoom(StartBookingViewModel booking)
        {
            //Check of de capaciteit geldig is.
            Boolean check = false;

            if (booking.Capaciteit == 2 || booking.Capaciteit == 3 || booking.Capaciteit == 5)
            {
                check = true;
            }
            
            if (check)
            {
                //Als een gebruiker de informatie heeft ingevuld ontvangen we dat model weer in deze action.

                //op basis van de gegevens die de gebruiker heeft ingevoerd, gaan we filteren op de lijst van kamers.
                //We willen een lisjt van kamers die voldoet aan de eisen van de gebruiker.

                //database wordt repository bovenaan.
                var model = bookingRepository.GetRooms()
                    .Include(m => m.BookingList)
                    .Where(k => k.Capaciteit == booking.Capaciteit);
                // .Where(j => j.BookingList.ToList().ForEach(i => i.StartDatum != booking.BeginDatum));

                //Ik geef de begin en eind datum mee aan de view omdat ik deze later in het 'proces' nog wil gebruikern
                ViewBag.StartDate = booking.BeginDatum;
                ViewBag.EndDate = booking.EindDatum;
                ViewBag.Capacity = booking.Capaciteit;

                return View(model);
            }
            else
            {
                return RedirectToAction("CreateBooking", new { errorMessage = "Kies een capaciteit van 2, 3 of 5!" });
            }
        }

        public ActionResult InsertGuestInfo(int roomId, DateTime startDate, DateTime endDate, int capacity)
        {
            InformationViewModel model = new InformationViewModel();

            /*
             * dit wouden we eerst doen maar om een of ander reden geeft dit null als ik informatie opvraag bij BookKamer. 
            model.BookingInformation.BeginDatum = beginDatum;
            model.BookingInformation.EindDatum = eindDatum;

            Als ik in de andere ActionResult kom, dan is alles ingevuld behalve de begin en eind datum die je in het begin moest kiezen
             * */
            ViewBag.RoomId = roomId;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.Capacity = capacity;

            return View(model);
        }

        public ActionResult BookedRoom(InformationViewModel information, int roomId, DateTime startDate, DateTime endDate, int capacity)
        {

            if (ModelState.IsValid) {

                //Ik heb nu alle informatie die ik nodig heb om een booking te maken
                //Ik maak mijn object en sla hem op in de database
                var booking = new Booking();

                booking.StartDatum = startDate;
                booking.EindDatum = endDate;
                booking.RoomId = roomId;
                booking.Voornaam = information.Voornaam;
                booking.Tussenvoegsel = information.Tussenvoegsel;
                booking.Achternaam = information.Achternaam;
                booking.GeboorteDatum = information.GeboorteDatum;
                booking.ManOfVrouw = information.ManOfVrouw;
                booking.Adres = information.Adres;
                booking.Postcode = information.Postcode;
                booking.Woonplaats = information.Woonplaats;
                booking.Email = information.Email;
                booking.BankrekeningNummer = information.Bankrekeningnummer;
                booking.Capaciteit = capacity;

                // bereken prijs
                // public int CalculatePrice(int capacity, DateTime startDatum, DateTime eindDatum) {
                int prijs = bookingRepository.CalculatePrice(capacity, booking.StartDatum, booking.EindDatum);

                booking.Prijs = prijs;

                // Het idee was om de booking mee te geven in zonder TempData, maar dat lukte niet omdat het anders null terug gaf
                // op sommige plekken.

                TempData["booking"] = booking;
                //Op het einde toon ik de opgeslage booking aan de gebruiker

                return View(booking);
            }
            else {
                return RedirectToAction("InsertGuestInfo", new { roomId = roomId, startDate = startDate, endDate = endDate, capacity = capacity });
            }
        }

        public ActionResult Invoice()
        {
            // genereer een random factuurnummer
            Booking booking = (Booking)TempData["booking"];

            Random random = new Random();
            string nummer = "";

            for(int i = 0; i < 6; i++)
            {
                nummer = nummer + random.Next(9);
            }

            int factuurNummer = Convert.ToInt32(nummer);

            booking.FactuurNummer = factuurNummer;

            bookingRepository.Create(booking);

            // voeg deze persoon toe als gast
            guestRepository.AddBookerAsGuest(booking);

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

            int nieuwPrijs = bookingRepository.CalculatePrice(booking.Capaciteit, booking.StartDatum, booking.EindDatum);
            booking.Prijs = nieuwPrijs;

            var b_edit = bookingRepository.Update(booking);
            return RedirectToAction("ShowAllBookings");
        }

        /*
         * Show details of booking
         * */
        public ActionResult DetailsBooking(int id) {
            var b_details = bookingRepository.GetBookingByID(id);

            ViewData["g_BookingId"] = guestRepository.GetGuestByBookingId(id);

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
            DateTime startDate = Convert.ToDateTime(Request["StartDatum"]);
            DateTime endDate = Convert.ToDateTime(Request["EindDatum"]);

            List<Booking> bookingListForPeriod = new List<Booking>();

            foreach (Booking b in bookingRepository.GetAll()) {
                if (b.StartDatum >= startDate && b.StartDatum <= endDate
                    || b.EindDatum >= startDate && b.EindDatum <= endDate) {
                    bookingListForPeriod.Add(b);
                }
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
