using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class User
    {
        //User Class, will be used for Login and Register features, will be stored in a Database and
        //will also contain seen movies and rated movies per user

        //Properties
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool LoggedIn { get; set; }
        public int UserRating { get; set; }
        public List<string> SeenMovies { get; set; } //List of movie titles that the user has seen, will link to the MovieID in movie class
        public int MovieID { get; set; } //will link to the MovieID in movie class
        public virtual Movie Movie { get; set; } //Navigation property for the Movie class

        //Constructor
        public User(string UserID, string Username, string Password) 
        {
            this.UserID = UserID;
            this.Username = Username;
            this.Password = Password;
        }



    }
}
