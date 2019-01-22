using System;

namespace ASR_System
{
    //Author: Elliot Schot <s3530160@student.rmit.edu.au>
    //Title: Appointment Scheduling and Reservation (ASR System)
    //Need: WDT Assignment 1
    //FOR MORE INFORMATION SEE WDT_Assignment_1.pdf

    //Data Validation Rules:

    //The Staff ID always starts with a letter ‘e’ followed by 5 numbers. - DONE [REGEX CHECK]
    //The Student ID always starts with a letter ‘s’ followed by 7 numbers. - DONE [REGEX CHECK]
    //Email for a staff always ends with rmit.edu.au - DONE [REGEX CHECK]
    //Email for a student always ends with student.rmit.edu.au - DONE [REGEX CHECK]
    //Each ID (Staff and Student) is unique. - DONE / NOT USED

    //Business Rules:

    //Each slot must be of 1-hour duration. - Done [REGEX CHECK]
    //A staff member can book a maximum of 4 slots per day. - DONE
    //The slots must be booked between the school working hours of 9am to 2pm and will always be booked at the start of the hour. - DONE [REGEX CHECK] + [< > TIMESPAN CHECK]
    //Each room can be booked for a maximum of 2 slots per day. - DONE
    //A staff member cannot delete a slot once it has been booked by a student. DONE [IF NULL CHECK]
    //A student can only make 1 booking per day. - DONE
    //A slot can have a maximum of 1 student booked into it. - DONE

    public static class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();
            View.MainMenu.MainMenuLoop();

            //Program should never get here
            Console.ReadLine();
        }

        //System Welcome Message
        static void WelcomeMessage()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
        }
    }
}