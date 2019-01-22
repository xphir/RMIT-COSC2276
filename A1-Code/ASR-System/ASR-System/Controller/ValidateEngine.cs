using System;
using System.Collections.Generic;
using ASR_System.Model;
using System.Linq;

namespace ASR_System.Controller
{
    public static class ValidateEngine
    {
        //CREATE SLOT START

        public static Slot ValidateCreateSlot(string inputRoom, string inputDate, string inputTime, string inputStaffID)
        {
            Slot slotSearchResult;
            Slot returnSlot = null;
            DateTime combinedTime;
            DateTime? combinedTimeNullable;

            //VALIDATE THE ROOM
            if (!(Room.ValidateRoom(inputRoom)))
            {
                Console.WriteLine("Unable to create slot: Invalid Room");
                return null;
            }

            //VALIDATE THE DATE + TIME
            combinedTimeNullable = Slot.ProcessDate(inputDate, inputTime, "Unable to create slot");
            if (!(combinedTimeNullable.HasValue))
            {
                return null;
            }
            else
            {
                //Cast nullable to normal
                combinedTime = (DateTime)combinedTimeNullable;
            }

            //VALIDATE THE STAFF ID
            if (!(ValidateStaffId(inputStaffID)))
            {
                Console.WriteLine("Unable to create slot: Invalid StaffID");
                return null;
            }

            //VALIDATE THE BOOKING COUNT
            if (!(ValidateStaffBookingCount(inputStaffID, combinedTime)))
            {
                Console.WriteLine("Unable to create slot: StaffID has to many bookings");
                return null;
            }

            //VALIDATE THE BOOKING COUNT
            if (!(ValidateRoomBookingCount(inputRoom, combinedTime)))
            {
                Console.WriteLine("Unable to create slot: Room has to many bookings");
                return null;
            }


            //Search for a matching slot
            slotSearchResult = GetSlot(new SlotManager().SlotList, inputRoom, combinedTime);

            //if slotResult is null it means no match was found - or there was to many matches
            if (slotSearchResult != null)
            {
                Console.WriteLine("Unable to create slot: Matching slot already exists");
                return null;
            }

            //TO GET HERE EVERYTHING ELSE PASSED

            //CREATE NEW SLOT
            returnSlot = new Slot(inputRoom, combinedTime, inputStaffID);

            return returnSlot;
        }

        //CREATE SLOT END

        //BOOK SLOT START

        public static Slot ValidateBookSlot(string inputRoom, string inputDate, string inputTime, string inputStudentID)
        {
            Slot slotSearchResult;
            Slot returnSlot = null;
            DateTime combinedTime;
            DateTime? combinedTimeNullable;

            //VALIDATE THE ROOM
            if (!(Room.ValidateRoom(inputRoom)))
            {
                Console.WriteLine("Unable to book slot: Invalid Room");
                return null;
            }

            //VALIDATE THE DATE + TIME
            combinedTimeNullable = Slot.ProcessDate(inputDate, inputTime, "Unable to book slot");
            if (!(combinedTimeNullable.HasValue))
            {
                return null;
            }
            else
            {
                //Cast nullable to normal
                combinedTime = (DateTime)combinedTimeNullable;
            }


            //VALIDATE THE STUDENTID
            if (!(ValidateStudentID(inputStudentID)))
            {
                Console.WriteLine("Unable to book slot: Invalid StudentID");
                return null;
            }

            //VALIDATE THE STUDENT BOOKING COUNT
            if (!(ValidateStudentBookingCount(inputStudentID, combinedTime)))
            {
                Console.WriteLine("Unable to book slot: StudentID has to many bookings");
                return null;
            }

            //Search for a matching slot
            slotSearchResult = GetSlot(new SlotManager().SlotList, inputRoom, combinedTime);

            //if slotResult is null it means no match was found - or there was to many matches
            if (slotSearchResult == null)
            {
                Console.WriteLine("Unable to book slot: slot does not exists");
                return null;
            }

            if (slotSearchResult.BookedInStudentID != null)
            {
                Console.WriteLine("Unable to book slot: slot already has a booking");
                return null;
            }

            slotSearchResult.BookedInStudentID = inputStudentID;
            //TO GET HERE EVERYTHING ELSE PASSED

            //CREATE NEW SLOT
            returnSlot = slotSearchResult;

            return returnSlot;
        }

        //BOOK SLOT END

        //CANCEL SLOT START

