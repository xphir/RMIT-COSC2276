using System.Collections.Generic;
using System.Linq;
using ASR_System.Model;
using ASR_System.Utilities;


namespace ASR_System.Controller
{
    class RoomManager
    {
        public List<Room> RoomList { get; }

        public RoomManager()
        {
            using (var connection = SQLConnectionSingleton.Instance().Connection.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from Room";

                RoomList = command.GetDataTable().Select().Select(x =>
                    new Room((string)x["RoomID"])).ToList();
            }
        }
    }
}