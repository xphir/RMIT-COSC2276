using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASR_Web.Data;
using ASR_Web.Models;
using ASR_Web.Models.SlotViewModels;
using ASR_Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASR_Web.Controllers
{
    public class SlotsController : Controller
    {
        private ISlotRepository _repo;
        private ApplicationDbContext _db;

        public SlotsController(ApplicationDbContext db, ISlotRepository repository)
        {
            _db = db;
            _repo = repository;
        }

        //public IActionResult Index()
        //{


        //    return View(_repo.All());
        //}
        
        // GET: Movies
        public IActionResult Index(string RoomSelect, string StaffSelect, string StudentSelect)
        {
            return View(new SlotIndexViewModel
            {
                Slots = _repo.GetFilteredSlots(RoomSelect, StaffSelect, StudentSelect),
                Rooms = new SelectList(_repo.GetDistinctRooms()),
                Staff = new SelectList(_repo.GetDistinctStaff()),
                Students = new SelectList(_repo.GetDistinctStudents()),
            });
        }

        public IActionResult Details(string RoomID, string StartTime)
        {
            //Incoming StartTimes should be in the following format YYYY-MM-DD-THH:mm:ss 2019-01-30T00:00:00

            //Check the RoomID & StartTime fields are there
            if ((string.IsNullOrEmpty(RoomID)) && (string.IsNullOrEmpty(StartTime)))
            {
                return NotFound();
            }

            //Try get the DateTime from the input string StartTime
            if (!(DateTime.TryParse(StartTime, out DateTime startTimeValue)))
            {
                return NotFound();
            }

            //Find the selected slot from the repo/db
            var selectedSlot = _repo.Find(RoomID, startTimeValue);

            //if null then nothing was found
            if (selectedSlot == null)
            {
                return NotFound();
            }

            return View(selectedSlot);
        }

        // GET: Slots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Slots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slot slot)
        {
            if (ModelState.IsValid)
            {
                _repo.Create(slot);
            }

            return View();
        }
    }
}

   