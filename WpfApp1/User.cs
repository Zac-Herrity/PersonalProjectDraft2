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
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> SeenMovies { get; set; }
        public int UserRating { get; set; }
    }
}
