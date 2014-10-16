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
    }
}
