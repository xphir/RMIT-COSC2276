using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASR_Web.Data;
using ASR_Web.Models;
using ASR_Web.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASR_Web.api
{
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private IRoomRepository repo;
        private ApplicationDbContext _db;

        public RoomController(ApplicationDbContext db, IRoomRepository repository)
        {
            _db = db;
            repo = repository;
        }

        // GET: api/Room/
        [HttpGet]
        public IEnumerable<Room> GetAllRooms()
        {
            return repo.GetAllRooms();
        }

        // POST: api/Room/Create/
        [HttpPost("Create")]
        public IActionResult Create([FromBody] Room room)
        {
            //Check if staff was submitted
            if (room == null)
            {
                return NotFound(new { status = "fail", message = "Room information must be supplied" });
            }

            //Check if the slot already exists
            var roomExists = repo.Find(room.RoomID);
            if (roomExists != null)
            {
                return NotFound(new { status = "fail", message = "Matching room already exists" });
            }

            //Check is a valid slot model
            if (ModelState.IsValid)
            {
                //Create the slot
                var newRoom = repo.Create(room);

                //Check the slot was created
                if (newRoom != null)
                {
                    return Ok(new { status = "success", message = "Room has been created", data = new { room = newRoom } });
                }
            }
            return NotFound(new { status = "fail", message = "Cannot save room", data = ModelState.Values.Select(v => v.Errors) });
        }

        // PUT: api/Room/Edit/
        [HttpPut("Edit/{RoomID}")]
        public IActionResult Book(string roomID, [FromBody] Room updatedRoom)
        {
            return NotFound(new { status = "fail", message = "Not currently Implemented", selectedID = roomID, updatedRoom = new { room = updatedRoom } });
            ////Check if roomID is valid
            //var selectedRoom = repo.Find(roomID);
            //if (selectedRoom == null)
            //{
            //    return NotFound(new { status = "fail", message = "No matching room ID found" });
            //}

            ////Check if slot was submitted
            //if (updatedRoom == null)
            //{
            //    return NotFound(new { status = "fail", message = "Room information must be supplied" });
            //}

            ////Check if updatedRoom exists
            //var roomExists = repo.Find(updatedRoom.RoomID);
            //if (roomExists != null)
            //{
            //    return NotFound(new { status = "fail", message = "Updated room already exists" });
            //}

            //if (ModelState.IsValid)
            //{
            //    //Update the staff
            //    var newRoom = repo.Update(selectedRoom, updatedRoom);

            //    //Check the staff was updated
            //    if (newRoom != null)
            //    {
            //        return Ok(new { status = "success", message = "Room has been updated", data = new { slot = newRoom } });
            //    }
            //}
            //return NotFound(new { status = "fail", message = "Cannot update room", data = ModelState.Values.Select(v => v.Errors) });
        }

        // DELETE: api/Room/Delete/
        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] Room room)
        {
            //Check if the slot already exists
            var roomExists = repo.Find(room.RoomID);
            if (roomExists == null)
            {
                return NotFound(new { status = "fail", message = "No room exists" });
            }

            var deletedSlot = repo.Delete(room);
            if (deletedSlot)
            {
                return Ok(new { status = "success", message = "Room has been deleted" });
            }
            return NotFound(new { status = "fail", message = "Cannot delete room" });
        }       
    }
}
