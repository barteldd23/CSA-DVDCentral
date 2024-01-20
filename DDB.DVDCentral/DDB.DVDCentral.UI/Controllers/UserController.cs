using DDB.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DDB.DVDCentral.UI.Controllers
{
    public class UserController : Controller
    {
        public void Seed()
        {
            UserManager.Seed();
        }

        //sets session variable. uses the user variabler passed to it
        private void SetUser(User user)
        {
            HttpContext.Session.SetObject("user", user);

            if (user != null)
            {
                HttpContext.Session.SetObject("fullname", "Welcome " + user.FullName);
            }
            else
            {
                HttpContext.Session.SetObject("fullname", string.Empty);
            }
        }



        // GET: UserController
        public ActionResult Index()
        {
            ViewBag.Title = "User List";
            return View(UserManager.Load());
        }


        // GET: UserController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            ViewBag.Subject = "New User";
            if (Authenticate.IsAuthenticated(HttpContext))
                return View();
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                int result = UserManager.Insert(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Title = "Create";
                ViewBag.Subject = "New User";
                ViewBag.Error = "Error, Unable to Create User";
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            var item = UserManager.LoadById(id);
            ViewBag.Title = "Edit";
            ViewBag.Subject = "User: " + item.FullName;
            if (Authenticate.IsAuthenticated(HttpContext))
                return View(UserManager.LoadById(id));
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
           
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                int result = UserManager.Update(user);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var item = UserManager.LoadById(id);
                ViewBag.Title = "Edit";
                ViewBag.Subject = "User: " + item.FullName;
                return View();
            }
        }

        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                bool result = UserManager.Login(user);
                SetUser(user);

                if (TempData["returnUrl"] != null)
                    return Redirect(TempData["returnUrl"]?.ToString());


                return RedirectToAction(nameof(Index), "Movie");
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Login";
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }

        public IActionResult Logout()
        {
            ViewBag.Title = "Logged Out";
            SetUser(null);
            return View();
        }



    }
}
