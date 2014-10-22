using DomainModel;
using PROGCS05_Dion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROGCS05_Dion.Controllers
{
    public class GuestController : Controller
    {
        private GuestRepository guestRepository;

        public GuestController()
        {
            guestRepository = new GuestRepository();
        }

        /*
        * Show all guests
        * */
        public ActionResult ShowAllGuests()
        {
            return View(guestRepository.GetAll());
        }

        /*
         * Show details for guest by Id
         * */
        public ActionResult DetailsGuest(int id)
        {
            var g_details = guestRepository.GetGuestByID(id);
            return View(g_details);
        }

        /*
         * Create new guest
         * */
        public ActionResult CreateGuest(int bookingId)
        {
            TempData["BookingId"] = bookingId;
            return View(guestRepository.Get());
        }

        [HttpPost]
        [ActionName("CreateGuest")]
        public ActionResult CreatRoomPost(Guest guest)
        {
            if (guest != null) {
                guest.BookingId = (int)TempData["BookingId"];
                guestRepository.Create(guest); 
            }
            return RedirectToAction("ShowAllGuests");
        }

        /*
         * Edit guest
         * */
        public ActionResult EditGuest(int id)
        {
            var r_edit = guestRepository.GetGuestByID(id);
            return View(r_edit);
        }

        [HttpPost]
        [ActionName("EditGuest")]
        public ActionResult EditGuest(Guest guest)
        {

            if (guest != null) { guestRepository.Update(guest); }

            return RedirectToAction("DetailsGuest", new { id = guest.Id });
        }

        /*
         * Delete guest by Id
         * */
        public ActionResult DeleteGuest(int id)
        {
            var g_delete = guestRepository.GetGuestByID(id);
            return View(g_delete);
        }

        [HttpPost]
        [ActionName("DeleteGuest")]
        public ActionResult DeleteGuestPost(int id)
        {
            var g_delete = guestRepository.GetGuestByID(id);
            if (g_delete != null) { guestRepository.Delete(g_delete); }

            return RedirectToAction("ShowAllGuests");
        }
    }
}