        public static Slot ValidateCancelBookSlot(string inputRoom, string inputDate, string inputTime)
        {
            Slot selectedSlot;
            DateTime combinedTime;
            DateTime? combinedTimeNullable;

            //VALIDATE THE ROOM
            if (!(Room.ValidateRoom(inputRoom)))
            {
                Console.WriteLine("Unable to cancel booking: Invalid Room");
                return null;
            }

            //VALIDATE THE DATE + TIME
            combinedTimeNullable = Slot.ProcessDate(inputDate, inputTime, "Unable to cancel booking");
            if (!(combinedTimeNullable.HasValue))
            {
                return null;
            }
            else
            {
                //Cast nullable to normal
                combinedTime = (DateTime)combinedTimeNullable;
            }

            //Search for a matching slot
            selectedSlot = GetSlot(new SlotManager().SlotList, inputRoom, combinedTime);

            //if slotResult is null it means no match was found - or there was to many matches
            if (selectedSlot == null)
            {
                Console.WriteLine("Unable to cancel booking: Slot does not exist");
                return null;
            }

            if (selectedSlot.BookedInStudentID == null)
            {
                Console.WriteLine("Unable to cancel booking: Slot does not have a booking");
                return null;
            }

            //TO GET HERE EVERYTHING ELSE PASSED

            //Set the booking to null
            selectedSlot.BookedInStudentID = null;


            //Return updated Slot
            return selectedSlot;
        }

        //CANCEL SLOT END

        //REMOVE SLOT START

        public static Slot ValidateRemoveSlot(string inputRoom, string inputDate, string inputTime)
        {
            Slot slotSearchResult;
            DateTime combinedTime;
            DateTime? combinedTimeNullable;

            //VALIDATE THE ROOM
            if (!(Room.ValidateRoom(inputRoom)))
            {
                Console.WriteLine("Unable to delete slot: Invalid Room");
                return null;
            }

            //VALIDATE THE DATE + TIME
            combinedTimeNullable = Slot.ProcessDate(inputDate, inputTime, "Unable to delete slot");
            if (!(combinedTimeNullable.HasValue))
            {
                return null;
            }
            else
            {
                //Cast nullable to normal
                combinedTime = (DateTime)combinedTimeNullable;
            }

            //Search for a matching slot
            slotSearchResult = GetSlot(new SlotManager().SlotList, inputRoom, combinedTime);

            //if slotResult is null it means no match was found - or there was to many matches
            if (slotSearchResult == null)
            {
                Console.WriteLine("Unable to delete slot: No unique match found");
                return null;
            }

            //A staff member cannot delete a slot once it has been booked by a student
            if (slotSearchResult.BookedInStudentID != null)
            {
                Console.WriteLine("Unable to delete slot: Selected slot has a booking");
                return null;
            }
            else
            {
                return slotSearchResult;
            }
        }

        //REMOVE SLOT END

        //START GENERALISED VALIDATE METHODS

        public static bool ValidateStaffId(string staffID)
        {
            return new StaffManager().StaffList.Where(x => x.UserID == staffID).Any();
        }

        public static bool ValidateStudentID(string studentID)
        {
            return new StudentManager().StudentList.Where(x => x.UserID == studentID).Any();
        }

        //Check the ammount of bookings a staff has for a given day
        public static bool ValidateStaffBookingCount(String inputStaffID, DateTime dateOnly)
        {
            List<Slot> slotList = new SlotManager().SlotList;

            var resultSlotList = from Slot slot in slotList
                                 where slot.StartTime.Date == dateOnly.Date && slot.StaffID == inputStaffID
                                 select slot;

            if (resultSlotList.Count() >= MiscellaneousUtilities.STAFF_DAILY_BOOKING_LIMIT)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Check the ammount of bookings a student has for a given day
        public static bool ValidateStudentBookingCount(String inputStudentID, DateTime dateOnly)
        {
            List<Slot> slotList = new SlotManager().SlotList;

            var resultSlotList = from Slot slot in slotList
                                 where slot.StartTime.Date == dateOnly.Date && slot.BookedInStudentID == inputStudentID
                                 select slot;

            if (resultSlotList.Count() >= MiscellaneousUtilities.STUDENT_DAILY_BOOKING_LIMIT)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Check the ammount of bookings a room has for a given day
        public static bool ValidateRoomBookingCount(String roomID, DateTime dateOnly)
        {
            List<Slot> slotList = new SlotManager().SlotList;

            var resultSlotList = from Slot slot in slotList
                                 where slot.StartTime.Date == dateOnly.Date && slot.RoomID == roomID
                                 select slot;

            if (resultSlotList.Count() >= MiscellaneousUtilities.ROOM_DAILY_BOOKING_LIMIT)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //END GENERALISED VALIDATE METHODS

        //START OTHER METHODS

        //Get a specifc unique slot
        public static Slot GetSlot(List<Slot> slotList, string roomID, DateTime startTime)
        {
            var resultSlotList = from Slot slot in slotList
                                 where slot.RoomID == roomID && slot.StartTime == startTime
                                 select slot;

            if (resultSlotList.Count() < MiscellaneousUtilities.ROOM_DOUBLEBOOKING_CHECK)
            {
                return null;
            }
            else if (resultSlotList.Count() > MiscellaneousUtilities.ROOM_DOUBLEBOOKING_CHECK)
            {
                return null;
            }
            else
            {
                return resultSlotList.FirstOrDefault();
            }
        }

        //END OTHER METHODS
    }
}
