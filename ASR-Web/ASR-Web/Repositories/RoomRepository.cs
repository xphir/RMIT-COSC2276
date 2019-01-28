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
            throw new NotImplementedException();
        }

        public bool Delete(Room room)
        {
            throw new NotImplementedException();
        }

        public Room Find(string roomID)
        {
            throw new NotImplementedException();
        }

        public Room Update(Room room)
        {
            throw new NotImplementedException();
        }

        public Room Validate(Room room)
        {
            throw new NotImplementedException();
        }
    }
}
