using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASR_Web.Data;
using ASR_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASR_Web.Controllers
{
    public class SlotsController : Controller
    {
        private readonly AsrContext _context;

        public SlotsController(AsrContext context)
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
        public async Task<IActionResult> Details(int? hashCode)
        {
            if (hashCode == null)
            {
                return NotFound();
            }

            var selectedSlot = await _context.Slot.SingleOrDefaultAsync(m => m.GetHashCode() == hashCode);
            if (selectedSlot == null)
            {
                return NotFound();
            }

            return View(selectedSlot);
        }
    }
}