using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AngularTest.Models
{
    public class ASRDataAccessLayer
    {
        private readonly s3530160Context db = new s3530160Context();

        public IEnumerable<Student> GetAllStudents()
        {
            return db.Student.ToList();
        }
        
        // To Add new Student record.
        public int AddStudent(Student student)
        {
            db.Student.Add(student);
            db.SaveChanges();
            return 1;
        }

        // To Update the records of a particular employee.
        public int UpdateStudent(Student student)
        {
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return 1;
        }

        // Get the details of a particular employee.
        public Student GetStudentData(int id)
        {
            var student = db.Student.Find(id);
            return student;
        }

        // To Delete the record of a particular employee.
        public int DeleteStudent(int id)
        {
            var emp = db.Student.Find(id);
            db.Student.Remove(emp);
            db.SaveChanges();
            return 1;
        }
        //STAFF


        public IEnumerable<Staff> GetAllStaff()
        {
            return db.Staff.ToList();
        }

        // To Add new Staff record.
        public int AddStaff(Staff staff)
        {
            db.Staff.Add(staff);
            db.SaveChanges();
            return 1;
        }

        // To Update the records of a particular employee.
        public int UpdateStaff(Staff staff)
        {
            db.Entry(staff).State = EntityState.Modified;
            db.SaveChanges();
            return 1;
        }

        // Get the details of a particular employee.
        public Staff GetStaffData(int id)
        {
            var staff = db.Staff.Find(id);
            return staff;
        }

        // To Delete the record of a particular employee.
        public int DeleteStaff(int id)
        {
            var emp = db.Staff.Find(id);
            db.Staff.Remove(emp);
            db.SaveChanges();
            return 1;
        }

        // To Get the list of Slots.
        public List<Slot> GetSlotList()
        {
            return db.Slot.ToList();
        }



    }
}
