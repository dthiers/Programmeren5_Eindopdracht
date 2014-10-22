using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROGCS05_Dion.Models {
    public interface IBookingRepository {
        Booking Get();
        List<Booking> GetAll();
        Booking Create(Booking booking);
        Booking Update(Booking booking, int roomId);
        void Delete(Booking booking);
    }
}