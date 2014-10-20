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


    }
}
