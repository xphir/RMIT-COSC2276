using System.Collections.Generic;
using System.Linq;
using ASR_System.Model;
using ASR_System.Utilities;


namespace ASR_System.Controller
{
    class StaffManager
    {
        public List<Staff> StaffList { get; }

        public StaffManager()
        {
            using (var connection = SQLConnectionSingleton.Instance().Connection.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from [User] where UserID like 'e%'";

                StaffList = command.GetDataTable().Select().Select(x =>
                    new Staff((string)x["UserID"], (string)x["Name"], (string)x["Email"])).ToList();
            }
        }
    }
}
