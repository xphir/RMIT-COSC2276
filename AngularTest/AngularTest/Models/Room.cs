using System;
using System.Collections.Generic;

namespace AngularTest.Models
{
    public partial class Room
    {
        public Room()
        {
            Slot = new HashSet<Slot>();
        }

        public string RoomID { get; set; }

        public ICollection<Slot> Slot { get; set; }
    }
}
