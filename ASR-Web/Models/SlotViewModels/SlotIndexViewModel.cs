using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASR_Web.Models.SlotViewModels
{
    public class SlotIndexViewModel
    {
        public IEnumerable<Slot> Slots { get; set; }
        public SelectList Rooms { get; set; }
        public SelectList Staff { get; set; }
        public SelectList Students { get; set; }

        public string RoomSelect { get; set; }
        public string StaffSelect { get; set; }
        public string StudentSelect { get; set; }
    }
}
