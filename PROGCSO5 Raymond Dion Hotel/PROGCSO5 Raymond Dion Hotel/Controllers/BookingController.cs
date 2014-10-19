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

        private BookingRepository bookingRepository;
        private BookingContext bookingContext;

        public BookingController() {
            bookingRepository = new BookingRepository();
            bookingContext = new BookingContext();
        }

        public ActionResult CreateBooking()
        {
            return View(new Booking());
        }

        [HttpPost]
        public ActionResult CreateBooking(Booking booking)
        {
            if (booking != null)
            {
                bookingRepository.AddBooking(booking);
                return RedirectToAction("Index", "Hotel");
            }

            return View(new Booking());
        }

        public ActionResult ShowBookings()
        {
            IEnumerable<Booking> bookings = bookingRepository.GetAll();
            return View(bookings);
        }

        // als de maanden van de checkindatum en gekozen datum hetzelfe zijn, toon alle boekingen in die maand.
        public ActionResult ShowBookingPeriod(DateTime date)
        {
            List<Booking> booking = new List<Booking>();
            IEnumerable<Booking> bookings = null;

            foreach (Booking b in bookingRepository.GetAll())
            {
                if (b.CheckInDatum.Month == date.Month)
                {
                    booking.Add(b);
                }
            }

            bookings = booking;

            return View(bookings);
        }




        /*
         * Edit booking
         * */
        public ActionResult EditBooking(int id) {
            Booking editBooking = bookingRepository.GetBookingByID(id);

            return View(editBooking);
        }

        [HttpPost]
        public ActionResult EditBooking(Booking editBooking) {
            if (editBooking != null) {
                bookingRepository.EditBooking(editBooking);
            }
            return RedirectToAction("ShowBookings");
        }

        /*
         * Delete booking
         * */
        public ActionResult DeleteBooking(int id) {
            Booking deleteBooking = bookingRepository.GetBookingByID(id);

            return View(deleteBooking);
        }

        [HttpPost]
        public ActionResult DeleteBooking(Booking deleteBooking) {
            bookingRepository.DeleteBooking(deleteBooking);

            return RedirectToAction("ShowBookings");
        }

        /*
         * Details booking
         * */
        public ActionResult DetailsBooking(int id) {
            Booking detailsBooking = bookingRepository.GetBookingByID(id);

            return View(detailsBooking);
        }
    }
}
