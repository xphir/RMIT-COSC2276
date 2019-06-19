using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASR_Web.Models.SlotViewModels
{
    public class SlotCreateViewModel
    {
        public Slot Slot { get; set; }
        public string Result { get; set; }
    }
}
