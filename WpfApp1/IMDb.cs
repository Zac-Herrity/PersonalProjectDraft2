using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{

    #region General
    public class ProductionCompany
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class IMDb
    {
        public string id { get; set; }
        public string url { get; set; }
        public string primaryTitle { get; set; }
        public string originalTitle { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string primaryImage { get; set; }
        public List<Thumbnail> thumbnails { get; set; }
        public string trailer { get; set; }
        public string contentRating { get; set; }
        public int? startYear { get; set; }
        public object endYear { get; set; }
        public string releaseDate { get; set; }
        public List<string> interests { get; set; }
        public List<string> countriesOfOrigin { get; set; }
        public List<string> externalLinks { get; set; }
        public List<string> spokenLanguages { get; set; }
        public List<string> filmingLocations { get; set; }
        public List<ProductionCompany> productionCompanies { get; set; }
        public int? budget { get; set; }
        public int? grossWorldwide { get; set; }
        public List<string> genres { get; set; }
        public bool isAdult { get; set; }
        public int? runtimeMinutes { get; set; }
        public double? averageRating { get; set; }
        public int numVotes { get; set; }
        public int? metascore { get; set; }

        //Constructor
        public IMDb(string id, string primaryTitle, string primaryImage, string contentRating, string releaseDate, List<string> genres,
            int? runtimeMinutes, double? averageRating)
        {   this.id = id;
            this.primaryTitle = primaryTitle;
            this.primaryImage = primaryImage;
            this.contentRating = contentRating;
            this.releaseDate = releaseDate;
            this.genres = genres;
            this.runtimeMinutes = runtimeMinutes;
            this.averageRating = averageRating;
        }

    }

    public class Thumbnail
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
    #endregion

}
