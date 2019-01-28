using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IEnumerable<Slot> All()
        {
            return _db.Slot.ToList();
               //.Include(s => s.RoomID)
               //.Include(s => s.StartTime)
               //.Include(s => s.StaffID)
               //.Include(s => s.StudentID)
               //.ToList();
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
            return _db.Slot.FirstOrDefault(s => s.RoomID == roomID.ToUpper() && s.StartTime == startTime);
        }

        public IEnumerable<Slot> SearchByDate(DateTime start, DateTime end)
        {
            return _db.Slot
                .Where(s => s.StartTime >= start && s.StartTime <= end)
                .ToList();
                //.Include(s => s.RoomID)
                //.Include(s => s.StartTime)
                //.Include(s => s.StaffID)
                //.Include(s => s.StudentID)
                //.ToList();
        }

        public IEnumerable<Slot> SearchByRoom(string roomID)
        {
            return _db.Slot
                .Where(s => s.RoomID == roomID.ToUpper() && s.StartTime > DateTime.Now)
                .ToList();
                //.Include(s => s.RoomID)
                //.Include(s => s.StartTime)
                //.Include(s => s.StaffID)
                //.Include(s => s.StudentID)
                //.ToList();
        }

        public IEnumerable<Slot> SearchByStaff(string staffID)
        {
            return _db.Slot
                .Where(s => s.StaffID == staffID.ToLower() && s.StartTime > DateTime.Now)
                .ToList();
                //.Include(s => s.RoomID)
                //.Include(s => s.StartTime)
                //.Include(s => s.StaffID)
                //.Include(s => s.StudentID)
                //.ToList();
        }

        public IEnumerable<Slot> SearchByStudent(string studentID)
        {
            return _db.Slot
                .Where(s => s.StudentID == studentID.ToLower() && s.StartTime > DateTime.Now)
                .ToList();
                //.Include(s => s.RoomID)
                //.Include(s => s.StartTime)
                //.Include(s => s.StaffID)
                //.Include(s => s.StudentID)
                //.ToList();
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
            return _db.Slot
                .Where(s => s.StudentID != null && s.StartTime > DateTime.Now)
                .ToList();
        }

        public IEnumerable<Slot> GetAvailibleSlotsGivenDay(DateTime day)
        {
            return _db.Slot
                 .Where(s => s.StudentID != null && s.StartTime.Date > day.Date)
                 .ToList();
        }

        public IEnumerable<Slot> GetRoomSlotsGivenDay(string roomID, DateTime day)
        {
            return _db.Slot
                 .Where(s => s.RoomID == roomID && s.StartTime.Date > day.Date)
                 .ToList();
        }
    }
}
