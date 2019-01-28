using ASR_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASR_Web.Repositories
{
    interface IRoomRepository
    {
        IEnumerable<Room> All();

        Room Find(string roomID);
        Room Create(Room room);
        Room Update(Room room);
        Room Validate(Room room);

        bool Delete(Room room);
    }
}
