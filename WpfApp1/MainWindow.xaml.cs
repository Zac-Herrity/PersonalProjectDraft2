using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    
    public partial class MainWindow : Window
    {
        /*Things to do:
         * Fill the List of Movies with data from an API 
         * Look through API, what endpoints exist? Can I add more details to the Movie class based on what data I can get from the API? More features?
         * 
         * Create account classes, login / registration and store user data in db
         * 
         * IMPROVE UI - LOCK IN, pick a colour scheme, make it look nice
         * 
         * Add a home page / login
         * Add a random movie generator page
         */


        #region API Movie Data
        public async Task API()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb236.p.rapidapi.com/api/imdb/most-popular-movies"),
                Headers =
            {
            { "x-rapidapi-key", "fc3b3a9015msh458bb3fde05ca24p1becc8jsn9029ffa1b702" },
            { "x-rapidapi-host", "imdb236.p.rapidapi.com" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                List<IMDb> IMDbOutPut = JsonConvert.DeserializeObject<List<IMDb>>(body);

                //Converting the IMDb output into Movie objects and adding them to the movieList
                foreach (IMDb m in IMDbOutPut)
                {
                    // ID,PrimaryTitle, PrimaryImage, Thumbnails, ContentRating, ReleaseDate Genre, RuntimeMinutes, AverageRating
                    string iD = m.id;
                    string title = m.primaryTitle;
                    string image = m.primaryImage;
                    List<Thumbnail> thumbnails = m.thumbnails;
                    string contentRating = m.contentRating;
                    string releaseDate = m.releaseDate;
                    Genre genre = (Genre)Enum.Parse(typeof(Genre), m.genres[0]); //takes the first genre
                    int? runtimeMinutes = m.runtimeMinutes;
                    double? averageRating = m.averageRating; //the ? is for nullable types, as some movies may not have a rating





                }
            }

        }
        #endregion

        private List<Movie> movieList = new List<Movie>(); // Main list of movies to be displayed in the app
        private List<Movie> seenMovies = new List<Movie>(); // List of movies that the user has marked as seen, will be displayed in a separate tab

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             API(); // Call the API method to get movie data when the window loads
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

            ratingCombo2.Items.Add(1);
            ratingCombo2.Items.Add(2);
            ratingCombo2.Items.Add(3);
            ratingCombo2.Items.Add(4);
            ratingCombo2.Items.Add(5);
            ratingCombo2.SelectedIndex = -1; //these are for the seen movies tab

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
            Movie selectedMovie = seenMoviesList.SelectedItem as Movie;
            if (seenMoviesList.SelectedItem == selectedMovie)
            {
                movieDetails2.Text = selectedMovie.RatedMovieDetails();
                movieTitle2.Text = selectedMovie.Title;
            }

            if (selectedMovie != null)
            {
                ratingCombo2.SelectedItem = selectedMovie.UserRating; //sets the rating combo box to be the same as the chosen movie

            }
            else
            {
                ratingCombo2.SelectedIndex = -1; //if no movie is selected it resets the rating combo box

            }
        }

        private void ratingCombo2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Movie selectedMovie = seenMoviesList.SelectedItem as Movie;

            if (selectedMovie == null)
            {
                MessageBox.Show("Please select a movie to change the rating of."); //ensures that a movie is selected before trying to change the rating
                return;
            }
            if (ratingCombo2.SelectedItem == null)
            {
                MessageBox.Show("Please select a rating for the movie."); //ensures that a rating is selected before trying to change the rating
                return;
            }

            int newRating = (int)ratingCombo2.SelectedItem; //gets the new rating from the combo box
            selectedMovie.UserRating = newRating; //sets the user rating of the selected movie to the new rating
            movieDetails2.Text = selectedMovie.RatedMovieDetails(); //updates the movie details text block to reflect the new rating
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

