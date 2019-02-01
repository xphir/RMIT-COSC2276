using ASR_Web.Data;
using ASR_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASR_Web.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public ApplicationDbContext _db;

        public StudentRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        private IQueryable<Student> BaseStudentSelector()
        {
            return _db.Student;
        }

        public Student Create(Student student)
        {
            var dbStudent = Find(student.StudentID);

            if (dbStudent == null)
            {
                var validStudent = Validate(student);
                _db.Student.Add(validStudent);
                _db.SaveChanges();
                return validStudent;
            }
            return null;
        }

        public bool Delete(Student student)
        {
            var dbStudent = Find(student.StudentID);

            if (dbStudent != null)
            {
                _db.Student.Remove(dbStudent);
                _db.SaveChanges();
            }

            return dbStudent == null;
        }

        public Student Find(string studentID)
        {
            return BaseStudentSelector().FirstOrDefault(s => s.StudentID == studentID.ToLower());
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return BaseStudentSelector().ToList();
        }

        public Student Update(Student student)
        {
            var dbStudent = Find(Validate(student).StudentID);

            if (dbStudent != null)
            {
                var validStudent = Validate(student);
                dbStudent.Name = validStudent.Name;
                dbStudent.Email = validStudent.Email;
                _db.SaveChanges();
            }
            return dbStudent;
        }

        public Student Validate(Student student)
        {
            return new Student
            {
                StudentID = student.StudentID.ToLower(),
                Name = student.Name,
                Email = student.Email.ToLower()
            };
        }
    }
}
