using BDF.Utility;
using DDB.DVDCentral.PL2.Entities;
using DDB.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DDB.DVDCentral.UI.Controllers
{
    public class GenericController<T> : Controller where T : class
    {
        // dynamic variable ?
        dynamic manager;

        private ApiClient apiClient;     // Api client Helper.

        public GenericController(HttpClient httpClient)
        {
            this.apiClient = new ApiClient(httpClient.BaseAddress.AbsoluteUri);
            manager = (T)Activator.CreateInstance(typeof(T));
        }

        // NEED VIRTUAL WHEN DOING GENERIC WITH INJECTION
        public virtual IActionResult Index()
        {
            ViewBag.Title = "List of " + typeof(T).Name + "s";
            var entities = apiClient.GetList<T>(typeof(T).Name);
            return View(entities);
        }

        public virtual ActionResult Details(Guid id)
        {
            string methodname = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ViewBag.Title = methodname + " for " + typeof(T).Name;

            var entity = apiClient.GetItem<T>(typeof(T).Name, id);
            return View(entity);
        }

        // GET: FormatController/Create
        public virtual IActionResult Create()
        {
            string methodname = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ViewBag.Title = methodname + " for " + typeof(T).Name;

            return View();

            }

        // POST: FormatController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Create(T entity, bool rollback = false)
        {
            string methodname = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                
                ViewBag.Title = methodname + " for " + typeof(T).Name;

                var response = apiClient.Post<T>(entity, typeof(T).Name);
                var result = response.Content.ReadAsStringAsync().Result;

                // TODO Get the id

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = methodname + " for " + typeof(T).Name;
                ViewBag.Error = ex.Message;
                return View(entity);
            }
        }

        // GET: FormatController/Edit/5
        public ActionResult Edit(Guid id)
        {
            string methodname = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ViewBag.Title = methodname + " for " + typeof(T).Name;

            var entity = apiClient.GetItem<T>(typeof(T).Name, id);
            return View(entity);
        }

        // POST: FormatController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, T entity, bool rollback = false)
        {
            string methodname = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {

                ViewBag.Title = methodname + " for " + typeof(T).Name;

                var response = apiClient.Put<T>(entity, typeof(T).Name, id);
                var result = response.Content.ReadAsStringAsync().Result;


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = methodname + " for " + typeof(T).Name;
                ViewBag.Error = ex.Message;
                return View(entity);
            }
        }

        // GET: FormatController/Delete/5
        public ActionResult Delete(Guid id)
        {
            string methodname = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ViewBag.Title = methodname + " for " + typeof(T).Name;

            var entity = apiClient.GetItem<T>(typeof(T).Name, id);
            return View(entity);
        }

        // POST: FormatController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, T entity, bool rollback = false)
        {
            try
            {
                var response = apiClient.Delete(typeof(T).Name, id);
                var result = response.Content.ReadAsStringAsync().Result;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(entity);
            }
        }
    }
}
