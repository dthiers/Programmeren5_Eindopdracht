using DomainModel;
using PROGCS05_Dion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROGCS05_Dion.Controllers {
    public class GuestController : Controller {
        private GuestRepository guestRepository;
        private BookingRepository bookingRepository;
        private Dropdowns d;

        public GuestController() {
            guestRepository = new GuestRepository();
            bookingRepository = new BookingRepository();
            d = new Dropdowns();
        }

        /*
        * Show all guests
        * */
        public ActionResult ShowAllGuests() {
            return View(guestRepository.GetAll());
        }

        /*
         * Show details for guest by Id
         * */
        public ActionResult DetailsGuest(int id) {
            var g_details = guestRepository.GetGuestByID(id);
            return View(g_details);
        }

        /*
         * Create new guest
         * */
        public ActionResult CreateGuest(int bookingId) {

            if (bookingRepository.CanAddGuest(bookingId))
            {
                TempData["BookingId"] = bookingId;
                ViewBag.sDrop = d.sDrop;
                return View(guestRepository.Get());
            }
            return RedirectToAction("ErrorGuest");
        }

        [HttpPost]
        [ActionName("CreateGuest")]
        public ActionResult CreateGuestPost(Guest guest) {
            int id = (int)TempData["BookingId"];
            guest.BookingId = id;
            if (guest != null) {
                guestRepository.Create(guest);
            }
            return RedirectToAction("ShowAllGuests");
        }

        /*
         * Edit guest
         * */
        public ActionResult EditGuest(int id) {
            var r_edit = guestRepository.GetGuestByID(id);
            ViewBag.sDrop = d.sDrop;
            return View(r_edit);
        }

        [HttpPost]
        [ActionName("EditGuest")]
        public ActionResult EditGuest(Guest guest, int bookingId) {

            if (guest != null) { guestRepository.Update(guest, bookingId); }

            return RedirectToAction("DetailsGuest", new { id = guest.Id });
        }

        /*
         * Delete guest by Id
         * */
        public ActionResult DeleteGuest(int id) {
            var g_delete = guestRepository.GetGuestByID(id);
            return View(g_delete);
        }

        [HttpPost]
        [ActionName("DeleteGuest")]
        public ActionResult DeleteGuestPost(int id) {
            var g_delete = guestRepository.GetGuestByID(id);
            if (g_delete != null) { guestRepository.Delete(g_delete); }

            return RedirectToAction("ShowAllGuests");
        }

        public ActionResult ErrorGuest()
        {
            return View();
        }
    }
}