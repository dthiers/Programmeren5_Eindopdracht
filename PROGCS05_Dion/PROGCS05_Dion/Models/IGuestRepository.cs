using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROGCS05_Dion.Models {
    interface IGuestRepository {
        Guest Get();
        List<Guest> GetAll();
        Guest GetGuestByID(int id);
        Guest Create(Guest guest);
        Guest Update(Guest guest, int bookingId);
        void Delete(Guest guest);

    }
}
