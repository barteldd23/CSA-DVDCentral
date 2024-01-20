using DDB.DVDCentral.UI.Models;
using DDB.DVDCentral.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DDB.DVDCentral.UI.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController
        public ActionResult Index()
        {
            ViewBag.Title = "Orders";
            List<Order> orders = OrderManager.Load();
            return View(orders);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            //CustomerOrders customerOrders = new CustomerOrders(id);
            //ViewBag.Title = "Order for " + customerOrders.Customer.FullName;
            //ViewBag.Subject = "Order No.: " + customerOrders.Order.Id;
            //return View(customerOrders);

            var item = OrderManager.LoadById(id);
            ViewBag.Title = "Order";
            ViewBag.Subject = "No. : " + item.Id.ToString();
            return View(item);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            ViewBag.Subject = "Order";
            if (Authenticate.IsAuthenticated(HttpContext))
                return View();
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                int result = OrderManager.Insert(order);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            var item = OrderManager.LoadById(id);
            ViewBag.Title = "Edit Order";
            ViewBag.Subject = "No. : " + item.Id.ToString();
            if (Authenticate.IsAuthenticated(HttpContext))
                return View(item);
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            try
            {
                int result = OrderManager.Update(order);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(order);
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            var item = OrderManager.LoadById(id);
            ViewBag.Title = "Are You sure you want to delete this?";
            ViewBag.Subject = "Order No.: " + item.Id.ToString();
            if (Authenticate.IsAuthenticated(HttpContext))
                return View(item);
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int result = OrderManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var item = OrderManager.LoadById(id);
                return View(item);
            }
        }
    }
}
