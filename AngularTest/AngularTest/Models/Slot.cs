using System;
using System.Collections.Generic;

namespace AngularTest.Models
{
    public partial class Slot
    {
        public string RoomID { get; set; }
        public DateTime StartTime { get; set; }
        public string StaffID { get; set; }
        public string StudentID { get; set; }

        public Room Room { get; set; }
        public Staff Staff { get; set; }
        public Student Student { get; set; }
    }
}
