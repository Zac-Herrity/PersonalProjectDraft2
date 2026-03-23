using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Rating
    {
        //Class is to store rating info for movies - users
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int RatingValue { get; set; }
    }
}
