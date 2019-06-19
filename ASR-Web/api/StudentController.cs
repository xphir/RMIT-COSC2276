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
    public class StudentController : ControllerBase
    {
        private IStudentRepository repo;
        private ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db, IStudentRepository repository)
        {
            _db = db;
            repo = repository;
        }


        // GET: api/Student/Get/
        [HttpGet]
        public IEnumerable<Student> GetAllStudent()
        {
            return repo.GetAllStudents();
        }

        // GET: api/Student/<StudentID>
        [HttpGet("{StudentID}")]
        public Student GetStudentID(string StudentID)
        {
            return repo.Find(StudentID);
        }

        // POST: api/Student/Create/
        [HttpPost("Create")]
        public IActionResult Create([FromBody] Student student)
        {
            //Check if student was submitted
            if (student == null)
            {
                return NotFound(new { status = "fail", message = "Student information must be supplied" });
            }

            //Check if the student already exists
            var studentExistsStudent = repo.Find(student.StudentID);
            if (studentExistsStudent != null)
            {
                return NotFound(new { status = "fail", message = "Updated student information conflict" });
            }

            //Check is a valid student model
            if (ModelState.IsValid)
            {
                //Create the student
                var newStudent = repo.Create(student);

                //Check the student was created
                if (newStudent != null)
                {
                    return Ok(new { status = "success", message = "Student has been created", data = new { student = newStudent } });
                }
            }
            return NotFound(new { status = "fail", message = "Cannot save student", data = ModelState.Values.Select(v => v.Errors) });
        }

        // PUT: api/Student/Update/
        [HttpPut("Update/{studentID}")]
        public IActionResult Update(string studentID, [FromBody] Student updatedStudent)
        {
            //Check if studentID is valid
            var selectedStudent = repo.Find(studentID);
            if (selectedStudent == null)
            {
                return NotFound(new { status = "fail", message = "No matching StudentID found" });
            }

            //Check if updatedStudent was submitted
            if (updatedStudent == null)
            {
                return NotFound(new { status = "fail", message = "Updated student information must be supplied" });
            }

            //Check if updatedStudent is valid ID
            if (studentID != updatedStudent.StudentID)
            {
                return NotFound(new { status = "fail", message = "Can not change student ID" });
            }

            //Check is a valid student model
            if (ModelState.IsValid)
            {
                //Update the student
                var newStudent = repo.Update(updatedStudent);

                //Check the student was updated
                if (newStudent != null)
                {
                    return Ok(new { status = "success", message = "Student has been updated", data = new { student = newStudent } });
                }
            }
            return NotFound(new { status = "fail", message = "Cannot update student", data = ModelState.Values.Select(v => v.Errors) });

        }

        // DELETE: api/Delete/
        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] Student student)
        {
            var deletedStudent = repo.Delete(student);

            if (deletedStudent)
            {
                return Ok(new { status = "success", message = "Student has been deleted" });
            }
            return NotFound(new { status = "fail", message = "Cannot delete student" });

        }
    }
}