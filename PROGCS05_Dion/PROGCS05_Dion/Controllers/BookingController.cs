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
        private Dropdowns d;
        public BookingController() {
            bookingRepository = new BookingRepository();
            guestRepository = new GuestRepository();
            d = new Dropdowns();
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
            ViewBag.cDrop = d.cDrop;
            //Dit geven we mee aan de view
            return View(model);
        }

        public ActionResult ChooseRoom(StartBookingViewModel booking)
        {
            if (booking.EindDatum <= booking.BeginDatum) {
                return RedirectToAction("CreateBooking", "Booking", new { errorMessage = "Reverse spacetimecontinuum doesn't exist" });
            }

            int capacity = Convert.ToInt32(booking.Capaciteit);

            var model = bookingRepository.GetRooms()
                    .Include(m => m.BookingList)
                    .Where(k => k.Capaciteit == capacity);
            
            ViewBag.StartDate = booking.BeginDatum;
            ViewBag.EndDate = booking.EindDatum;
            ViewBag.Capacity = booking.Capaciteit;

            return View(model);
            /*
             *
             * 
             *  List<Booking> b_list = new List<Booking>();

            foreach (Room r in model) {
                foreach (Booking b in r.BookingList) {
                    b_list.Add(b);
                }
            }
            List<Room> r_list = new List<Room>();

            foreach (Room r in model) {
                Boolean emptyRoom = true;
                foreach (Booking b in b_list) {
                    if (r.Id == b.Room.Id) {
                        emptyRoom = false;
                    }
                }
                if (emptyRoom) {
                    r_list.Add(r);
                }
            }
            // HOLY MOLY
            foreach (var number in model) {
                Boolean canAdd = false;
                foreach (Booking b in b_list) {
                    if ((booking.BeginDatum < b.StartDatum && booking.EindDatum < b.StartDatum) ||
                        (booking.BeginDatum > b.EindDatum && booking.EindDatum > b.EindDatum)) {
                        canAdd = true;
                    }
                    if (canAdd) {

                        int c = Convert.ToInt32(booking.Capaciteit);
                        if (b.Room.Capaciteit == c) {
                            Boolean added = false;
                            if (r_list.Count == 0) {
                                r_list.Add(b.Room);
                            }
                            else {
                                foreach (Room room in r_list) {
                                    if (room.Id == b.Room.Id) {
                                        if (!added) {
                                            added = true;
                                        }
                                    }
                                }
                                if (!added) {
                                    r_list.Add(b.Room);
                                }
                            }
                        }
                    }
                }
            }
             * b = StartDatum 
             * booking = BeginDatum
             

           
            // HOLY MOLY
            foreach (var number in model) {
                Boolean cantAdd = false;
               
                    foreach (Booking b in b_list) {
                        if (b.StartDatum == booking.BeginDatum || b.EindDatum == booking.EindDatum)
                            cantAdd = true; // If any set is the same time, then by default there must be some overlap. 

                        if (b.StartDatum < booking.BeginDatum) {
                            if (b.EindDatum > booking.BeginDatum && b.EindDatum < booking.EindDatum)
                                cantAdd = true; // Condition 1

                            if (b.EindDatum > booking.EindDatum)
                                cantAdd = true; // Condition 3
                        }
                        else {
                            if (booking.EindDatum > b.StartDatum && booking.EindDatum < b.EindDatum)
                                cantAdd = true; // Condition 2

                            if (booking.EindDatum > b.EindDatum)
                                cantAdd = true; // Condition 4
                        }
                    }

                    if (!cantAdd) {

                        int c = Convert.ToInt32(booking.Capaciteit);
                        if (b.Room.Capaciteit == c) {
                            Boolean added = false;
                            if (r_list.Count == 0) {
                                r_list.Add(b.Room);
                            }
                            else {
                                foreach (Room room in r_list) {
                                    if (room.Id == b.Room.Id) {
                                        if (!added) {
                                            added = true;
                                        }
                                    }
                                }
                                if (!added) {
                                    r_list.Add(b.Room);
                                }
                            }
                        }
                    }
                }
            }

        /*
            //Boolean cantAdd = false;
            foreach (Booking b in b_list) {
                if (b.StartDatum == booking.BeginDatum || b.EindDatum == booking.EindDatum)
                    cantAdd = true; // If any set is the same time, then by default there must be some overlap. 

                if (b.StartDatum < booking.BeginDatum) {
                    if (b.EindDatum > booking.BeginDatum && b.EindDatum < booking.EindDatum)
                        cantAdd= true; // Condition 1

                    if (b.EindDatum > booking.EindDatum)
                        cantAdd =true; // Condition 3
                }
                else {
                    if (booking.EindDatum > b.StartDatum && booking.EindDatum < b.EindDatum)
                        cantAdd =true; // Condition 2

                    if (booking.EindDatum > b.EindDatum)
                        cantAdd= true; // Condition 4
                }
            }
         * 
         * */

        }

        public ActionResult InsertGuestInfo(int roomId, DateTime startDate, DateTime endDate, int capacity)
        {
            ViewBag.sDrop = d.sDrop;
            InformationViewModel model = new InformationViewModel();

            Room room = new Room();
            room.Capaciteit = capacity;
            room.Id = roomId;

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            TempData["Room"] = room;

            return View(model);
        }

        public ActionResult BookedRoom(InformationViewModel information,DateTime startDate, DateTime endDate)
        {
            Room room = (Room)TempData["Room"];
           
            var booking = new Booking();

            booking.Room = room;
            booking.StartDatum = startDate;
            booking.EindDatum = endDate;
            booking.RoomId = room.Id;
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
            booking.Capaciteit = room.Capaciteit;

            // bereken prijs
            // public int CalculatePrice(int capacity, DateTime startDatum, DateTime eindDatum) {
            int prijs = bookingRepository.CalculatePrice(booking.Capaciteit, booking.StartDatum, booking.EindDatum);

            booking.Prijs = prijs;

            // Het idee was om de booking mee te geven in zonder TempData, maar dat lukte niet omdat het anders null terug gaf
            // op sommige plekken.

            TempData["booking"] = booking;
            //Op het einde toon ik de opgeslage booking aan de gebruiker

            return View(booking);
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
            ViewBag.cDrop = d.cDrop;
            ViewBag.sDrop = d.sDrop;
            return View(b_edit);
        }
        [HttpPost]
        public ActionResult EditBooking(Booking booking, int roomId) {

            int nieuwPrijs = bookingRepository.CalculatePrice(booking.Capaciteit, booking.StartDatum, booking.EindDatum);
            booking.Prijs = nieuwPrijs;

            var b_edit = bookingRepository.Update(booking, roomId);

            Guest guest = guestRepository.GetGuestByID(booking.Id);
      //      guestRepository.Update()
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
