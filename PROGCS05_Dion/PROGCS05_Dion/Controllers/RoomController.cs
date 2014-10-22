using DomainModel;
using PROGCS05_Dion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROGCS05_Dion.Controllers
{
    public class RoomController : Controller
    {
        private RoomRepository roomRepository;
        public RoomController() {
            roomRepository = new RoomRepository();
        }

        /*
         * Show all rooms
         * */
        public ActionResult ShowAllRooms() {
            return View(roomRepository.GetAll());
        }

        /*
         * Show details for room by Id
         * */
        public ActionResult DetailsRoom(int id) {
            var r_details = roomRepository.GetRoomByID(id);
            return View(r_details);
        }
        /*
        [HttpPost]
        [ActionName("DetailsRoom")]
        public ActionResult DetailsRoomPost() {
            if (Request.Form["Go to booking"] != null) {
                return RedirectToAction("DetailsBooking", "Booking", new {id=Request})
            }
        }
         * */

        /*
         * Create new room
         * */
        public ActionResult CreateRoom() {
            return View(roomRepository.Get());
        }

        [HttpPost]
        [ActionName("CreateRoom")]
        public ActionResult CreatRoomPost(Room room) {
            if (room != null) { roomRepository.Create(room); }
            return RedirectToAction("ShowAllRooms");
        }

        /*
         * Edit room
         * */
        public ActionResult EditRoom(int id) {
            var r_edit = roomRepository.GetRoomByID(id);
            return View(r_edit);
        }

        [HttpPost]
        [ActionName("EditRoom")]
        public ActionResult EditRoom(Room room) {

            if (room != null) { roomRepository.Update(room); }

            return RedirectToAction("DetailsRoom", new { id = room.Id});
        }

        
        /*
         * Delete room by Id
         * */
        public ActionResult DeleteRoom(int id) {
            var r_delete = roomRepository.GetRoomByID(id);
            return View(r_delete);
        }

        [HttpPost]
        [ActionName("DeleteRoom")]
        public ActionResult DeleteRoomPost(int id) {
            var r_delete = roomRepository.GetRoomByID(id);
            if (r_delete != null) { roomRepository.Delete(r_delete); }

            return RedirectToAction("ShowAllRooms");
        }
    }
}
