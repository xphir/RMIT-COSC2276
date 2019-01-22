using ASR_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASR_System.Controller
{
    public static class MainEngine
    {
        //CALLED FROM STAFF MENU
        public static void CheckRoomAvailability()
        {
            DateTime dateOnly;
            DateTime? dateOnlyNullable;
            List<Slot> slotList = new SlotManager().SlotList;

            Console.WriteLine("---Room availability---");
            Console.Write("Enter date for room availability (dd-mm-yyyy): ");

            string inputDate = Console.ReadLine();

            //VALIDATE THE DATE
            dateOnlyNullable = Slot.ValidateDate(inputDate);
            if (!(dateOnlyNullable.HasValue))
            {
                Console.WriteLine("Invalid Date");
                return;
            }
            else
            {
                //Cast nullable to normal
                dateOnly = (DateTime)dateOnlyNullable;
            }

            //This LINQ will return a list of roomIDs availible to be booked, with duplicates removed

            var resultRoomList = from Slot slot in slotList
                                 where slot.StartTime.Date == dateOnly.Date && slot.BookedInStudentID == null
                                 group slot.RoomID by slot.RoomID into roomGroup
                                 orderby roomGroup.Key
                                 select roomGroup.Key;

            PrintRoomAvailability(resultRoomList.ToList(), "Rooms available on " + dateOnly.ToString("dd-MM-yyy"), "<no rooms availible>");
        }

        //CALLED FROM STUDENT MENU
        public static void CheckStaffAvailability()
        {
            DateTime dateOnly;
            DateTime? dateOnlyNullable;
            List<Slot> slotList = new SlotManager().SlotList;

            Console.WriteLine("---Staff availability---");

            Console.Write("Enter date for room availability (dd-mm-yyyy): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter staff ID: ");
            string inputStaffID = Console.ReadLine();

            //VALIDATE THE DATE
            dateOnlyNullable = Slot.ValidateDate(inputDate);
            if (!(dateOnlyNullable.HasValue))
            {
                Console.WriteLine("Invalid Date");
                return;
            }
            else
            {
                //Cast nullable to normal
                dateOnly = (DateTime)dateOnlyNullable;
            }

            //VALIDATE THE STAFF ID
            if (!(ValidateEngine.ValidateStaffId(inputStaffID)))
            {
                Console.WriteLine("Invalid StaffID");
                return;
            }

            //This LINQ will return a list of roomIDs availible to be booked, with duplicates removed

            var resultRoomList = from Slot slot in slotList
                                 where slot.StartTime.Date == dateOnly.Date && slot.BookedInStudentID == null && slot.StaffID == inputStaffID
                                 select slot;
            PrintStaffAvailability(resultRoomList.ToList(), "Staff " + inputStaffID + " availability on " + dateOnly.ToString("dd-MM-yyy"), "<no slots availible>");
        }


        //LOGIC METHODS START

        //NOT CURRENTLY IMPLEMENTED
        //Checks to see if UserID exists
        public static bool CheckIdExists(List<User> inputUserList, string userID)
        {
            foreach (User selectedUser in inputUserList)
            {
                if (selectedUser.UserID == userID)
                {
                    return true;
                }
            }
            return false;
        }

        //LOGIC METHODS END

        //PRINT METHODS START

        public static void PrintStaffAvailability(List<Slot> slotList, String title, String error)
        {
            Console.WriteLine(title);
            Console.WriteLine(string.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}", "Room name", "Start time", "End time"));
            if (slotList.Any())
            {
                foreach (Slot selectedSlot in slotList)
                {
                    Console.WriteLine(string.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}", selectedSlot.RoomID, selectedSlot.StartTime.ToString("HH:mm"), selectedSlot.StartTime.AddHours(1).ToString("HH:mm")));
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
        }

        //Print out a list of users
        public static void PrintUserList(List<User> userList, String title, String error)
        {
            Console.WriteLine(title);
            Console.WriteLine(string.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}", "ID", "Name", "Email"));

            if (userList.Any())
            {
                foreach (User selectedUser in userList)
                {
                    Console.WriteLine(string.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}", selectedUser.UserID, selectedUser.Name, selectedUser.Email));
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
        }

        public static void PrintRoomList(List<Room> roomList, String title, String error)
        {

            Console.WriteLine(title);
            Console.WriteLine(string.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}", "Room name"));

            //Check if any rooms currently exist
            if (roomList.Any())
            {
                foreach (Room selectedRoom in roomList)
                {
                    Console.WriteLine(string.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}", selectedRoom.RoomID));
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
        }

        public static void PrintSlotList(List<Slot> slotList, String title, String error)
        {
            Console.WriteLine(title);
            Console.WriteLine(string.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}", "Room name", "Start time", "End time", "Staff ID", "Bookings"));
            if (slotList.Any())
            {
                foreach (Slot selectedSlot in slotList)
                {
                    Console.WriteLine(string.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}", selectedSlot.RoomID, selectedSlot.StartTime.ToString("HH:mm"), selectedSlot.StartTime.AddHours(1).ToString("HH:mm"), selectedSlot.StaffID, selectedSlot.BookedInStudentID));
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
        }

        public static void PrintRoomAvailability(List<string> roomList, String title, String error)
        {
            Console.WriteLine(title);
            Console.WriteLine(string.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}", "Room name"));
            if (roomList.Any())
            {
                foreach (string selectedRoom in roomList)
                {
                    Console.WriteLine(string.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}", selectedRoom));
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
        }
        //PRINT METHODS END
    }

}
