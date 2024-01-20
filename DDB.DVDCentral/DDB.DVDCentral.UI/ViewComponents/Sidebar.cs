using Microsoft.AspNetCore.Mvc;

namespace DDB.DVDCentral.UI.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(GenreManager.Load());
        }
    }
}
