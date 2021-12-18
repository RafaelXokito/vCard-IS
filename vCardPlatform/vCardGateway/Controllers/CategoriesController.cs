using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vCardGateway.Models;

namespace vCardGateway.Controllers
{
    public class CategoriesController : ApiController
    {
        private string entitiesPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Entities.xml";
        /// <summary>
        /// Search for category
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <returns>A list of all categories</returns>
        /// <response code="200">Returns the Categories found. Returns null if you are not authorized</response>
        [Route("api/entities/{entity_id}/categories")]
        public IHttpActionResult GetCategories(string entity_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("categories", Method.GET);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse<List<Category>> response = client.Execute<List<Category>>(request);
                GeneralLogsController.PostGeneralLog("Categories", "N/A", entity.Name, response.StatusCode.ToString(), "GetCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "categories");
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                if (dataDefaultCategory != null)
                {
                    return Content(response.StatusCode, dataDefaultCategory);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Categories", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "GetCategories", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "categories");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Only For IS Entities
        /// Search for category based on given ID and User authenticated
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="category_id">Category ID</param>
        /// <returns>Category found</returns>
        /// <response code="200">Returns the Category found</response>
        /// <response code="401">Category does not belongs to authenticated user</response>
        /// <response code="404">If the Category was not found</response>
        [Route("api/entities/{entity_id}/categories/{category_id}")]
        public IHttpActionResult GetCategory(string entity_id, int category_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"categories/{category_id}", Method.GET);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse<Category> response = client.Execute<Category>(request);
                //if (response.IsSuccessful && response.Data != null)
                //{
                //    return Content(response.StatusCode, response.Data);
                //}
                GeneralLogsController.PostGeneralLog("Categories", "N/A", entity.Name, response.StatusCode.ToString(), "GetCategory", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "categories");
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                if (dataDefaultCategory != null)
                {
                    return Content(response.StatusCode, dataDefaultCategory);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Categories", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "GetCategory", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "categories");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Insert Category for authenticated User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Name": "Food",
        ///        "Type": "D"
        ///     }
        ///     
        ///     Type IN ("D", "C")
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="category">Category to insert</param>
        /// <returns>Category inserted</returns>
        /// <response code="200">Returns the newly created Category</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/categories")]
        public IHttpActionResult PostCategories(string entity_id, Category category)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                if (category == null || category.Name == null || category.Type == null || (category.Type != "D" && category.Type != "C"))
                {
                    GeneralLogsController.PostGeneralLog("Categories", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PostCategories", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"categories", Method.POST, DataFormat.Json);
                request.AddJsonBody(category);
                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse<Category> response = client.Execute<Category>(request);
                GeneralLogsController.PostGeneralLog("Categories", "N/A", entity.Name, response.StatusCode.ToString(), "PostCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "categories");
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                return Content(response.StatusCode, dataDefaultCategory);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Categories", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "PostCategories", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "categories");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update Category of authenticated User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///        "Name": "Food",
        ///        "Type": "D"
        ///     }
        ///     
        ///     Type IN ("D", "C")
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="category_id">Category ID</param>
        /// <param name="category">Category to be updated</param>
        /// <returns>Category Updated</returns>
        /// <response code="200">Returns the updated created Category</response>
        /// <response code="401">Category does not belongs to authenticated user</response>
        /// <response code="404">If given Category not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/categories/{category_id}")]
        public IHttpActionResult PutCategories(string entity_id, int category_id, [FromBody]Category category)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                if (category == null || category.Name == null || category.Type == null || (category.Type != "D" && category.Type != "C"))
                {
                    GeneralLogsController.PostGeneralLog("Categories", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PutCategories", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"categories/{category_id}", Method.PUT, DataFormat.Json);
                request.AddJsonBody(category);
                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse<Category> response = client.Execute<Category>(request);
                //if (response.IsSuccessful && response.Data != null)
                //{
                //    return Content(response.StatusCode, response.Data);
                //}
                GeneralLogsController.PostGeneralLog("Categories", "N/A", entity.Name, response.StatusCode.ToString(), "PutCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "categories");
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                return Content(response.StatusCode, dataDefaultCategory);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Categories", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "PutCategories", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "categories");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete a category based on given ID and on User authenticated
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="category_id">Category ID</param>
        /// <returns>HTTPResponse</returns>
        /// <response code="200">Returns subjective message</response>
        /// <response code="401">Category does not belongs to authenticated user</response>
        /// <response code="404">If given Category not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/categories/{category_id}")]
        public IHttpActionResult DeleteCategories(string entity_id, int category_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"categories/{category_id}", Method.DELETE);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);

                IRestResponse response = client.Execute(request);
                GeneralLogsController.PostGeneralLog("Categories", "N/A", entity.Name, response.StatusCode.ToString(), "DeleteCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "categories");
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                return Content(response.StatusCode, dataDefaultCategory);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Categories", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "DeleteCategories", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "categories");
                return InternalServerError(ex);
            }
        }
    }
}
