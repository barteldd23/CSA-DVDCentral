using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDCentral.BL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public  string Title { get; set; }
        public  string Description { get; set; }
        public  double Cost { get; set; }
        [DisplayName("Rating")]
        public  int RatingId { get; set; }
        [DisplayName("Format")]
        public  int FormatId { get; set; }
        [DisplayName("Director")]
        public  int DirectorId { get; set; }
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

    }
}
