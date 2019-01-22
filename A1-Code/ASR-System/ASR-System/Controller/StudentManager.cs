using System.Collections.Generic;
using System.Linq;
using ASR_System.Model;
using ASR_System.Utilities;

namespace ASR_System.Controller
{
    class StudentManager
    {
        public List<Student> StudentList { get; }

        public StudentManager()
        {
            using (var connection = SQLConnectionSingleton.Instance().Connection.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from [User] where UserID like 's%'";

                StudentList = command.GetDataTable().Select().Select(x =>
                    new Student((string)x["UserID"], (string)x["Name"], (string)x["Email"])).ToList();
            }
        }
    }
}
