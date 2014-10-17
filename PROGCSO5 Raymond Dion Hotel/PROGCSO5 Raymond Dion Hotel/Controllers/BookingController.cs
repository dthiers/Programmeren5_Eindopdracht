using PROGCSO5_Raymond_Dion_Hotel.Models;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROGCSO5_Raymond_Dion_Hotel.Controllers
{
    public class BookingController : Controller
    {
        //
        // GET: /Booking/

        private BookingRepository bookingList = new BookingRepository();

        public ActionResult CreateBooking()
        {
            return View(new Booking());
        }

        [HttpPost]
        public ActionResult CreateBooking(Booking booking)
        {
            if (booking != null)
            {
                bookingList.AddBooking(booking);
                return RedirectToAction("Index", "Hotel");
            }

            return View(new Booking());
        }

        public ActionResult ShowBookings()
        {
            IEnumerable<Booking> bookings = bookingList.GetAll();
            return View(bookings);
        }

        // als de maanden van de checkindatum en gekozen datum hetzelfe zijn, toon alle boekingen in die maand.
        public ActionResult ShowBookingPeriod(DateTime date)
        {
            List<Booking> booking = new List<Booking>();
            IEnumerable<Booking> bookings = null;

            foreach (Booking b in bookingList.GetAll())
            {
                if (b.CheckInDatum.Month == date.Month)
                {
                    booking.Add(b);
                }
            }

            bookings = booking;

            return View(bookings);
        }

        // ga naar de pagina waar je de gekozen boeking kunt wijzigen.
        public ActionResult EditBooking(int sleutel)
        {
            Booking editBooking = bookingList.GetBooking(sleutel);

            return View(editBooking);
        }

        [HttpPost]
        public ActionResult EditBooking(Booking booking)
        {
            bookingList.EditBooking(booking);
            return RedirectToAction("ShowBookings");
        }

        public ActionResult DeleteBooking(int sleutel)
        {
            Booking booking = bookingList.GetBooking(sleutel);

            return View(booking);
        }

        [HttpPost]
        public ActionResult DeleteBooking(Booking booking)
        {
            bookingList.DeleteBooking(booking);
            return RedirectToAction("ShowBookings");
        }
    }
}
