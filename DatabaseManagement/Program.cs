using System;
using System.Collections.Generic;
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
            MovieData db = new MovieData(); //creates an instance of movieData class.

            using (db)
            {
                //Use API to get movie data and add it to the database, for now I will just add some dummy data
                Movie test = new Movie("The Shawshank Redemption", Genre.Drama, "Frank Darabont", new DateTime(1994, 9, 22));
                User user = new User() { UserID = 1, Username = "testuser", Password = "password", UserRating = 5, SeenMovies = new List<string>() { test.Title }, MovieID = test.MovieID, Movie = test };
            }
        }
    }
}
