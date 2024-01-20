using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDCentral.BL.Models
{//
    public class Order
    {
        public int Id { get; set; }
        public required int CustomerId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public required DateTime OrderDate { get; set; }
        public required int UserId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public  DateTime ShipDate { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double SubTotal { get
            {
                double total = 0;
                foreach ( OrderItem item in OrderItems)
                {
                    total += item.TotalCost;
                }
                return total;
            } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Tax { get { return SubTotal * .055; } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Total { get { return SubTotal + Tax; } }

        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get { return LastName + ", " + FirstName; } }

        [DisplayName("User Name")]
        public string UserName { get; set; }
    }
}
