using ASR_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASR_Web.Repositories
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAllRooms();

        Room Find(string roomID);
        Room Create(Room room);
        Room Update(Room oldRoom, Room newRoom);
        Room Validate(Room room);

        bool Delete(Room room);
    }
}
