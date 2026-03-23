using System;
using System.Data.SqlClient;

namespace WpfApp1
{
    public class Database
    {
        //Class is to store db methods and connection string

        //connection string for local db (copied from db properties)
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SceneItData;Integrated Security=True;";
        public bool UserValidation(string username, string password)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password", connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                int result = (int)command.ExecuteScalar(); //scalar means it will return only one value
                return result > 0; //returns true if greater than 0


            }
        }

        public bool CreateUser(string username, string password)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("INSERT INTO Users (Username, Password) VALUES (@username, @password)", connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                int result = command.ExecuteNonQuery(); //nonquery means it will return the number of rows affected
                return result > 0; //returns true if greater than 0
            }
        }

        public void SaveRating(string username, int rating, string movieID)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("INSERT INTO Ratings (Username, MovieID, Rating) VALUES (@username, @movieID, @rating)", connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@movieID", movieID);
                command.Parameters.AddWithValue("@rating", rating);
                command.ExecuteNonQuery(); //nonquery means it will return the number of rows affected
            }
        }
    }
}
