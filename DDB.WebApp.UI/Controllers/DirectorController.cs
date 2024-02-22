using DDB.DVDCentral.BL;
using DDB.DVDCentral.BL.Models;
using DDB.DVDCentral.PL2.Data;
using Microsoft.AspNetCore.Authorization;


//using DDB.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DDB.WebApp.UI.Controllers
{
    public class DirectorController : Controller
    {

        private readonly DbContextOptions<DVDCentralEntities> options;


        public DirectorController(ILogger<DirectorController> logger,
                                DbContextOptions<DVDCentralEntities> options)
        {
            this.options = options;
            logger.LogWarning("I was here");
        }

        // GET: DirectorController
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Directors";
            return View(new DirectorManager(options).Load());
        }

        // GET: DirectorController/Details/5
        [AllowAnonymous]
        public ActionResult Details(Guid id)
        {
            var item = new DirectorManager(options).LoadById(id);
            ViewBag.Title = "Director";
            ViewBag.Subject = item.FullName;
            return View(item);
        }

        // GET: DirectorController/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            ViewBag.Subject = "Director";
            return View();
            }

        // POST: DirectorController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Director director)
        {
            try
            {
                int result = new DirectorManager(options).Insert(director);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: DirectorController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var item = new DirectorManager(options).LoadById(id);
            ViewBag.Title = "Edit Director";
            ViewBag.Subject = item.FullName;
            return View(item);
            
        }

        // POST: DirectorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Director director)
        {
            try
            {
                int result = new DirectorManager(options).Update(director);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(director);
            }
        }

        // GET: DirectorController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var item = new DirectorManager(options).LoadById(id);
            ViewBag.Title = "Are You sure you want to delete this?";
            ViewBag.Subject = "Director: " + item.FullName;
                return View(item);
            
        }

        // POST: DirectorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                int result = new DirectorManager(options).Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var item = new DirectorManager(options).LoadById(id);
                return View(item);
            }
        }
    }
}
