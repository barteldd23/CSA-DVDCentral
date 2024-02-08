using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.BL.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public  string Title { get; set; }
        public  string Description { get; set; }
        public  double Cost { get; set; }
        [DisplayName("Rating")]
        public  Guid RatingId { get; set; }
        [DisplayName("Format")]
        public  Guid FormatId { get; set; }
        [DisplayName("Director")]
        public  Guid DirectorId { get; set; }
        [DisplayName("In Stock Quantity")]
        public  int InStkQty { get; set; }

        [DisplayName("Image")]
        public string? ImagePath { get; set; }

        [DisplayName("Rating")]
        public string RatingDescription { get; set; }
        [DisplayName("Format")]
        public string FormatDescription { get; set; }

        [DisplayName("Director")]
        public string DirectorFullName { get; set; }

        [DisplayName("Number In Cart")]
        public int CartQty { get; set; } = 0;

        public List<Genre> Genres { get; set; } = new List<Genre>();

        [DisplayName("Genres")]
        public string GenreList
        {
            get
            {
                string genreList = string.Empty;
                Genres.ForEach(a => genreList += a.Description + ", ");

                if (!string.IsNullOrEmpty(genreList))
                {
                    genreList = genreList.Substring(0, genreList.Length - 2);

                }
                return genreList;
            }

        }

    }
}
