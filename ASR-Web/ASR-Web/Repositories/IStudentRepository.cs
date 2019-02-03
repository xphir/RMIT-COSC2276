using ASR_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASR_Web.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents();

        Student Find(string studentID);
        Student Validate(Student student);

        Student Create(Student student);
        Student Update(Student student);
        bool Delete(Student student);
    }
}
