using Microsoft.Extensions.Configuration;

namespace ASR_System.Utilities
{
    public class SQLConnectionSingleton
    {
        //Not sure if there is a better way to store the IConfigurationRoot
        private static IConfigurationRoot _configuration { get; } = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private static SQLConnectionSingleton _instance;
        private string _connection;

        protected SQLConnectionSingleton()
        {
            _connection = _configuration["ConnectionString"];
    }

        public static SQLConnectionSingleton Instance()
        {
            if (_instance == null)
            {
                _instance = new SQLConnectionSingleton();
            }

            return _instance;
        }

        //Not using auto properties, to allow for future enchancement
        public string Connection
        {
            //could make this more complicated
            get
            {
                return _connection;
            }
        }
    }
}
