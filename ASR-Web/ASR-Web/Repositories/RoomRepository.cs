using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASR_Web.Data;
using ASR_Web.Models;

namespace ASR_Web.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public ApplicationDbContext _db;

        public RoomRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IEnumerable<Room> All()
        {
            return _db.Room.ToList();
        }

        public Room Create(Room room)
        {
            var dbRoom = Find(room.RoomID);

            if (dbRoom == null)
            {
                _db.Room.Add(room);
                _db.SaveChanges();
                return room;
            }
            return null;
        }

        public bool Delete(Room room)
        {
            var dbRoom = Find(room.RoomID);
            if (dbRoom != null)
            {
                _db.Room.Remove(dbRoom);
                _db.SaveChanges();
            }

            if (Find(room.RoomID) == null)
            {
                return true;
            }

            return false;
        }

        public Room Find(string roomID)
        {
            return _db.Room.FirstOrDefault(s => s.RoomID == roomID.ToUpper());
        }

        public Room Update(Room room)
        {
            var dbRoom = Find(room.RoomID);
            if (dbRoom != null)
            {
                dbRoom.RoomID = room.RoomID;
                _db.SaveChanges();
            }
            return dbRoom;
        }

        public Room Validate(Room room)
        {
            throw new NotImplementedException();
        }
    }
}
