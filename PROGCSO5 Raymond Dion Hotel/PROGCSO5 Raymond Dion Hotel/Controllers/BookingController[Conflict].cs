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
            return View();
        }

        public ActionResult ShowBookings()
        {
            return View();
        }
    }
}
