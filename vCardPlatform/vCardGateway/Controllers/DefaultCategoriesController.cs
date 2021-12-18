using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vCardGateway.Models;

namespace vCardGateway.Controllers
{
    public class DefaultCategoriesController : ApiController
    {
        private string entitiesPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Entities.xml";
        private string entitiesPathXSD = System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Entities.xsd";

        /// <summary>
        /// Search for all Default Categoies based on User authenticated
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <returns>A list of all Default Categoies</returns>
        /// <response code="200">Returns the Default Categoies found. Returns null if you are not authorized</response>
        [Route("api/entities/{entity_id}/defaultcategories")]
        public IHttpActionResult GetDefaultCategories(string entity_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath, entitiesPathXSD);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("defaultcategories", Method.GET);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse response = client.Execute(request);
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                if (dataDefaultCategory != null)
                {
                    GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", entity.Name, response.StatusCode.ToString(), "GetDefaultCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                    return Content(response.StatusCode, dataDefaultCategory);
                }
                GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", entity.Name, HttpStatusCode.Unauthorized.ToString(), "GetDefaultCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                return Content(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString());
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "GetDefaultCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for a category based on given ID and User authenticated
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="category_id">Default Categoy ID</param>
        /// <returns>Default Categoy found</returns>
        /// <response code="200">Returns the Default Categoy found</response>
        /// <response code="401">Default Categoy does not belongs to authenticated user</response>
        /// <response code="404">If the Default Categoy was not found</response>
        [Route("api/entities/{entity_id}/defaultcategories/{category_id}")]
        public IHttpActionResult GetDefaultCategoriy(string entity_id, int category_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath, entitiesPathXSD);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"defaultcategories/{category_id}", Method.GET);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse response = client.Execute(request);
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", entity.Name, response.StatusCode.ToString(), "GetDefaultCategoriy", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                return Content(response.StatusCode, dataDefaultCategory);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "GetDefaultCategoriy", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Insert Default Categoy for authenticated User
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
        /// <param name="defaultCategory">Default Categoy to insert</param>
        /// <returns>Default Categoy inserted</returns>
        /// <response code="200">Returns the newly created Default Categoy</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/defaultcategories")]
        public IHttpActionResult PostDefaultCategories(string entity_id, DefaultCategory defaultCategory)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath, entitiesPathXSD);

            try
            {
                if (defaultCategory == null || defaultCategory.Name == null || defaultCategory.Type == null || (defaultCategory.Type != "D" && defaultCategory.Type != "C"))
                {
                    GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PostDefaultCategories", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("defaultcategories", Method.POST, DataFormat.Json);

                request.AddJsonBody(new { name = defaultCategory.Name, type = defaultCategory.Type });
                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse<DefaultCategory> response = client.Execute<DefaultCategory>(request);
                if (response.IsSuccessful)
                {
                    return Ok(response.Data);
                }
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", entity.Name, response.StatusCode.ToString(), "PostDefaultCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                return Content(response.StatusCode, dataDefaultCategory);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "PostDefaultCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update Default Categoy of authenticated User
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
        /// <param name="category_id">Default Categoy ID</param>
        /// <param name="defaultCategory">Default Categoy to be updated</param>
        /// <returns>Default Categoy Updated</returns>
        /// <response code="200">Returns the updated updated Default Categoy</response>
        /// <response code="401">Default Categoy does not belongs to authenticated user</response>
        /// <response code="404">If given Default Categoy not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/defaultcategories/{category_id}")]
        public IHttpActionResult PutDefaultCategories(string entity_id, int category_id, DefaultCategory defaultCategory)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath, entitiesPathXSD);

            try
            {
                if (defaultCategory == null || defaultCategory.Name == null || defaultCategory.Type == null || (defaultCategory.Type != "D" && defaultCategory.Type != "C"))
                {
                    GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PutDefaultCategories", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"defaultcategories/{category_id}", Method.PUT, DataFormat.Json);

                request.AddJsonBody(new { name = defaultCategory.Name, type = defaultCategory.Type });
                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse<DefaultCategory> response = client.Execute<DefaultCategory>(request);
                if (response.IsSuccessful)
                {
                    return Ok(response.Data);
                }
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", entity.Name, response.StatusCode.ToString(), "PutDefaultCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                return Content(response.StatusCode, dataDefaultCategory);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "PutDefaultCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete a category based on given ID and on User authenticated
        /// </summary>Entity
        /// <param name="entity_id">Entity ID</param>
        /// <param name="category_id">Default Categoy ID</param>
        /// <returns>HTTPResponse</returns>
        /// <response code="200">Returns subjective message</response>
        /// <response code="401">Default Categoy does not belongs to authenticated user</response>
        /// <response code="404">If given Default Categoy not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/defaultcategories/{category_id}")]
        public IHttpActionResult DeleteDefaultCategories(string entity_id, int category_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath, entitiesPathXSD);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest requestDelete = new RestRequest("defaultcategories/" + category_id, Method.DELETE);
                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                requestDelete.AddHeader("Authorization", auth);
                IRestResponse responseDelete = client.Execute(requestDelete);
                if (responseDelete.IsSuccessful)
                {
                    //VCARDDAD need to be force deleted
                    if (entity.Endpoint == "http://172.22.21.95")
                    {
                        RestRequest requestForceDelete = new RestRequest("defaultcategories/" + category_id, Method.POST);
                        requestForceDelete.AddHeader("Authorization", entity.Authentication.Token);
                        IRestResponse responseForceDelete = client.Execute(requestForceDelete);
                        if (responseForceDelete.IsSuccessful)
                        {
                            GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", entity.Name, responseForceDelete.StatusCode.ToString(), "DeleteDefaultCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                            return Ok("Default Category Deleted");
                        }
                        dynamic dataresponseForceDelete = JsonConvert.DeserializeObject(responseForceDelete.Content);
                        GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", entity.Name, responseForceDelete.StatusCode.ToString(), "DeleteDefaultCategories", dataresponseForceDelete, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                        return Content(responseForceDelete.StatusCode, responseForceDelete);
                    }
                    GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", entity.Name, responseDelete.StatusCode.ToString(), "DeleteDefaultCategories", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                    return Ok("Default Category Deleted");
                }
                dynamic dataresponseDelete = JsonConvert.DeserializeObject(responseDelete.Content);
                GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", entity.Name, responseDelete.StatusCode.ToString(), "DeleteDefaultCategories", dataresponseDelete, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                return Content(responseDelete.StatusCode, dataresponseDelete);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("DefaultCategories", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "DeleteDefaultCategories", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "defaultcategories");
                return InternalServerError(ex);
            }
        }

    }
}
