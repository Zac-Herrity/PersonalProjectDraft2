using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*Things to do:
         * Fill the List of Movies with data from an API (TMDB API is what I'm thinking of using)
         */

        // Dummy movie data for testing and presentation purposes
        #region Dummy Movie Data

        private List<Movie> movieList = new List<Movie>
{
            new Movie("Robots", Genre.Animation, "Chris Wedge", new DateTime(2005, 3, 11)),
            new Movie("The Dark Knight", Genre.Action, "Christopher Nolan", new DateTime(2008, 7, 18)),
            new Movie("The Hangover", Genre.Comedy, "Todd Phillips", new DateTime(2009, 6, 5)),
            new Movie("The Shawshank Redemption", Genre.Drama, "Frank Darabont", new DateTime(1994, 9, 23)),
            new Movie("The Conjuring", Genre.Horror, "James Wan", new DateTime(2013, 7, 19)),
            new Movie("Interstellar", Genre.ScienceFiction, "Christopher Nolan", new DateTime(2014, 11, 7)),
            new Movie("Titanic", Genre.Romance, "James Cameron", new DateTime(1997, 12, 19)),
            new Movie("Gone Girl", Genre.Thriller, "David Fincher", new DateTime(2014, 10, 3)),
            new Movie("Planet Earth", Genre.Documentary, "Alastair Fothergill", new DateTime(2006, 3, 5)),
            new Movie("The Lord of the Rings: The Fellowship of the Ring", Genre.Fantasy, "Peter Jackson", new DateTime(2001, 12, 19)),
            new Movie("Mad Max: Fury Road", Genre.Action, "George Miller", new DateTime(2015, 5, 15)),
            new Movie("Superbad", Genre.Comedy, "Greg Mottola", new DateTime(2007, 8, 17)),
            new Movie("Joker", Genre.Drama, "Todd Phillips", new DateTime(2019, 10, 4)),
            new Movie("A Quiet Place", Genre.Horror, "John Krasinski", new DateTime(2018, 4, 6)),
            new Movie("Avatar", Genre.ScienceFiction, "James Cameron", new DateTime(2009, 12, 18)),
            new Movie("The Notebook", Genre.Romance, "Nick Cassavetes", new DateTime(2004, 6, 25)),
            new Movie("Se7en", Genre.Thriller, "David Fincher", new DateTime(1995, 9, 22)),
            new Movie("Frozen", Genre.Animation, "Chris Buck", new DateTime(2013, 11, 27)),
            new Movie("Free Solo", Genre.Documentary, "Jimmy Chin", new DateTime(2018, 8, 31)),
            new Movie("Harry Potter and the Sorcerer's Stone", Genre.Fantasy, "Chris Columbus", new DateTime(2001, 11, 16))
        };

        private List<Movie> seenMovies = new List<Movie>(); // List to store movies marked as seen by the user
        #endregion



        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            movieListBox.ItemsSource = movieList; // Bind the movie list to the ListBox
            genreCombo.ItemsSource = Enum.GetValues(typeof(Genre)); // Bind the Genre enum values to the ComboBox for filtering

            //The sorting combobox
            sortByCombo.Items.Add("Title");
            sortByCombo.Items.Add("Release Year");
            sortByCombo.SelectedIndex = 0; // Default to sorting by title
            ratingCombo.SelectedIndex = -1; // force the user to select a rating (prevents default selection of 1)

            //filling rating combo box
            ratingCombo.Items.Add(1);
            ratingCombo.Items.Add(2);
            ratingCombo.Items.Add(3);
            ratingCombo.Items.Add(4);
            ratingCombo.Items.Add(5);

        }

        #region Movie Populating and Filtering
        private void movieListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (movieListBox.SelectedItem is Movie selectedMovie)
            {
                movieDetails1.Text = selectedMovie.GetMovieDetails();
                movieTitle1.Text = selectedMovie.Title;
            }
        }
        private void genreCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterSort(); // Call the method to filter and sort the movie list whenever the genre selection changes                                                                                                                                                                                 
        }
        private void sortByCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterSort(); // Call the method to filter and sort the movie list whenever the sorting selection changes
        }
        private void FilterSort() //Method is for 
        {
            List<Movie> genreList = new List<Movie>(movieList);
            if (genreCombo.SelectedItem != null)
            {
                Genre selectedGenre = (Genre)genreCombo.SelectedItem; //filets movie based on selected genre (if there is one)
                List<Movie> filteredList = new List<Movie>();
                foreach (Movie m in genreList)
                {
                    if (m.Genre == selectedGenre)
                    {
                        filteredList.Add(m); //if movie is same genre as selcted one it is then added to new list
                    }
                }
                genreList = filteredList;
            }

            string sortingChoice = sortByCombo.SelectedItem as string; //sorts the list based on the sorting choice in the combo box
            if (sortingChoice == "Title")
            {
                genreList = genreList.OrderBy(m => m.Title).ToList();
            }
            else if (sortingChoice == "Release Year")
            {
                genreList = genreList.OrderBy(m => m.ReleaseYear).ToList();
            }
            movieListBox.ItemsSource = genreList; //updates the listbox with the new sorted and filtered list
        }

        private void seenItBtn_Click(object sender, RoutedEventArgs e)
        {
            Movie selectedMovie = movieListBox.SelectedItem as Movie; 
            if (selectedMovie == null)
            {
                MessageBox.Show("Please select a movie to mark as seen."); //ensures that a movie is selected before trying to mark it as seen
                return;
            }

            if (ratingCombo.SelectedItem == null)
            {
                MessageBox.Show("Please select a rating for the movie."); //ensures that a rating is selected before trying to mark the movie as seen
                return;
            }

            int chosenRating = (int)ratingCombo.SelectedItem; //gets the selected rating from the combo box
            selectedMovie.UserRating = chosenRating; //sets the user rating of the selected movie to the chosen rating

            movieList.Remove(selectedMovie); //removes the movie from the main movie list
            seenMovies.Add(selectedMovie); //adds the movie to the seen movies list

            //refresh page
            ratingCombo.SelectedIndex = -1;
            movieDetails1.Clear();
            movieTitle1.Text = "";
            seenMoviesList.ItemsSource = seenMovies; //updates the seen movies listbox with the new movie
            FilterSort(); //refreshes movie list
        }

        private void seenMoviesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (seenMoviesList.SelectedItem is Movie selectedMovie)
            {
                movieDetails2.Text = selectedMovie.RatedMovieDetails();
                movieTitle2.Text = selectedMovie.Title;
            }
        }

        #endregion

        #region Nav Buttons
        private void homeBtnList_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 1;
        }

        private void homeBtnSeen_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 2;
        }

        private void homeBtnRandom_Click(object sender, RoutedEventArgs e)
        {
            //Yet to be implemented
        }

        private void exit1_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 0;
        }

        private void exit2_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 0;
        }








        #endregion

        
    }
}
