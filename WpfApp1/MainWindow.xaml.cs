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
using System.Configuration;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    
    public partial class MainWindow : Window
    {
        #region API Movie Data
        string apiKey = ConfigurationManager.AppSettings["apiKey"]; //Had to add a reference to System.Configuration in order to use ConfigurationManager 
        string apiHost = ConfigurationManager.AppSettings["apiHost"]; 
        public async Task API()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", apiHost);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb236.p.rapidapi.com/api/imdb/most-popular-movies"),
            };
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    List<IMDb> IMDbOutPut = JsonConvert.DeserializeObject<List<IMDb>>(body);

                    //Converting the IMDb output into Movie objects and adding them to the movieList
                    foreach (IMDb m in IMDbOutPut)
                    {
                        // ID,PrimaryTitle, PrimaryImage, ContentRating, ReleaseDate Genre, RuntimeMinutes, AverageRating
                        string iD = m.id;
                        string title = m.primaryTitle;
                        string image = m.primaryImage;
                        string contentRating = m.contentRating;
                        string releaseDate = m.releaseDate;
                        string genreString = m.genres[0].Replace("-", ""); //removes the hyphen from genre string to match api genre
                        if (!Enum.TryParse(genreString, true, out Genre genre)) //tries to parse the genre string into a Genre enum value, if it fails it defaults to Genre.Other
                        {
                            genre = Genre.Drama;
                            //fallback if API returns an unrecognised genre
                        }


                        int? runtimeMinutes = m.runtimeMinutes;
                        double? averageRating = m.averageRating; //the ? is for nullable types, as some movies may not have a rating


                        //Creating a new Movie object with the data from the API and adding it to the movieList
                        Movie newMovie = new Movie(0, title, image, contentRating, releaseDate, genre, runtimeMinutes ?? 0, averageRating ?? 0);
                        //the ?? operator is for null coalescing, if runtimeMinutes or averageRating is null it will default to 0
                        movieList.Add(newMovie);

                    }

                    movieListBox.ItemsSource = movieList; //updates the ListBox with the new movie list from the API
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching movie data from API: " + ex.Message); //shows an error message if there is an issue with the API call
            }

        }
        #endregion

        private List<Movie> movieList = new List<Movie>(); // Main list of movies to be displayed in the app
        private List<Movie> seenMovies = new List<Movie>(); // List of movies that the user has marked as seen, will be displayed in a separate tab

        private string currentUsername; 
        //constructor, stores username for saving ratings
        public MainWindow(string username)
        {
            InitializeComponent();
            this.currentUsername = username; 
            //passed from login
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await API(); // Call the API method to get movie data when the window loads
            Database db = new Database();
            seenMovies = db.LoadSeenMovies(currentUsername);
            seenMoviesList.ItemsSource = seenMovies;
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
                poster1.Source = new BitmapImage(new Uri(selectedMovie.Image));
                poster1.Height = 150; 
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
        private void FilterSort()  
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
            Database db = new Database();
            bool saved = db.SaveSeenMovies(currentUsername, selectedMovie); //saves the seen movie to the database
            if (!saved)
            {
                MessageBox.Show("Error saving seen movie to database.");
                return;
            }
            movieList.Remove(selectedMovie); //removes the movie from the main movie list
            seenMovies.Add(selectedMovie); //adds the movie to the seen movies list

            //refresh page
            ratingCombo.SelectedIndex = -1;
            movieDetails1.Clear();
            movieTitle1.Text = "";
            poster1.Height = 0; //hides the poster image until a new movie is selected
            seenMoviesList.ItemsSource = seenMovies; //updates the seen movies listbox with the new movie
            FilterSort(); //refreshes movie list
        }

        private void seenMoviesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Movie selectedMovie = seenMoviesList.SelectedItem as Movie;
            if (selectedMovie != null)
            {
                ratingCombo2.SelectedItem = selectedMovie.UserRating; //sets the rating combo box to be the same as the chosen movie

            }
            else
            {
                ratingCombo2.SelectedIndex = -1; //if no movie is selected it resets the rating combo box

            }
            
            if (seenMoviesList.SelectedItem == selectedMovie)
            {
                movieDetails2.Text = selectedMovie.RatedMovieDetails();
                movieTitle2.Text = selectedMovie.Title;
            }

            
            moviePoster2.Source = new BitmapImage(new Uri(selectedMovie.Image)); //sets the poster image to be the same as the chosen movie
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
            if (movieList == null)
            {
                MessageBox.Show("Movie list is not loaded yet. Please try again in a moment.");
            }
            Random rand = new Random();
            int randomIndex = rand.Next(movieList.Count); //gets a random index from the movie list
            MainTabControl.SelectedIndex = 1; 
            movieListBox.SelectedItem = movieList[randomIndex]; //selects a random movie from the movie list and displays it in the main tab
            movieListBox.ScrollIntoView(movieList[randomIndex]); //scrolls the listbox to the randomly selected movie
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

        #region Logout
        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Database db = new Database();
            db.LogOut(currentUsername); 
            LoginCreation loginWindow = new LoginCreation();
            loginWindow.Show();
            this.Close();
            //Logs the user out in the database, then opens the login window and closes the main window
        }
        protected override void OnClosed(EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentUsername))
            {
                Database db = new Database();
                db.LogOut(currentUsername); 
                //Ensures that the user is logged out in the database when the window is closed
            }
        }
        #endregion
    }
}

