using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BDF.Utility
{
    public class ApiClient : HttpClient
    {
        // Hello EVeryone
        public ApiClient(string baseAddress)
        {
            BaseAddress = new Uri(baseAddress);
        }

        /// <summary>
        /// Gets a list of items by performing an http get operation from an API controller
        /// </summary>
        /// <typeparam name="T">Object type for which a list will be returned by the API call</typeparam>
        /// <param name="controller">API controller to call</param>
        /// <returns>List of objects of type T</returns>
        public List<T> GetList<T>(string controller)
        {
            var response = this.GetAsync(controller).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var items = (JArray)JsonConvert.DeserializeObject(result);

            return items.ToObject<List<T>>();
        }

        /// <summary>
        /// Gets a list of items by performing an http get operation from an API controller
        /// </summary>
        /// <typeparam name="T">Object type for which a list will be returned by the API call</typeparam>
        /// <param name="controller">API controller to call</param>
        /// <param name="id">Id to pass in for the API route</param>
        /// <returns>List of objects of type T</returns>
        public List<T> GetList<T>(string controller, Guid id)
        {
            try
            {
                return GetList<T>(controller + "/" + id.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a list of items by performing an http get operation from an API controller
        /// </summary>
        /// <typeparam name="T">Object type for which a list will be returned by the API call</typeparam>
        /// <param name="controller">API controller to call</param>
        /// <param name="route">Additional API route information</param>
        /// <returns>List of objects of type T</returns>
        public List<T> GetList<T>(string controller, string route)
        {
            try
            {
                return GetList<T>(controller + "/" + route);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a list of items by performing an http get operation from an API controller
        /// </summary>
        /// <typeparam name="T">Object type for which a list will be returned by the API call</typeparam>
        /// <param name="controller">API controller to call</param>
        /// <param name="route">Additional API route information</param>
        /// <param name="id">Id to pass in for the API route</param>
        /// <returns>List of objects of type T</returns>
        public List<T> GetList<T>(string controller, string route, Guid id)
        {
            return GetList<T>(controller + "/" + route + "/" + id);
        }

        /// <summary>
        /// Gets a single item by performing an http get operation from an API controller
        /// </summary>
        /// <typeparam name="T">Type of the object returned by the API call</typeparam>
        /// <param name="controller">API controller to call</param>
        /// <returns>Object of type T</returns>
        public T GetItem<T>(string controller)
        {
            try
            {
                var response = this.GetAsync(controller).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var item = JsonConvert.DeserializeObject(result, typeof(T));

                return (T)item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a single item by performing an http get operation from an API controller
        /// </summary>
        /// <typeparam name="T">Type of the object returned by the API call</typeparam>
        /// <param name="controller">API controller to call</param>
        /// <param name="id">Id to pass in for the API route</param>
        /// <returns>Object of type T</returns>
        public T GetItem<T>(string controller, Guid id)
        {
            try
            {
                return GetItem<T>(controller + "/" + id.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a single item by performing an http get operation from an API controller
        /// </summary>
        /// <typeparam name="T">Type of the object returned by the API call</typeparam>
        /// <param name="controller">API controller to call</param>
        /// <param name="route">Additional API route information</param>
        /// <returns>Object of type T</returns>
        public T GetItem<T>(string controller, string route)
        {
            try
            {
                return GetItem<T>(controller + "/" + route);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Performs an http post operation from an API controller
        /// </summary>
        /// <typeparam name="T">Type of the object returned by the API call</typeparam>
        /// <param name="item">Item to be sent to the post call</param>
        /// <param name="controller">API controller to call</param>
        /// <returns>API response message</returns>
        public HttpResponseMessage Post<T>(T item)
        {
            try
            {
                string serializedItem = JsonConvert.SerializeObject(item);
                var content = new StringContent(serializedItem);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                return this.PostAsync("", content).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Performs an http post operation from an API controller
        /// </summary>
        /// <typeparam name="T">Type of the object returned by the API call</typeparam>
        /// <param name="item">Item to be sent to the post call</param>
        /// <param name="controller">API controller to call</param>
        /// <returns>API response message</returns>
        public HttpResponseMessage Post<T>(T item, string controller)
        {
            try
            {
                string serializedItem = JsonConvert.SerializeObject(item);
                var content = new StringContent(serializedItem);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                return this.PostAsync(controller, content).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Performs an http post operation from an API controller
        /// </summary>
        /// <typeparam name="T">Type of the object to be sent to the API call</typeparam>
        /// <param name="item">Item to be sent to the post call</param>
        /// <param name="controller">API controller to call</param>
        /// <param name="route">Route to call within the API controller</param>
        /// <returns>API response message</returns>
        public HttpResponseMessage Post<T>(T item, string controller, string route)
        {
            try
            {
                return Post(item, controller + "/" + route);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Performs an http post operation from an API controller
        /// </summary>
        /// <typeparam name="T">Type of the object to be sent to the API call</typeparam>
        /// <param name="item">Item to be sent to the post call</param>
        /// <param name="controller">API controller to call</param>
        /// <param name="id">Id to pass in for the API route</param>
        /// <returns>API response message</returns>
        public HttpResponseMessage Post<T>(T item, string controller, Guid id)
        {
            try
            {
                string serializedItem = JsonConvert.SerializeObject(item);
                var content = new StringContent(serializedItem);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return this.PostAsync(controller, content).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Performs an http put operation from an API controller
        /// </summary>
        /// <typeparam name="T">Type of the object to be sent to the API call</typeparam>
        /// <param name="item">Item to be sent to the post call</param>
        /// <param name="controller">API controller to call</param>
        /// <param name="id">Id of the item to be sent to the API call</param>
        /// <returns>API response message</returns>
        public HttpResponseMessage Put<T>(T item, string controller, Guid id)
        {
            try
            {
                string serializedItem = JsonConvert.SerializeObject(item);
                var content = new StringContent(serializedItem);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                return this.PutAsync(controller + "/" + id.ToString(), content).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Performs an http delete operation from an API controller
        /// </summary>
        /// <param name="controller">API controller to call</param>
        /// <param name="id">Id of the item to be sent to the API call</param>
        /// <returns>API response message</returns>
        public HttpResponseMessage Delete(string controller, Guid id)
        {
            return this.DeleteAsync(controller + "/" + id.ToString()).Result;
        }

        /// <summary>
        /// Performs an http delete operation from an API controller
        /// </summary>
        /// <param name="controller">API controller to call</param>
        /// <param name="id1">Id of the first item to be sent to the API call</param>
        /// <param name="id2">Id of the second item to be sent to the API call</param>
        /// <returns>API response message</returns>
        public HttpResponseMessage Delete(string controller, Guid id1, Guid id2)
        {
            return this.DeleteAsync(controller + "/" + id1.ToString() + "/" + id2.ToString()).Result;
        }

        public HttpResponseMessage Delete(string controller, string route, Guid id)
        {
            return this.DeleteAsync(controller + "/" + route + "/" + id.ToString()).Result;
        }
    }
}
