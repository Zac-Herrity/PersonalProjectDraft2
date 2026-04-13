using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1;

namespace DatabaseManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SceneItInfo;Integrated Security=True;";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //create tables
                string usersTableQuery = @"CREATE TABLE Users (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Username NVARCHAR(100) NOT NULL UNIQUE,
                        Password NVARCHAR(100) NOT NULL,
                        LoggedIn BIT NOT NULL)";
                var usersCommand = new SqlCommand(usersTableQuery, connection);
                usersCommand.ExecuteNonQuery();
                string seenMoviesTableQuery = @"CREATE TABLE SeenMovies (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Username NVARCHAR(100) NOT NULL,
                        MovieTitle NVARCHAR(255) NOT NULL,
                        MovieImage NVARCHAR(255),
                        ContentRating NVARCHAR(50),
                        Genre INT,
                        Runtime INT,
                        AverageRating FLOAT,
                        ReleaseYear NVARCHAR(50) NULL,
                        UserRating INT)";
                var seenMoviesCommand = new SqlCommand(seenMoviesTableQuery, connection);
                seenMoviesCommand.ExecuteNonQuery();
            }
        }
    }
}
