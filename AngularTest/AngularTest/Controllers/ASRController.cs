using System.Collections.Generic;
using AngularTest.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace AngularTest.Controllers
{
    [Route("api/[controller]")]
    public class ASRController : Controller
    {
        private readonly ASRDataAccessLayer ASRDataAccessLayer = new ASRDataAccessLayer();

        [HttpGet]
        [Route("GetStudents")]
        public IEnumerable<Student> Get()
        {
            return ASRDataAccessLayer.GetAllStudents();
        }

        [HttpPost]
        [Route("CreateStudent")]
        public int Create([FromBody] Student student)
        {
            return ASRDataAccessLayer.AddStudent(student);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public Student Details(int id)
        {
            return ASRDataAccessLayer.GetStudentData(id);
        }

        [HttpPut]
        [Route("Edit")]
        public int Edit([FromBody] Student student)
        {
            return ASRDataAccessLayer.UpdateStudent(student);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public int Delete(int id)
        {
            return ASRDataAccessLayer.DeleteStudent(id);
        }

        [HttpGet]
        [Route("GetSlotList")]
        public IEnumerable<Slot> Details()
        {
            return ASRDataAccessLayer.GetSlotList();
        }
    }
}
