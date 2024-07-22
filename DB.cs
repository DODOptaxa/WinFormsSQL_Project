using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows.Forms;

namespace WinFormsSQL
{
    class DB
    {

        private static string server = ConfigurationManager.AppSettings.Get("server");
        private static string port = ConfigurationManager.AppSettings.Get("port");
        private static string user = ConfigurationManager.AppSettings.Get("user");
        private static string password = ConfigurationManager.AppSettings.Get("password");
        private static string db = ConfigurationManager.AppSettings.Get("database");

        MySqlConnection connect = new MySqlConnection($"server={server};port={port};" +
                                                      $"username={user};password={password};database={db}");
        public void openConnection()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }

        public void closeConnection()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }

        public MySqlConnection GetConnection() 
        {
            return connect;
        }
    }
}
