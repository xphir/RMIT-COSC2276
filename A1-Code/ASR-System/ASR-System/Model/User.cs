using System.Text.RegularExpressions;

namespace ASR_System.Model
{
    public abstract class User
    {
        public string UserID { get; }
        public string Name { get; set; }
        public string Email { get; set; }

        protected User(string userID, string name, string email)
        {
            UserID = userID;
            Name = name;
            Email = email;
        }

        //DATA VALIDATION METHODS

        //The Staff ID always starts with a letter ‘e’ followed by 5 numbers.
        //REGEX IS ^e+\d{5}$
        public static bool StaffIdValidation(string staffId)
        {
            string regexString = @"^e+\d{5}$";
            return Regex.IsMatch(staffId, regexString);
        }

        //The Student ID always starts with a letter ‘s’ followed by 7 numbers.
        //REGEX IS ^s+\d{7}$
        public static bool StudentIdValidation(string studentId)
        {
            string regexString = @"^s+\d{7}$";
            return Regex.IsMatch(studentId, regexString);
        }

        //Email for a staff always ends with rmit.edu.au
        //REGEX IS ^(e+\d{5})+@+rmit.edu.au$
        public static bool StaffEmailValidation(string staffEmail)
        {
            string regexString = @"^(e+\d{5})+@+rmit.edu.au$";
            return Regex.IsMatch(staffEmail, regexString);
        }


        //Email for a student always ends with student.rmit.edu.au
        //REGEX IS ^(s+\d{7})+@+student.rmit.edu.au$
        public static bool StudentEmailValidation(string studentEmail)
        {
            string regexString = @"^(s+\d{7})+@+student.rmit.edu.au$";
            return Regex.IsMatch(studentEmail, regexString);
        }
    }
}
