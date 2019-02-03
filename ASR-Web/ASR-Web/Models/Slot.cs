using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASR_Web.Models;
using System.Globalization;
using System.Text.RegularExpressions;
using Asr.Data;

namespace ASR_Web.Models
{
    public class Slot
    {
        [Required]
        public string RoomID { get; set; }
        public virtual Room Room { get; set; }

        //[DisplayFormat(DataFormatString = "{0:s}")]
        public DateTime StartTime { get; set; }

        [Required]
        public string StaffID { get; set; }
        public virtual Staff Staff { get; set; }

        [DisplayFormat(NullDisplayText = "No Booking", ApplyFormatInEditMode = true)]
        public string StudentID { get; set; }
        public virtual Student Student { get; set; }



        //TIME Validation/Conversion methods
        public static DateTime? ValidateDate(DateTime inputDate)
        {
            //Check booking is in the future
            if(inputDate < DateTime.Now)
            {
                return null;
            }
            
            //Check date is within the bookable time frame
            if ((inputDate.TimeOfDay >= Constants.START_TIME) && (inputDate.TimeOfDay <= Constants.END_TIME))
            {
                return inputDate;
            }

            return null;
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
