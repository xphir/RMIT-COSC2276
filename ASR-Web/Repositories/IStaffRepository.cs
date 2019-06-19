using ASR_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASR_Web.Repositories
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> GetAllStaff();
        
        Staff Find(string staffID);
        Staff Validate(Staff staff);

        Staff Create(Staff staff);
        Staff Update(Staff staff);
        bool Delete(Staff staff);
    }
}
