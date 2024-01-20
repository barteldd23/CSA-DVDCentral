using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDB.DVDCentral.UI.Controllers
{
    public class OrderItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Remove(int orderItemId)
        {
            OrderItemManager.Delete(orderItemId);
            return RedirectToAction(nameof(Index), "Order");
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
