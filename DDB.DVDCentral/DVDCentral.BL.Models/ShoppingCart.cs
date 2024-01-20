using DVDCentral.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.BL.Models
{
    public class ShoppingCart
    {
        public List<Movie> Items { get; set; } = new List<Movie>();
        public int NumItems { get; set; }
        public int TotalItems
        {
            get
            {
                int count = 0;
                foreach (Movie item in Items)
                {
                    count += item.CartQty;
                }
                return count;
            }
        }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayName("Sub Total")]
        public double SubTotal { 
            get 
            {
                double subTotal = 0;
                foreach (Movie item in Items)
                {
                    subTotal += item.Cost * item.CartQty;
                }

                return subTotal;
            } 
        }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayName("Tax")]
        public double Tax { get { return SubTotal * .055; } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayName("Order Total")]
        public double Total { get { return SubTotal + Tax; } }




    }
}
