using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASR_Web.Data;
using ASR_Web.Models;

namespace ASR_Web.Repositories
{
    class StaffRepository : IStaffRepository
    {
        public ApplicationDbContext _db;

        public StaffRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        private IQueryable<Staff> BaseStaffSelector()
        {
            return _db.Staff;
        }

        public Staff Create(Staff staff)
        {
            var dbStaff = Find(staff.StaffID);
            
            if (dbStaff == null)
            {
                var validStaff = Validate(staff);
                _db.Staff.Add(validStaff);
                _db.SaveChanges();
                return validStaff;
            }
            return null;
        }

        public bool Delete(Staff staff)
        {
            var dbStaff = Find(staff.StaffID);

            if (dbStaff != null)
            {
                _db.Staff.Remove(dbStaff);
                _db.SaveChanges();
            }

            return dbStaff == null;
        }

        public Staff Find(string staffID)
        {
            return BaseStaffSelector().FirstOrDefault(s => s.StaffID == staffID.ToLower());
        }

        public IEnumerable<Staff> GetAllStaff()
        {
            return BaseStaffSelector().ToList();
        }

        public Staff Update(Staff staff)
        {
            var dbStaff = Find(Validate(staff).StaffID);

            if (dbStaff != null)
            {
                var validStaff = Validate(staff);
                dbStaff.Name = validStaff.Name;
                dbStaff.Email = validStaff.Email;
                _db.SaveChanges();
            }
            return dbStaff;
        }

        public Staff Validate(Staff staff)
        {
            return new Staff
            {
                StaffID = staff.StaffID.ToLower(),
                Name = staff.Name,
                Email = staff.Email.ToLower()
            };
        }


    }
}
