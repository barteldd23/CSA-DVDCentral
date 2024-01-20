using DDB.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DDB.DVDCentral.UI.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomerController
        public ActionResult Index()
        {
            ViewBag.Title = "Customers";
            return View(CustomerManager.Load());
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var item = CustomerManager.LoadById(id);
            ViewBag.Title = "Customer";
            ViewBag.Subject = item.FullName;
            return View(item);
        }

        // GET: CustomerController/Create
        public ActionResult Create(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            TempData["returnUrl"] = returnUrl;

            ViewBag.Title = "Create";
            
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Subject = "Customer for " + HttpContext.Session.GetObject<User>("user").FullName;
                ViewBag.userId = HttpContext.Session.GetObject<User>("user").Id;
                return View();
            }
                
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer rating)
        {
            try
            {
                int result = CustomerManager.Insert(rating);

                if (TempData["returnUrl"] != null)
                    return Redirect(TempData["returnUrl"]?.ToString());

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var item = CustomerManager.LoadById(id);
            ViewBag.Title = "Edit Customer";
            ViewBag.Subject = item.FullName;
            if (Authenticate.IsAuthenticated(HttpContext))
                return View(item);
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                int result = CustomerManager.Update(customer);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(customer);
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            var item = CustomerManager.LoadById(id);
            ViewBag.Title = "Are You sure you want to delete this?";
            ViewBag.Subject = "Customer: " + item.FullName;
            if (Authenticate.IsAuthenticated(HttpContext))
                return View(item);
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int result = CustomerManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var item = CustomerManager.LoadById(id);
                return View(item);
            }
        }
    }
}
