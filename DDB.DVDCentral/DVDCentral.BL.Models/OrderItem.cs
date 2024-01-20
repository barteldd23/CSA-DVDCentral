using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDCentral.BL.Models
{
    public class OrderItem
    {
        public  int Id { get; set; }
        public  int  OrderId { get; set; }
        public required int MovieId { get; set; }
        public required int Quantity { get; set; }
        public required double Cost { get; set; }

        [DisplayName("Movie Title")]
        public string MovieTitle { get; set; }
        public string MovieImagePath { get; set; }

        [DisplayName("Total Item Cost")]
        public double TotalCost
        {
            get { return Cost * Quantity; }
        }
    }
}
