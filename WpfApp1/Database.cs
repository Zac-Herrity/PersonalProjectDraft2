using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WpfApp1
{
    public class Database
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SceneItInfo;Integrated Security=True;";
        public bool UserValidation(string username, string password)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT password FROM Users WHERE username = @username", connection);
                command.Parameters.AddWithValue("@username", username);
                var result = command.ExecuteScalar(); //scalar means it will return only one value
                if (result != null)
                {
                    string storedHashPassword = result.ToString();
                    return BCrypt.Net.BCrypt.Verify(password, storedHashPassword); //verify the password using bcrypt
                    //if password is found,return
                }
                return false; //if username isn't found


            }
        }

        public bool CreateUser(string username, string password)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); //hash the password using bcrypt
                var command = new System.Data.SqlClient.SqlCommand("INSERT INTO Users (Username, Password, LoggedIn) VALUES (@username, @password, @loggedIn)", connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", hashedPassword);
                command.Parameters.AddWithValue("@loggedIn", false);
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

        public void LoggedIn(string username, bool loggedIn)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Users SET LoggedIn = @loggedIn WHERE Username = @username", connection);
                //updates logged in status of user in db
                command.Parameters.AddWithValue("@loggedIn", loggedIn);
                command.Parameters.AddWithValue("@username", username);
                command.ExecuteNonQuery();
            }
        }
        public void LogOut(string username)
        {
            LoggedIn(username, false); //calls the logged in method to set logged in status to false
        }

        public bool SaveSeenMovies(string username, Movie movie) //saves the movie that the user has seen to the db
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(@"INSERT INTO SeenMovies 
                (Username, MovieTitle, MovieImage, ContentRating, Genre, Runtime, AverageRating, ReleaseYear, UserRating)
                VALUES
                (@username, @movieTitle, @movieImage, @contentRating, @genre, @runtime, @averageRating, @releaseYear, @userRating)",
                connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@movieTitle", movie.Title);
                command.Parameters.AddWithValue("@movieImage", movie.Image ?? "");
                command.Parameters.AddWithValue("@contentRating", movie.ContentRating ?? "");
                command.Parameters.AddWithValue("@genre", (int)movie.Genre);
                command.Parameters.AddWithValue("@runtime", movie.Runtime);
                command.Parameters.AddWithValue("@averageRating", movie.AverageRating);
                command.Parameters.AddWithValue("@releaseYear", movie.ReleaseYear ?? "");
                command.Parameters.AddWithValue("@userRating", movie.UserRating);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public List<Movie> LoadSeenMovies(string username) //loads the movies that the user has seen from the db
        {
            List<Movie> seenMovies = new List<Movie>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(@"SELECT MovieTitle, MovieImage, ContentRating, Genre, Runtime, AverageRating, ReleaseYear, UserRating
                                            FROM SeenMovies
                                            WHERE Username = @username", connection);
                command.Parameters.AddWithValue("@username", username); //gets rows from table where username matches the logged in user

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) //loops through the rows returned by the query
                    {
                        Movie movie = new Movie(0, reader["MovieTitle"].ToString(), reader["MovieImage"].ToString(), reader["ContentRating"].ToString(), reader["ReleaseYear"].ToString(),
                            (Genre)reader["Genre"], (int)reader["Runtime"], (double)reader["AverageRating"]); 
                        movie.UserRating = (int)reader["UserRating"]; //sets the user rating of the movie to the value in the db
                        seenMovies.Add(movie);
                    }
                }
            }
            return seenMovies;
        }

        public void DbExists()
        {
            string masterConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";
            //master is used to check if the db exists
            using (var connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();
                string checkDbQuery = "SELECT COUNT(*) FROM sys.databases WHERE name = 'SceneItInfo'";
                var command = new SqlCommand(checkDbQuery, connection);
                int exists = (int)command.ExecuteScalar();
                if (exists == 0)
                {
                    string createDbQuery = "CREATE DATABASE SceneItInfo";
                    var createCommand = new SqlCommand(createDbQuery, connection);
                    createCommand.ExecuteNonQuery();

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
}
