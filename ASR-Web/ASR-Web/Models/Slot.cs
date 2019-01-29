using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASR_Web.Models;

namespace ASR_Web.Models
{
    public class Slot
    {
        [Required]
        public string RoomID { get; set; }
        public virtual Room Room { get; set; }

        //[DisplayFormat(DataFormatString = "{0:s}")]
        public DateTime StartTime { get; set; }

        [Required]
        public string StaffID { get; set; }
        public virtual Staff Staff { get; set; }

        [DisplayFormat(NullDisplayText = "No Booking", ApplyFormatInEditMode = true)]
        public string StudentID { get; set; }
        public virtual Student Student { get; set; }
    }

}
