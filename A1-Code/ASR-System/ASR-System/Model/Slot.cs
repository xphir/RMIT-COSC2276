using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ASR_System.Model
{
    public class Slot
    {
        public string RoomID { get; }
        public DateTime StartTime { get; }
        public string StaffID { get; }
        public string BookedInStudentID { get; set; }

        public Slot(string roomID, DateTime startTime, string staffID)
        {
            RoomID = roomID;
            StartTime = startTime;
            StaffID = staffID;
            BookedInStudentID = null;
        }

        public Slot(string roomID, DateTime startTime, string staffID, string bookedInStudentID)
        {
            RoomID = roomID;
            StartTime = startTime;
            StaffID = staffID;
            BookedInStudentID = bookedInStudentID;
        }

        public Boolean makeBooking(string studentID)
        {
            if(BookedInStudentID == null)
            {
                //Slot Can be booked
                BookedInStudentID = studentID;
                return true;
            }
            else
            {
                Console.WriteLine("Error: Selected slot already has a booking");
                return false;
            }
        }

        public int CountStaffBooking(string staffID)
        {
            if(User.StaffIdValidation(staffID))
            {
                //StaffID is valid
            }
            else
            {
                //StaffID is not valid
            }
            return 0;
        }


        //a) Each slot must be of 1-hour duration.
        //b) A staff member can book a maximum of 4 slots per day.
        //c) The slots must be booked between the school working hours of 9am to 2pm and will always be booked at the start of the hour, e.g., 10:00am, 1:00pm, etc…
        //d) Each room can be booked for a maximum of 2 slots per day.
        //e) A staff member cannot delete a slot once it has been booked by a student.
        //f) A student can only make 1 booking per day.
        //g) A slot can have a maximum of 1 student booked into it.



        //TIME Validation/Conversion methods
        public static DateTime? ValidateDate(string inputDate)
        {
            //If inputDate passes the regex check and the TryParseExact return a DateTime else return null
            if ((Slot.dateRegexCheck(inputDate)) && (DateTime.TryParseExact(inputDate, "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime dateOnly)))
            {
                return dateOnly;
            }

            return null;
        }

        public static TimeSpan? ValidateTime(string inputTime)
        {
            //If inputTime passes the regex check and the TryParseExact return a TimeSpan else return null
            if ((Slot.timeRegexCheck(inputTime)) && (TimeSpan.TryParseExact(inputTime, "hh\\:mm", null, TimeSpanStyles.None, out TimeSpan timeOnly)))
            {
                if ((timeOnly >= MiscellaneousUtilities.START_TIME) && (timeOnly <= MiscellaneousUtilities.END_TIME))
                {
                    return timeOnly;
                }
            }
            return null;
        }

        public static DateTime? ProcessDate(string inputDate, string inputTime, string errorArea)
        {
            // Unable to cancel booking
            DateTime dateOnly;
            DateTime? dateOnlyNullable;
            TimeSpan? timeOnlyNullable;
            TimeSpan timeOnly;
            DateTime? combinedTime;

            //VALIDATE THE DATE
            dateOnlyNullable = ValidateDate(inputDate);
            if (!(dateOnlyNullable.HasValue))
            {
                Console.WriteLine(errorArea + " : Invalid Date");
                return null;
            }
            else
            {
                //Cast nullable to normal
                dateOnly = (DateTime)dateOnlyNullable;
            }

            //VALIDATE THE TIME
            timeOnlyNullable = Slot.ValidateTime(inputTime);
            if (!(timeOnlyNullable.HasValue))
            {
                Console.WriteLine(errorArea + " : Invalid Time");
                return null;
            }
            else
            {
                //Cast nullable to normal
                timeOnly = (TimeSpan)timeOnlyNullable;
            }

            //COMBINE THE DATE AND TIME
            try
            {
                combinedTime = dateOnly.Add(timeOnly);
            }
            catch (Exception)
            {
                Console.WriteLine(errorArea + " : Invalid Date + Time Join");
                return null;
            }

            return combinedTime;
        }

        //DATA VALIDATION METHODS

        // HH:MM REGEX ^([0-1][0-9]|2[0-3]):[0-5][0-9]$
        // HH:00 REGEX ^([0-1][0-9]|2[0-3]):00$
        public static bool timeRegexCheck(string inputTime)
        {
            string timeRegexString = @"^([0-1][0-9]|2[0-3]):00$";
            return Regex.IsMatch(inputTime, timeRegexString);
        }

        //dd-mm-yyyy REGEX ^((0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-[12]\d{3})$
        public static bool dateRegexCheck(string inputDate)
        {
            string dateRegexString = @"^((0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-[12]\d{3})$";
            return Regex.IsMatch(inputDate, dateRegexString);
        }
    }
}
