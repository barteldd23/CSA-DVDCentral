using Microsoft.AspNetCore.Mvc;

namespace DDB.WebApp.UI.Controllers
{
    public class BingoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
