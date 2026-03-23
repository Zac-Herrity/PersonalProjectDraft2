using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace WpfApp1
{
    public class Database
    {
        //Class is to store db methods and connection string

        //connection string for local db (copied from db properties)
        private string connectionString = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = SceneItData; Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";

        public bool UserValidation(string username, string password)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password", connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                int result = (int)command.ExecuteScalar(); //scalar means it will return only one value
                return result > 0;


            }
    }
}
