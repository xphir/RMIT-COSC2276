using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASR_Web.Data;
using ASR_Web.Models;
using ASR_Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASR_Web.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IStaffRepository repo;
        private ApplicationDbContext _db;

        public StaffController(ApplicationDbContext db, IStaffRepository repository)
        {
            _db = db;
            repo = repository;
        }


        // GET: api/Staff/Get/
        [HttpGet]
        public IEnumerable<Staff> GetAllStaff()
        {
            return repo.GetAllStaff();
        }

        // GET: api/Staff/<StaffID>
        [HttpGet("{StaffID}")]
        public Staff GetStaffID(string StaffID)
        {
            return repo.Find(StaffID);
        }

        // POST: api/Staff/Create/
        [HttpPost("Create")]
        public IActionResult Create([FromBody] Staff staff)
        {
            //Check if staff was submitted
            if (staff == null)
            {
                return NotFound(new { status = "fail", message = "Staff information must be supplied" });
            }

            //Check if the staff already exists
            var staffExistsStaff = repo.Find(staff.StaffID);
            if (staffExistsStaff != null)
            {
                return NotFound(new { status = "fail", message = "Updated staff information conflict" });
            }

            //Check is a valid staff model
            if (ModelState.IsValid)
            {
                //Create the staff
                var newStaff = repo.Create(staff);

                //Check the staff was created
                if (newStaff != null)
                {
                    return Ok(new { status = "success", message = "Staff has been created", data = new { staff = newStaff } });
                }
            }
            return NotFound(new { status = "fail", message = "Cannot save staff", data = ModelState.Values.Select(v => v.Errors) });
        }

        // PUT: api/Staff/Update/
        [HttpPut("Update/{staffID}")]
        public IActionResult Update(string staffID, [FromBody] Staff updatedStaff)
        {
            //Check if staffID is valid
            var selectedStaff = repo.Find(staffID);
            if (selectedStaff == null)
            {
                return NotFound(new { status = "fail", message = "No matching StaffID found" });
            }

            //Check if updatedStaff was submitted
            if (updatedStaff == null)
            {
                return NotFound(new { status = "fail", message = "Updated staff information must be supplied" });
            }

            //Check if updatedStaff is valid ID
            if (staffID != updatedStaff.StaffID)
            {
                return NotFound(new { status = "fail", message = "Can not change staff ID" });
            }

            //Check is a valid staff model
            if (ModelState.IsValid)
            {
                //Update the staff
                var newStaff = repo.Update(updatedStaff);

                //Check the staff was updated
                if (newStaff != null)
                {
                    return Ok(new { status = "success", message = "Staff has been updated", data = new { staff = newStaff } });
                }
            } 
            return NotFound(new { status = "fail", message = "Cannot update staff", data = ModelState.Values.Select(v => v.Errors) });
            
        }

        // DELETE: api/Delete/
        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] Staff staff)
        {
            var deletedStaff = repo.Delete(staff);

            if (deletedStaff)
            {
                return Ok(new { status = "success", message = "Staff has been deleted" });
            }
            return NotFound(new { status = "fail", message = "Cannot delete staff" });

        }
    }
}
