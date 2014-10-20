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
        public ActionResult CreateBooking() {
            return View(new Booking());
        }
        [HttpPost]
        public ActionResult CreateBooking(Booking booking) {
            var c_booking = bookingRepository.Create(booking);
            return RedirectToAction("Index", "Hotel");
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
        public ActionResult DeleteBooking(Booking booking) {
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
                if (b.CheckInDatum >= startDate && b.CheckInDatum <= endDate) {
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
