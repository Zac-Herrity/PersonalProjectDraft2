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
                Movie testMovie = new Movie("The Shawshank Redemption", Genre.Drama, "Frank Darabont", new DateTime(1994, 9, 22));
                User testUser = new User() { UserID = 1, Username = "testuser", Password = "password", UserRating = 5, SeenMovies = new List<string>() { testMovie.Title }, MovieID = testMovie.MovieID, Movie = testMovie };

                db.Movies.Add(testMovie);
                Console.WriteLine("Added testMovie to db. ");
                db.Users.Add(testUser);
                Console.WriteLine("Added testUser to db. ");

                db.SaveChanges();
                Console.WriteLine("Saved to database.");
            }
        }
    }
}
