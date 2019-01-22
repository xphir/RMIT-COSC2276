using ASR_System.Controller;
using System;

namespace ASR_System.View
{
    public static class MainMenu
    {
        //Loop to keep the within the menu system if an option fails
        public static void MainMenuLoop()
        {
            while (true)
            {
                ShowMainMenu();
                ReadMainMenu();
            }
        }

        //Self explanatory print menu
        private static void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("{0}", "Main menu:");
            Console.WriteLine("{0}. {1}", 1, "List rooms");
            Console.WriteLine("{0}. {1}", 2, "List slots");
            Console.WriteLine("{0}. {1}", 3, "Staff menu");
            Console.WriteLine("{0}. {1}", 4, "Student menu");
            Console.WriteLine("{0}. {1}", 5, "Exit");
            Console.WriteLine();
        }

        //Switch options to read the users choice of sub program
        private static void ReadMainMenu()
        {
            string menuInput = MiscellaneousUtilities.ReadUserInput("Enter option: ");

            if (int.TryParse(menuInput, out int menuNumber))
            {
                switch (menuNumber)
                {
                    case 1:
                        ShowListRooms();
                        break;
                    case 2:
                        ShowListSlots();
                        break;
                    case 3:
                        StaffMenu.StaffMenuLoop();
                        break;
                    case 4:
                        StudentMenu.StudentMenuLoop();
                        break;
                    case 5:
                        Console.WriteLine("Terminating ASR.");
                        Environment.Exit(0);
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

        //Show the list of rooms within the system
        private static void ShowListRooms()
        {
            MainEngine.PrintRoomList(new RoomManager().RoomList, "---List rooms---", "<no rooms>");
        }

        //DONE

        //Show the list of slots within the system
        private static void ShowListSlots()
        {
            MainEngine.PrintSlotList(new SlotManager().SlotList, "---List slots---", "<no slots>");        }
    }
}
