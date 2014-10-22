using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;
using PROGCS05_Dion.Controllers;

namespace PROGCS05_Dion.Models {
    public class RoomRepository : IRoomRepository {

        private DatabaseContext dbContext;

        public RoomRepository() {
            dbContext = new DatabaseContext();
        }

        /*
         * This method does nothing so far but return a new empty room
         * */
        public Room Get() {
            return new Room();
        }

        /*
         * Return list of all rooms (in DbSet)
         * */
        public List<Room> GetAll() {
            return dbContext.Rooms.ToList();
        }

        /*
         * Get room by ID
         * */
        public Room GetRoomByID(int id) {
            return dbContext.Rooms.Where(r => r.Id == id).FirstOrDefault();
        }

        /*
         * Create room and save to DbSet
         * */
        public Room Create(Room room) {
            if (room != null) { dbContext.Rooms.Add(room); }
            dbContext.SaveChanges();
            return room;
        }

        /*
         * Update room in DbSet
         * */
        public Room Update(Room room) {
            Room r_update = dbContext.Rooms.Where(r => r.Id == room.Id).FirstOrDefault();

            if (r_update != null) {
                dbContext.Entry(r_update).CurrentValues.SetValues(room);
            }
            dbContext.SaveChanges();
            return room;
        }
        
        /*
         * Delete room from DbSet
         * */
        public void Delete(Room room) {
            // Toch weer even die lambda proberen, ziet er netter uit imo
            Room r_delete = dbContext.Rooms.Where(r => r.Id == room.Id).FirstOrDefault();

            if (r_delete != null) {
                dbContext.Rooms.Remove(r_delete);
            }
            dbContext.SaveChanges();
        }
        public Boolean CanAddRoom()
        {
            int counter = 0;
            foreach (Room r in dbContext.Rooms.ToList()){
                counter++;
            }
            if (counter == 12)
            {
                return false;
            }

            return true;
        }
    }
}