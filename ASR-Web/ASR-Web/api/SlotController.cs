using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASR_Web.Data;
using ASR_Web.Models;
using ASR_Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASR_Web.api
{
    [Route("api/[controller]")]
    public class SlotController : Controller
    {
        private ISlotRepository repo;
        private ApplicationDbContext _db;

        public SlotController(ApplicationDbContext db, ISlotRepository repository)
        {
            _db = db;
            repo = repository;
        }

        // GET: api/Slot/
        [HttpGet]
        public IEnumerable<Slot> GetAllSlots()
        {
            return repo.GetAllSlots();
        }

        // GET: api/Slot/StaffBooking/{staffID}
        [HttpGet("StaffBooking/{staffID}")]
        public IEnumerable<Slot> GetStaffBookings(string staffID)
        {
            return repo.SearchByStaff(staffID);
        }

        // GET: api/Slot/StudentBooking/{studentID}
        [HttpGet("StudentBooking/{studentID}")]
        public IEnumerable<Slot> GetStudentBookings(string studentID)
        {
            return repo.SearchByStudent(studentID);
        }

        // POST: api/Slot/Details/
        [HttpPost("Details")]
        public IActionResult Details([FromBody] Slot slot)
        {
            var slotResult = repo.Find(slot.RoomID, slot.StartTime);
            if (slotResult == null)
            {
                return NotFound(new { status = "fail", message = "No matching slot found" });
            }
            else
            {
                return Ok(new { status = "success", message = "Slot has been found", data = new { slot = slotResult } });
            }

        }

        // POST: api/Slot/Create/
        [HttpPost("Create")]
        public IActionResult Create([FromBody] Slot slot)
        {
            //Check if staff was submitted
            if (slot == null)
            {
                return NotFound(new { status = "fail", message = "Slot information must be supplied" });
            }

            //Check if the slot already exists
            var slotExists = repo.Find(slot.RoomID, slot.StartTime);
            if (slotExists != null)
            {
                return NotFound(new { status = "fail", message = "Matching slot already exists" });
            }

            //Check is a valid slot model
            if (ModelState.IsValid)
            {
                //Create the slot
                var newSlot = repo.Create(slot);

                //Check the slot was created
                if (newSlot != null)
                {
                    return Ok(new { status = "success", message = "Slot has been created", data = new { slot = newSlot } });
                }
            }
            return NotFound(new { status = "fail", message = "Cannot save slot", data = ModelState.Values.Select(v => v.Errors) });
        }

        // PUT: api/Slot/Book/
        [HttpPut("Book/{studentID}")]
        public IActionResult Book(string studentID, [FromBody] Slot slot)
        {
            //Check if staffID is valid
            var selectedStudent = _db.Student.FirstOrDefault(s => s.StudentID == studentID.ToLower());
            if (selectedStudent == null)
            {
                return NotFound(new { status = "fail", message = "No matching student ID found" });
            }

            //Check if slot was submitted
            if (slot == null)
            {
                return NotFound(new { status = "fail", message = "Slot information must be supplied" });
            }

            //Check if slot exists
            var selectedSlot = repo.Find(slot.RoomID, slot.StartTime);
            if (selectedSlot == null)
            {
                return NotFound(new { status = "fail", message = "Slot does not exist" });
            }


            //Check if slot is already booked
            if (selectedSlot.StudentID != null)
            {
                return NotFound(new { status = "fail", message = "Slot already has a booking" });
            }


            if (ModelState.IsValid)
            {
                //Update the staff
                var bookedSlot = repo.Book(selectedSlot, studentID);

                //Check the staff was updated
                if (bookedSlot != null)
                {
                    return Ok(new { status = "success", message = "Slot has been booked", data = new { slot = bookedSlot } });
                }
            }
            return NotFound(new { status = "fail", message = "Cannot book slot", data = ModelState.Values.Select(v => v.Errors) });
        }

        // PUT: api/Slot/Unbook/
        [HttpPut("Unbook")]
        public IActionResult Unook([FromBody] Slot slot)
        {
            //Check if slot was submitted
            if (slot == null)
            {
                return NotFound(new { status = "fail", message = "Slot information must be supplied" });
            }

            //Check if slot exists
            var selectedSlot = repo.Find(slot.RoomID, slot.StartTime);
            if (selectedSlot == null)
            {
                return NotFound(new { status = "fail", message = "Slot does not exist" });
            }

            //Check if slot is already booked
            if (selectedSlot.StudentID == null)
            {
                return NotFound(new { status = "fail", message = "Slot already has no booking" });
            }


            if (ModelState.IsValid)
            {
                //Update the staff
                var bookedSlot = repo.Unbook(selectedSlot);

                //Check the staff was updated
                if (bookedSlot != null)
                {
                    return Ok(new { status = "success", message = "Slot has been unbooked", data = new { slot = bookedSlot } });
                }
            }
            return NotFound(new { status = "fail", message = "Cannot unbook slot", data = ModelState.Values.Select(v => v.Errors) });
        }

        // DELETE: api/Slot/Delete/
        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] Slot slot)
        {
            var deletedSlot = repo.Delete(slot);

            if (deletedSlot)
            {
                return Ok(new { status = "success", message = "Staff has been deleted" });
            }
            return NotFound(new { status = "fail", message = "Cannot delete slot" });
        }
    }
}
