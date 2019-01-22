using ASR_System.Controller;
using ASR_System.Model;
using System;
using System.Linq;


namespace ASR_System.View
{
    public static class StaffMenu
    {
        //Loop to keep the within the menu system if an option fails
        public static void StaffMenuLoop()
        {
            while (true)
            {
                ShowStaffMenu();
                ReadStaffMenu();
            }
        }

        //Self explanatory print menu
        private static void ShowStaffMenu()
        {
            Console.WriteLine();
            Console.WriteLine("{0}", "------------------------------------------------------------");
            Console.WriteLine("{0}", "Staff menu:");
            Console.WriteLine("{0}. {1}", 1, "List staff");
            Console.WriteLine("{0}. {1}", 2, "Room availability");
            Console.WriteLine("{0}. {1}", 3, "Create slot");
            Console.WriteLine("{0}. {1}", 4, "Remove slot");
            Console.WriteLine("{0}. {1}", 5, "Exit");
            Console.WriteLine();

        }

        //Switch options to read the users choice of sub program
        private static void ReadStaffMenu()
        {
            string menuInput = MiscellaneousUtilities.ReadUserInput("Enter option: ");

            if (int.TryParse(menuInput, out int menuNumber))
            {
                switch (menuNumber)
                {
                    case 1:
                        ListStaff();
                        break;
                    case 2:
                        RoomAvailability();
                        break;
                    case 3:
                        CreateSlot();
                        break;
                    case 4:
                        RemoveSlot();
                        break;
                    case 5:
                        Console.WriteLine("Exiting staff menu.");
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

        //Show the staff within the system
        private static void ListStaff()
        {
            MainEngine.PrintUserList(new StaffManager().StaffList.Cast<User>().ToList(), "--- List staff ---", "<no staff>");
        }

        //Show Room availibility on a given day
        private static void RoomAvailability()
        {
            MainEngine.CheckRoomAvailability();
        }

        //COMPLETE
        //A staff member can book a maximum of 4 slots per day. - DONE
        //The slots must be booked between the school working hours of 9am to 2pm and will always be booked at the start of the hour. - DONE
        //Each room can be booked for a maximum of 2 slots per day. - DONE

        //Create a new slot
        private static void CreateSlot()
        {
            Slot newSlot;
            Console.WriteLine("---Create Slot---");

            Console.Write("Enter room name: ");
            string inputRoom = Console.ReadLine().ToUpper();

            Console.Write("Enter date for slot(dd-MM-yyyy): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter time for slot (HH:mm): ");
            string inputTime = Console.ReadLine();

            Console.Write("Enter staff ID: ");
            string inputStaffID = Console.ReadLine();

            newSlot = ValidateEngine.ValidateCreateSlot(inputRoom, inputDate, inputTime, inputStaffID);

            if (newSlot == null)
            {
                return;
            }
            else
            {
                var SlotList = new SlotManager();
                SlotList.CreateSlot(newSlot);
                Console.WriteLine("Slot created successfully."); ;
            }
        }

        //COMPLETE
        //A staff member cannot delete a slot once it has been booked by a student. - DONE

        //Remove a slot
        private static void RemoveSlot()
        {
            Slot slotResult;

            Console.WriteLine("---Remove Slot---");

            Console.Write("Enter room name: ");
            string inputRoom = Console.ReadLine().ToUpper();

            Console.Write("Enter date for slot(dd-MM-yyyy): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter time for slot (HH:mm): ");
            string inputTime = Console.ReadLine();

            slotResult = ValidateEngine.ValidateRemoveSlot(inputRoom, inputDate, inputTime);
            if (slotResult == null)
            {
                return;
            }
            else
            {
                Console.WriteLine("Slot removed successfully.");
            }
        }
    }
}
