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
                //May be needed to recreate the database
            }

            
        }
    }
}
