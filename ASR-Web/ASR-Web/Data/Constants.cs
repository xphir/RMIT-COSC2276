using System;

namespace Asr.Data
{
    public static class Constants
    {
        public const string StaffRole = "Staff";
        public const string StudentRole = "Student";

        //True if you want past results, False if you dont want past results
        public static bool PastResults = false;

        //GLOBAL DECLARATIONS
        public const int STAFF_DAILY_BOOKING_LIMIT = 4;
        public const int STUDENT_DAILY_BOOKING_LIMIT = 1;
        public const int ROOM_DAILY_BOOKING_LIMIT = 2;
        public const int SLOT_BOOKING_LIMIT = 1;
        public const int ROOM_DOUBLEBOOKING_CHECK = 1;
        public const string PRINT_INDENT = "\t";
        public static readonly TimeSpan START_TIME = new TimeSpan(9, 0, 0); // 9:00am
        public static readonly TimeSpan END_TIME = new TimeSpan(13, 0, 0); // 1:00pm - Booking at 1:00pm will end 2:00pm
    }
}
