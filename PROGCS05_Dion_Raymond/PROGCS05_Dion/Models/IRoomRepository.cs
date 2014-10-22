using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace PROGCS05_Dion.Models {
    public interface IRoomRepository {
        Room Get();
        List<Room> GetAll();
        Room Create(Room room);
        Room Update(Room room);
        void Delete(Room room);
    }
}
