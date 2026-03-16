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
                //Movie testMovie = new Movie("tt0111161", "The Shawshank Redemption", "https://m.media-amazon.com/images/M/MV5BMDFkYTc0MGEtZmNhMC00ZDIzLWFmNTEtODM1ZmRlYjM3NjA2XkEyXkFqcGdeQXVyNDYyMDk5MTU@._V1_.jpg", "R", "1994", Genre.Drama, 142, 9.3);
                //User testUser = new User() { UserID = "1", Username = "testuser", Password = "password", UserRating = 5, SeenMovies = new List<string>() { testMovie.Title }, Movie = testMovie };

                /*db.Movies.Add(testMovie);
                Console.WriteLine("Added testMovie to db. ");
                db.Users.Add(testUser);
                Console.WriteLine("Added testUser to db. ");*/

                //Test 2, adding new users to database for login / account creation
                User Zac = new User("2", "Zac", "password");
                db.Users.Add(Zac);

                db.SaveChanges();
                Console.WriteLine("Saved to database.");
            }

            
        }
    }
}
