namespace DDB.DVDCentral.UI.ViewModels
{
    public class CustomerOrders
    {
        public Order Order { get; set; }
        public Customer Customer { get; set; }

        public CustomerOrders(int id) 
        {
        }
    }
}
