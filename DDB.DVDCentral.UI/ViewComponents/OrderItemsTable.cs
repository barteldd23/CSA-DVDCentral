using Microsoft.AspNetCore.Mvc;

namespace DDB.DVDCentral.UI.ViewComponents
{
    public class OrderItemsTable : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //Dont know how to get the list of orderItems for a specific order sent to this view call:
            return View();
        }
    }
}
