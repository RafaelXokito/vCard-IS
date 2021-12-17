using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using vCardGateway.Models;

namespace vCardGateway.Controllers
{
    public class CategoriesController : ApiController
    {
        private string entitiesPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Entities.xml";

        /// <summary>
        /// Only For IS Entities
        /// Search for a category based on given ID based on User authenticated
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <returns>Category founded</returns>
        /// <response code="200">Returns the Category founded</response>
        /// <response code="401">Category does not belongs to authenticated user</response>
        /// <response code="404">If the Category was not founded</response>
        [Route("api/entities/{entity_id}/categories")]
        public IHttpActionResult GetCategories(string entity_id)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("categories", Method.GET);

                request.AddHeader("Authorization", Request.Headers.Authorization.ToString());
                IRestResponse<List<Category>> response = client.Execute<List<Category>>(request);
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                if (dataDefaultCategory != null)
                {
                    return Content(response.StatusCode, dataDefaultCategory);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for all categories based on User authenticated
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="category_id">Category ID</param>
        /// <returns>A list of all categories</returns>
        /// <response code="200">Returns the Categories founded. Returns null if you are not authorized</response>
        [Route("api/entities/{entity_id}/categories/{category_id}")]
        public IHttpActionResult GetCategories(string entity_id, int category_id)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"categories/{category_id}", Method.GET);

                request.AddHeader("Authorization", Request.Headers.Authorization.ToString());
                IRestResponse<Category> response = client.Execute<Category>(request);
                //if (response.IsSuccessful && response.Data != null)
                //{
                //    return Content(response.StatusCode, response.Data);
                //}
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                if (dataDefaultCategory != null)
                {
                    return Content(response.StatusCode, dataDefaultCategory);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
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
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"categories", Method.POST, DataFormat.Json);
                request.AddJsonBody(category);
                request.AddHeader("Authorization", Request.Headers.Authorization.ToString());
                IRestResponse<Category> response = client.Execute<Category>(request);
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                return Content(response.StatusCode, dataDefaultCategory);
            }
            catch (Exception ex)
            {
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
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"categories/{category_id}", Method.PUT, DataFormat.Json);
                request.AddJsonBody(category);
                request.AddHeader("Authorization", Request.Headers.Authorization.ToString());
                IRestResponse<Category> response = client.Execute<Category>(request);
                //if (response.IsSuccessful && response.Data != null)
                //{
                //    return Content(response.StatusCode, response.Data);
                //}
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                return Content(response.StatusCode, dataDefaultCategory);
            }
            catch (Exception ex)
            {
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
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"categories/{category_id}", Method.DELETE);

                request.AddHeader("Authorization", Request.Headers.Authorization.ToString());

                IRestResponse response = client.Execute(request);
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                return Content(response.StatusCode, dataDefaultCategory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
