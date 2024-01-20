using DDB.DVDCentral.BL.Models;

namespace DDB.DVDCentral.UI.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public List<Customer> Customers { get; set; }
        public int UserId { get; set; }
        public ShoppingCart Cart { get; set; }
    }
}
