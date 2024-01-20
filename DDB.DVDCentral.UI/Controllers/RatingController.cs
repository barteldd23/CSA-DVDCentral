using DDB.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace DDB.DVDCentral.UI.Controllers
{
    public class RatingController : Controller
    {
        #region "Pre Web-API"
        // GET: RatingController
        public ActionResult Index()
        {
            ViewBag.Title = "Ratings";
            return View(RatingManager.Load());
        }

        // GET: RatingController/Details/5
        public ActionResult Details(int id)
        {
            var item = RatingManager.LoadById(id);
            ViewBag.Title = "Rating";
            ViewBag.Subject = item.Description;
            return View(item);
        }

        // GET: RatingController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            ViewBag.Subject = "Rating";
            if (Authenticate.IsAuthenticated(HttpContext))
                return View();
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        // POST: RatingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rating rating)
        {
            try
            {
                int result = RatingManager.Insert(rating);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: RatingController/Edit/5
        public ActionResult Edit(int id)
        {
            var item = RatingManager.LoadById(id);
            ViewBag.Title = "Edit Rating";
            ViewBag.Subject = item.Description;
            if (Authenticate.IsAuthenticated(HttpContext))
                return View(item);
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        // POST: RatingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rating rating)
        {
            try
            {
                int result = RatingManager.Update(rating);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(rating);
            }
        }

        // GET: RatingController/Delete/5
        public ActionResult Delete(int id)
        {
            var item = RatingManager.LoadById(id);
            ViewBag.Title = "Are You sure you want to delete this?";
            ViewBag.Subject = "Rating: " + item.Description;
            if (Authenticate.IsAuthenticated(HttpContext))
                return View(item);
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        // POST: RatingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int result = RatingManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var item = RatingManager.LoadById(id);
                return View(item);
            }
        }

        #endregion


        #region "Web-API"
        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7143/api/");
            return client;
        }

        public IActionResult Get()
        {
            ViewBag.Title = "Ratings";
            HttpClient client = InitializeClient();

            // Hit the API
            HttpResponseMessage response = client.GetAsync("Rating").Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            List<Rating> itemsList = items.ToObject<List<Rating>>();

            return View(nameof(Index), itemsList);
        }

        public IActionResult GetOne(int id)
        {
            ViewBag.Title = "Rating";
            HttpClient client = InitializeClient();

            // Hit the API
            HttpResponseMessage response = client.GetAsync("Rating/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            Rating rating = item.ToObject<Rating>();

            ViewBag.Subject = rating.Description;

            return View(nameof(Details), rating);
        }

        public IActionResult Insert()
        {
            ViewBag.Title = "Create";
            ViewBag.Subject = "Rating";
            if (Authenticate.IsAuthenticated(HttpContext))
                return View(nameof(Create));
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            

            
        }

        [HttpPost]
        public IActionResult Insert(Rating rating)
        {
            try
            {
                HttpClient client = InitializeClient();

                //Convert our rating into Json to be sent to the api
                string serializedObject = JsonConvert.SerializeObject(rating);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


                // Hit the API and send the content it needs to do the post
                HttpResponseMessage response = client.PostAsync("Rating", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Get));
                    
                }
                throw new Exception(response.StatusCode.ToString());

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nameof(Create));
            }
        }

        public IActionResult Update(int id)
        {
            ViewBag.Title = "Edit Rating";

            HttpClient client = InitializeClient();

            // Hit the API
            HttpResponseMessage response = client.GetAsync("Rating/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            Rating rating = item.ToObject<Rating>();

            ViewBag.Subject = rating.Description;

            if (Authenticate.IsAuthenticated(HttpContext))
                return View(nameof(Edit), rating);
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

            
        }

        [HttpPost]
        public IActionResult Update(int id, Rating rating)
        {
            try
            {
                HttpClient client = InitializeClient();

                //Convert our rating into Json to be sent to the api
                string serializedObject = JsonConvert.SerializeObject(rating);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Hit the API and send the content it needs to do the PUT for edit/update
                HttpResponseMessage response = client.PutAsync("Rating/" + id, content).Result;

                
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Get));

                }
                throw new Exception(response.StatusCode.ToString());
                
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nameof(Edit));
            }
        }

        public IActionResult Remove(int id)
        {
            ViewBag.Title = "Delete Rating";

            HttpClient client = InitializeClient();

            // Hit the API
            HttpResponseMessage response = client.GetAsync("Rating/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            Rating rating = item.ToObject<Rating>();

            ViewBag.Subject = rating.Description;

            if (Authenticate.IsAuthenticated(HttpContext))
                return View(nameof(Delete), rating);
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            
        }

        [HttpPost]
        public IActionResult Remove(int id, Rating rating)
        {
            try
            {
                HttpClient client = InitializeClient();
                // Hit the API
                HttpResponseMessage response = client.DeleteAsync("Rating/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Get));

                }
                throw new Exception(response.StatusCode.ToString());
                

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nameof(Delete));
            }
        }




        #endregion

    }
}
