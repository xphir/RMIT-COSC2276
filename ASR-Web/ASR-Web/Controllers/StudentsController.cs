using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASR_Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASR_Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AsrContext _context;

        public StudentsController(AsrContext context)
        {
            _context = context;
        }

        //FROM W9 Tute Files
        // GET: Staff
        public async Task<IActionResult> Index()
        {
            return View(await _context.Student.ToListAsync());
            //View(await _context.Slot.Include(s => s.Room).Include(s => s.Staff).Include(s => s.Student).ToListAsync());
        }
    }
}