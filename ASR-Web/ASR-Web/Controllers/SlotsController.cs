using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASR_Web.Data;
using ASR_Web.Models;
using ASR_Web.Models.SlotViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASR_Web.Controllers
{
    public class SlotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SlotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //FROM W9 Tute Files
        // GET: Slots
        public async Task<IActionResult> Index()
        {
            return View(await _context.Slot.ToListAsync());
            //View(await _context.Slot.Include(s => s.Room).Include(s => s.Staff).Include(s => s.Student).ToListAsync());
        }

        // GET: Movies/Details/5

        // GET: Slots/Create
        public async Task<IActionResult> Create()
        {
            var rooms = _context.Room.Select(x => x.RoomID).Distinct().OrderBy(x => x);
            var slot = new Slot();

            return View(new SlotCreateViewModel
            {
                Rooms = new SelectList(await rooms.ToListAsync()),
                Slot = slot
            });


        }

        // GET: Slot/Details/
        public async Task<IActionResult> Details(string roomID, DateTime startTime)
        {

            if ((!string.IsNullOrEmpty(roomID)) && (startTime != DateTime.MinValue))
            {
                return NotFound();
            }

            var selectedSlot = await _context.Slot.SingleOrDefaultAsync(s => s.RoomID == roomID && s.StartTime == startTime);

            if (selectedSlot == null)
            {
                return NotFound();
            }

            return View(selectedSlot);
        }
    }
}