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

            return View();
        }

        /*
        [HttpPost]
        public ActionResult CreateBooking(Booking booking)
        {
            if (booking != null)
            {
                return RedirectToAction("PriceDetail", "Booking", new { newBooking = booking });
            }

            return View();
        }
         * */

        public ActionResult ShowBookings()
        {
            IEnumerable<Booking> bookings = bookingRepository.GetAll();
            return View(bookings);
        }

        // als de maanden van de checkindatum en gekozen datum hetzelfe zijn, toon alle boekingen in die maand.
        public ActionResult ShowBookingPeriod(IEnumerable<Booking> bookings)
        {
            return View(bookings);
        }

        public ActionResult CreateBookingPeriod()
        {
            return View(new Booking());
        }

        [HttpPost]
        public ActionResult CreateBookingPeriod(Booking booking)
        {
            if (booking != null)
            {
                List<Booking> bookingList = new List<Booking>();
                IEnumerable<Booking> bookings = null;

                foreach (Booking b in bookingRepository.GetAll())
                {
                    if (b.CheckInDatum >= booking.CheckInDatum && b.CheckOutDatum <= booking.CheckOutDatum)
                    {
                        bookingList.Add(b);
                    }
                }

                bookings = bookingList;

                return RedirectToAction("ShowBookingPeriod", new { bookingEnum = bookings });
            }
            return View();
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

        /*

        // hier nog een view van maken
        public ActionResult PriceDetail(Booking newBooking)
        {
            Booking getBooking = bookingRepository.CalculatePrice(newBooking);
            return View(getBooking);
        }

        [HttpPost]
        public ActionResult PriceDetail(Booking booking)
        {
            if (booking != null)
            {
                bookingRepository.AddBooking(booking);
                return RedirectToAction("Index", "Hotel");
            }

            return View(new Booking());
        }
         * */
    }
}
