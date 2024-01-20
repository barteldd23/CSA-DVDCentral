using DDB.DVDCentral.BL.Models;
using DDB.DVDCentral.UI.Models;
using DDB.DVDCentral.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DDB.DVDCentral.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;

        // GET: ShoppingCartController
        public IActionResult Index()
        {
            ViewBag.Title = "Shopping Cart";
            cart = GetShoppingCart();
            HttpContext.Session.SetObject("cart", cart);
            return View(cart);
        }

        private ShoppingCart GetShoppingCart()
        {
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") != null)
            {
                return HttpContext.Session.GetObject<ShoppingCart>("cart");
            }
            else
            {
                return new ShoppingCart();
            }
        }

        public IActionResult Remove(int id)
        {
            cart = GetShoppingCart();

            //do this way to prevent a hit on the DB!
            Movie movie = cart.Items.FirstOrDefault(item => item.Id == id);

            ShoppingCartManager.Remove(cart, movie);
            HttpContext.Session.SetObject("cart", cart);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Add(int id)
        {
            cart = GetShoppingCart();
            Movie movie = MovieManager.LoadById(id);

            ShoppingCartManager.Add(cart, movie);
            HttpContext.Session.SetObject("cart", cart);
            return RedirectToAction(nameof(Index), "Movie");
        }

        public IActionResult Checkout()
        {
            cart = GetShoppingCart();

            if (cart.TotalItems == 0)
            {
                ViewBag.Error = "No Items in you Cart";
                return View(nameof(Index), cart);
            }
                
            
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                User user = HttpContext.Session.GetObject<User>("user");

                CustomerViewModel customerViewModel = null;
                customerViewModel = new CustomerViewModel();
                customerViewModel.UserId = user.Id;
                customerViewModel.Customers = CustomerManager.Load(user.Id);
                if (customerViewModel.Customers.Count == 0) 
                {
                    customerViewModel.Customers = CustomerManager.Load();
                }
                customerViewModel.Cart = cart;

                ViewBag.Title = "Processs Order";
                ViewBag.Subject = "Assign to a Customer";

                ViewData["ReturnUrl"] = UriHelper.GetDisplayUrl(HttpContext.Request);

                return View(customerViewModel);
            }

            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            
        }

        [HttpPost]
        public IActionResult FinishCheckout(CustomerViewModel customerViewModel)
        {
            customerViewModel.Cart = GetShoppingCart();
            string message = ShoppingCartManager.Checkout(customerViewModel.Cart, customerViewModel.CustomerId);

            //no more cart after we check out
            if(message == "Thank You For Your Order") 
            {
                ViewBag.chkOutMsg_1 = "Thank you";
                ViewBag.chkOutMsg_2 = "Your order has been processed";
                ViewBag.chkOutMsg_3 = "Your movie(s) will be shipped in 1-2 bussiness days";
                HttpContext.Session.SetObject("cart", null);
            }
            else
            {
                ViewBag.Error = message;
            }
            
            
            return View();
        }

        //public IActionResult CreateNewCustomer()
        //{
        //    return RedirectToAction("Create", "Customer", new { returnUrl = "/ShoppingCart/Checkout" });
        //}

    }
}

