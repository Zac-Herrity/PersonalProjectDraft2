using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public enum Genre
    {
        Action,
        Comedy,
        Drama,
        Horror,
        ScienceFiction,
        Romance,
        Thriller,
        Animation,
        Documentary,
        Fantasy
    }
    public class Movie
    {
        //Movie class with properties for Title, Genre, Director and Release Year

        //Note - A lot of this is subject to change once I start working with an API to get movie data, but for now this is what I have in mind for the Movie class

        //Properties
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; } 
        public string ContentRating { get; set; }
        public Genre Genre { get; set; }
        
        public int Runtime { get; set; } 
        public double AverageRating { get; set; } 
        public string ReleaseYear { get; set; } //Using string as some movies only have release year in API
        public virtual List<User> Users { get; set; } //Navigation property for the User class, will link to the UserID in user class

        //Will try and add these later after everything else is working:
        public string Director { get; set; } 
        public string Description { get; set; }
        // Cast + Locations, budget?


        public int UserRating { get; set; } //Default at -1 (no rating)

        //Constructor
        public Movie(string MovieID, string Title, string Image, string ContentRating, string ReleaseYear, Genre genre, int Runtime, double AverageRating)
        {

        }
        //public Movie() { } //Empty constructor for when I start working with an API to get movie data

        //ToString method for only displaying the movie title in the ListBox
        public override string ToString()
        {
            return Title;
        }

        public string GetMovieDetails() //Method to get the details of the movie for when a movie is selected in the ListBox
        {
            return $"Title: {Title}\nGenre: {Genre}\nDirector: {Director}\nRelease Year: {ReleaseYear}";
        }

        public string RatedMovieDetails() //This method is seperate as I want to remove rated movies from movieList and add them to a new list of rated movies
        {
            return $"Title: {Title}\nGenre: {Genre}\nDirector: {Director}\nRelease Year: {ReleaseYear}\nUser Rating: {UserRating}/5 Stars";
        }

    }

    public class MovieData : DbContext
    {
        public MovieData() : base("SceneItData") { }

        public DbSet<User> Users { get; set; } //DbSet for the User class, will be used to store user data in the database
        public DbSet<Movie> Movies { get; set; } //DbSet for the Movie class, will be used to store movie data in the database
    }
}
