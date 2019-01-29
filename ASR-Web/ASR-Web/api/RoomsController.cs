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
    public class RoomsController : Controller
    {
        private IRoomRepository repo;
        private ApplicationDbContext _db;

        public RoomsController(ApplicationDbContext db, IRoomRepository repository)
        {
            _db = db;
            repo = repository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var allRooms = repo.All();

            return Ok(
                new
                {
                    status = "OK",
                    message = $"Found {allRooms.Count()} result(s)",
                    data = new { rooms = allRooms }
                }
            );
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPost("create")]
        public IActionResult CreateRoom([FromBody]Room room)
        {
            if (room == null)
            {
                return NotFound(new { status = "fail", message = "Room information must be supplied" });
            }

            if (ModelState.IsValid)
            {
                var createdRoom = repo.Create(room);
                if (createdRoom != null)
                {
                    return Ok(new { status = "success", message = "Room has been created", data = new { room = createdRoom } });
                }
            }
            return NotFound(new { status = "fail", message = "Cannot save room", data = ModelState.Values.Select(v => v.Errors) });
        }

        // DELETE api/<controller>/5
        [HttpDelete("delete")]
        public IActionResult DeleteRoom([FromBody]Room room)
        {
            if(repo.Find(room.RoomID) == null)
            {
                return NotFound(new { status = "fail", message = "Room does not exist" });
            }

            var deletedRoom = repo.Delete(room);
            if (deletedRoom)
            {
                return Ok(new { status = "success", message = "Room has been deleted" });
            }
            return NotFound(new { status = "fail", message = "Cannot delete room" });
        }
    }
}
