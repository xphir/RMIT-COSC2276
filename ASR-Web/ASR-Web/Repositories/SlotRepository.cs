using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asr.Data;
using ASR_Web.Data;
using ASR_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ASR_Web.Repositories
{
    public class SlotRepository : ISlotRepository
    {
        public ApplicationDbContext _db;

        public SlotRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        //CHANGE THIS VALUE IF YOU WANT RESULTS FROM THE PAST
        private IQueryable<Slot> BaseSlotSelector()
        {
            if (Constants.PastResults)
            {
                return _db.Slot;
            }
            else
            {
                return _db.Slot.Where(s => s.StartTime > DateTime.Now);
            }
            
        }
      
        public IEnumerable<Slot> GetAllSlots()
        {
            return BaseSlotSelector().ToList();
        }

        public IEnumerable<Slot> GetFilteredSlots(string RoomSelect, string StaffSelect, string StudentSelect)
        {
            var slots = BaseSlotSelector();

            if (!string.IsNullOrEmpty(RoomSelect))
            {
                slots = slots.Where(s => s.RoomID == RoomSelect);
            }

            if (!string.IsNullOrEmpty(StaffSelect))
            {
                slots = slots.Where(s => s.StaffID == StaffSelect);
            }

            if (!string.IsNullOrEmpty(StudentSelect))
            {
                if (StudentSelect == "FreeSlot")
                {
                    slots = slots.Where(s => s.StudentID == null);
                }
                else
                {
                    slots = slots.Where(s => s.StudentID == StudentSelect);
                }
                    
            }

            return slots.ToList();

        }

        public IEnumerable<String> GetDistinctRooms()
        {
            return BaseSlotSelector()
                .Select(x => x.RoomID)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        public IEnumerable<String> GetDistinctStaff()
        {
            return BaseSlotSelector()
                .Select(x => x.StaffID)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        public IEnumerable<String> GetDistinctStudents()
        {
            return BaseSlotSelector()
                .Select(x => x.StudentID)
                .Where(x => x != null)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        public Slot Create(Slot slot)
        {
            var dbSlot = Find(slot.RoomID, slot.StartTime);

            if (dbSlot == null)
            {
                if(slot.StartTime >= DateTime.Now)
                {
                    _db.Slot.Add(slot);
                    _db.SaveChanges();
                    return slot;
                }
                return null;
            }
            return null;
        }

        public Slot Book(Slot slot, string studentID)
        {
            var dbSlot = Find(slot.RoomID, slot.StartTime);

            if (dbSlot != null && dbSlot.StudentID == null)
            {
                dbSlot.StudentID = studentID;
                _db.SaveChanges();
                return dbSlot;
            }
            return null;
        }

        public Slot Unbook(Slot slot)
        {
            var dbSlot = Find(slot.RoomID, slot.StartTime);

            if (dbSlot != null && dbSlot.StudentID != null)
            {
                dbSlot.StudentID = null;
                _db.SaveChanges();
                return dbSlot;
            }
            return null;
        }

        public Slot Update(Slot slot)
        {
            var dbSlot = Find(slot.RoomID, slot.StartTime);
            if (dbSlot != null)
            {
                dbSlot.RoomID = slot.RoomID;
                dbSlot.StartTime = slot.StartTime;
                dbSlot.StaffID = slot.StaffID;
                dbSlot.StudentID = slot.StudentID;
                _db.SaveChanges();
            }
            return dbSlot;
        }

        public bool Delete(Slot slot)
        {
            var dbSlot = Find(slot.RoomID, slot.StartTime);
            if (dbSlot != null)
            {
                _db.Slot.Remove(dbSlot);
                _db.SaveChanges();
            }
            return dbSlot == null;
        }

        public Slot Find(string roomID, DateTime startTime)
        {
            return BaseSlotSelector()
                .FirstOrDefault(s => s.RoomID == roomID.ToUpper() && s.StartTime == startTime);
        }

        public IEnumerable<Slot> SearchByDate(DateTime start, DateTime end)
        {
            return BaseSlotSelector()
                .Where(s => s.StartTime >= start && s.StartTime <= end)
                .ToList();
        }

        public IEnumerable<Slot> SearchByRoom(string roomID)
        {
            return BaseSlotSelector()
                .Where(s => s.RoomID == roomID.ToUpper() && s.StartTime > DateTime.Now)
                .ToList();
        }

        public IEnumerable<Slot> SearchByStaff(string staffID)
        {
            return BaseSlotSelector()
                .Where(s => s.StaffID == staffID.ToLower() && s.StartTime > DateTime.Now)
                .ToList();
        }

        public IEnumerable<Slot> SearchByStaffDate(string staffID, DateTime inputDate)
        {
            return BaseSlotSelector()
                .Where(s => s.StaffID == staffID.ToLower() && s.StartTime.Date == inputDate.Date)
                .ToList();
        }

        public IEnumerable<Slot> SearchByStudentDate(string studentID, DateTime inputDate)
        {
            return BaseSlotSelector()
                .Where(s => s.StudentID == studentID.ToLower() && s.StartTime.Date == inputDate.Date)
                .ToList();
        }

        public IEnumerable<Slot> SearchByStudent(string studentID)
        {
            return BaseSlotSelector()
                .Where(s => s.StudentID == studentID.ToLower() && s.StartTime > DateTime.Now)
                .ToList();
        }

        public Slot Validate(Slot slot)
        {
            var returnSlot = new Slot();

            returnSlot.RoomID = slot.RoomID.ToUpper();
            returnSlot.StartTime = slot.StartTime;
            returnSlot.StaffID = slot.StaffID.ToLower();
            returnSlot.StudentID = slot.StudentID.ToLower();

            return returnSlot;
        }

        public IEnumerable<Slot> GetAvailibleSlots()
        {
            return BaseSlotSelector()
                .Where(s => s.StudentID != null && s.StartTime > DateTime.Now)
                .ToList();
        }

        public IEnumerable<Slot> GetAvailibleSlotsGivenDay(DateTime day)
        {
            return BaseSlotSelector()
                 .Where(s => s.StudentID != null && s.StartTime.Date > day.Date)
                 .ToList();
        }

        public IEnumerable<Slot> GetRoomSlotsGivenDay(string roomID, DateTime day)
        {
            return BaseSlotSelector()
                 .Where(s => s.RoomID == roomID && s.StartTime.Date > day.Date)
                 .ToList();
        }
    }
}
