using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASR_Web.Data;
using ASR_Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASR_Web.api
{
    [Route("api/[controller]")]
    public class SlotsController : Controller
    {
        private ISlotRepository repo;
        private ApplicationDbContext _db;

        public SlotsController(ApplicationDbContext db, ISlotRepository repository)
        {
            _db = db;
            repo = repository;
        }

        //// GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var allSlots = repo.All();

            return Ok(
                new
                {
                    status = "OK",
                    message = $"Found {allSlots.Count()} result(s)",
                    data = new { slots = allSlots }
                }
            );
        }

        //// GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet("staffbookings/{staffID}")]
        public IActionResult GetStaffBookings(string staffID)
        {
            var staffSlots = repo.SearchByStaff(staffID);

            return Ok(
                new
                {
                    status = "ok",
                    message = $"found {staffSlots.Count()} result(s)",
                    data = new { slots = staffSlots }
                }
            );
        }

        [HttpGet("studentbookings/{studentID}")]
        public IActionResult GetStudentBookings(string studentID)
        {
            var studentSlots = repo.SearchByStudent(studentID);

            return Ok(
                new
                {
                    status = "ok",
                    message = $"found {studentSlots.Count()} result(s)",
                    data = new { slots = studentSlots }
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


        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NotFound(new { status = "fail", message = "Cannot delete selected slot" });
        }
    }
}
