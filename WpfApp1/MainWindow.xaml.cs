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
         * Add DateTime method
         * Make a Genre class with properties for Genre Name and a List of Movies in that Genre
         * Sort by Genre, Director, Release Year
         * Fill the List of Movies with data from an API (TMDB API is what I'm thinking of using)
         */

        // Dummy movie data for testing and presentation purposes
        #region Dummy Movie Data

        Movie movie1 = new Movie("Robots", Genre.Animation, "Chris Wedge", new DateTime(2005, 3, 11));
        Movie movie2 = new Movie("The Dark Knight", Genre.Action, "Christopher Nolan", new DateTime(2008, 7, 18));
        Movie movie3 = new Movie("The Hangover", Genre.Comedy, "Todd Phillips", new DateTime(2009, 6, 5));
        Movie movie4 = new Movie("The Shawshank Redemption", Genre.Drama, "Frank Darabont", new DateTime(1994, 9, 23));
        Movie movie5 = new Movie("The Conjuring", Genre.Horror, "James Wan", new DateTime(2013, 7, 19));
        Movie movie6 = new Movie("Interstellar", Genre.ScienceFiction, "Christopher Nolan", new DateTime(2014, 11, 7));
        Movie movie7 = new Movie("Titanic", Genre.Romance, "James Cameron", new DateTime(1997, 12, 19));
        Movie movie8 = new Movie("Gone Girl", Genre.Thriller, "David Fincher", new DateTime(2014, 10, 3));
        Movie movie9 = new Movie("Planet Earth", Genre.Documentary, "Alastair Fothergill", new DateTime(2006, 3, 5));
        Movie movie10 = new Movie("The Lord of the Rings: The Fellowship of the Ring", Genre.Fantasy, "Peter Jackson", new DateTime(2001, 12, 19));
        Movie movie11 = new Movie("Mad Max: Fury Road", Genre.Action, "George Miller", new DateTime(2015, 5, 15));
        Movie movie12 = new Movie("Superbad", Genre.Comedy, "Greg Mottola", new DateTime(2007, 8, 17));
        Movie movie13 = new Movie("Joker", Genre.Drama, "Todd Phillips", new DateTime(2019, 10, 4));
        Movie movie14 = new Movie("A Quiet Place", Genre.Horror, "John Krasinski", new DateTime(2018, 4, 6));
        Movie movie15 = new Movie("Avatar", Genre.ScienceFiction, "James Cameron", new DateTime(2009, 12, 18));
        Movie movie16 = new Movie("The Notebook", Genre.Romance, "Nick Cassavetes", new DateTime(2004, 6, 25));
        Movie movie17 = new Movie("Se7en", Genre.Thriller, "David Fincher", new DateTime(1995, 9, 22));
        Movie movie18 = new Movie("Frozen", Genre.Animation, "Chris Buck", new DateTime(2013, 11, 27));
        Movie movie19 = new Movie("Free Solo", Genre.Documentary, "Jimmy Chin", new DateTime(2018, 8, 31));
        Movie movie20 = new Movie("Harry Potter and the Sorcerer's Stone", Genre.Fantasy, "Chris Columbus", new DateTime(2001, 11, 16));

        #endregion



        public MainWindow()
        {
            InitializeComponent();
        }

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
