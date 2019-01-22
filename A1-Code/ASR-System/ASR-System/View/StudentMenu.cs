using ASR_System.Controller;
using ASR_System.Model;
using System;
using System.Linq;

namespace ASR_System.View
{
    public static class StudentMenu
    {
        //Loop to keep the within the menu system if an option fails
        public static void StudentMenuLoop()
        {
            while (true)
            {
                ShowStudentMenu();
                ReadStudentMenu();
            }
        }

        //Self explanatory print menu
        private static void ShowStudentMenu()
        {
            Console.WriteLine();
            Console.WriteLine("{0}", "------------------------------------------------------------");
            Console.WriteLine("{0}", "Student menu:");
            Console.WriteLine("{0}. {1}", 1, "List students");
            Console.WriteLine("{0}. {1}", 2, "Staff availability");
            Console.WriteLine("{0}. {1}", 3, "Make booking");
            Console.WriteLine("{0}. {1}", 4, "Cancel booking");
            Console.WriteLine("{0}. {1}", 5, "Exit");
            Console.WriteLine();

        }

        //Switch options to read the users choice of sub program
        private static void ReadStudentMenu()
        {
            string menuInput = MiscellaneousUtilities.ReadUserInput("Enter option: ");

            if (int.TryParse(menuInput, out int menuNumber))
            {
                switch (menuNumber)
                {
                    case 1:
                        ListStudents();
                        break;
                    case 2:
                        StaffAvailability();
                        break;
                    case 3:
                        MakeBooking();
                        break;
                    case 4:
                        CancelBooking();
                        break;
                    case 5:
                        Console.WriteLine("Exiting student menu.");
                        MainMenu.MainMenuLoop();
                        break;
                    default:
                        Console.WriteLine("Invalid input: {0}", menuNumber);
                        break;
                }
            }
            else
            {
                Console.WriteLine("invalid input value: {0}", menuInput);
            }
        }

        //DONE

        //Show the students within the system
        private static void ListStudents()
        {
            MainEngine.PrintUserList(new StudentManager().StudentList.Cast<User>().ToList(), "--- List students ---", "<no students>");
        }

        //DONE

        //Show staff availible (open slot they own) on a given day
        private static void StaffAvailability()
        {
            MainEngine.CheckStaffAvailability();
        }

        //A student can only make 1 booking per day. - DONE
        //A slot can have a maximum of 1 student booked into it. - DONE

        //Create a booking (assign the studentID to a slots booking field) for a specified slot
        static void MakeBooking()
        {
            Slot newSlot;

            Console.WriteLine("---Make booking---");

            Console.Write("Enter room name: ");
            string inputRoom = Console.ReadLine().ToUpper();

            Console.Write("Enter date for slot(dd-MM-yyyy): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter time for slot (HH:mm): ");
            string inputTime = Console.ReadLine();

            Console.Write("Enter student ID: ");
            string inputStudentID = Console.ReadLine();

            newSlot = ValidateEngine.ValidateBookSlot(inputRoom, inputDate, inputTime, inputStudentID);

            if (newSlot == null)
            {
                return;
            }
            else
            {
                var SlotList = new SlotManager();
                SlotList.UpdateBooking(newSlot);
                Console.WriteLine("Booking created successfully."); ;
            }
        }

        //DONE

        //Delete/Cancel a booking (assign the slot booking field to null) for a specified slot
        private static void CancelBooking()
        {
            Slot newSlot;

            Console.WriteLine("---Cancel booking---");

            Console.Write("Enter room name: ");
            string inputRoom = Console.ReadLine().ToUpper();

            Console.Write("Enter date for slot(dd-MM-yyyy): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter time for slot (HH:mm): ");
            string inputTime = Console.ReadLine();

            newSlot = ValidateEngine.ValidateCancelBookSlot(inputRoom, inputDate, inputTime);

            if (newSlot == null)
            {
                return;
            }
            else
            {
                var SlotList = new SlotManager();
                SlotList.UpdateBooking(newSlot);
                Console.WriteLine("Slot cancelled successfully."); ;
            }
        }

    }
}
