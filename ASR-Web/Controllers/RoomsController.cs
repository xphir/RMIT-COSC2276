using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASR_Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASR_Web.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //FROM W9 Tute Files
        // GET: Staff
        public async Task<IActionResult> Index()
        {
            return View(await _context.Room.ToListAsync());
            //View(await _context.Slot.Include(s => s.Room).Include(s => s.Staff).Include(s => s.Student).ToListAsync());
        }
    }
}