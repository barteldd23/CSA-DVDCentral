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
        public  Guid Id { get; set; }
        public  Guid  OrderId { get; set; }
        public Guid MovieId { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }

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
