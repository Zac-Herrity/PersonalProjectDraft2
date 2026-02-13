using System;
using System.Collections.Generic;
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
    internal class Movie
    {
        //Movie class with properties for Title, Genre, Director and Release Year

        //Note - A lot of this is subject to change once I start working with an API to get movie data, but for now this is what I have in mind for the Movie class

        //Properties
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseYear { get; set; }

        //Constructor
        public Movie(string title,Genre genre, string director, DateTime releaseYear)
        {
            Title = title;
            Genre = genre;
            Director = director;
            ReleaseYear = releaseYear;
        }
        //public Movie() { } //Empty constructor for when I start working with an API to get movie data

        //ToString method for only displaying the movie title in the ListBox
        public override string ToString()
        {
            return Title;
        }

        public string GetMovieDetails() //Method to get the details of the movie for when a movie is selected in the ListBox
        {
            return $"Title: {Title}\nGenre: {Genre}\nDirector: {Director}\nRelease Year: {ReleaseYear.ToString("yyyy")}";
        }

    }
}
