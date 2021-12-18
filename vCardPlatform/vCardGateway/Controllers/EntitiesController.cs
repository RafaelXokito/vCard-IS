using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Xml;
using vCardGateway.Models;

namespace vCardGateway.Controllers
{
    [BasicAuthentication]
    public class EntitiesController : ApiController
    {
        private string entitiesPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Entities.xml";

        /// <summary>
        /// Search for a entity based on given ID and User authenticated
        /// </summary>
        /// <param name="name">Filter by name.</param>
        /// <returns>Entity found</returns>
        /// <response code="200">Returns the Entity found</response>
        /// <response code="401">Entity does not belongs to authenticated user</response>
        /// <response code="404">If the Entity was not found</response>
        [Route("api/entities")]
        public IEnumerable<Entity> GetEntities([FromUri] string name = null)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.OK.ToString(), "GetEntities", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
            return handlerXML.GetEntities(name);
        }

        /// <summary>
        /// Search for entity
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <returns>A list of all entities</returns>
        /// <response code="200">Returns the Entities found. Returns null if you are not authorized</response>
        [Route("api/entities/{entity_id}")]
        public IHttpActionResult GetEntity(string entity_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            Entity entity = handlerXML.GetEntity(entity_id);

            if (entity == null)
            {
                GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.NotFound.ToString(), "GetEntity", $"Endpoint {entity_id} was not found", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                return Content(HttpStatusCode.NotFound, $"Endpoint {entity_id} was not found");
            }
            GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.OK.ToString(), "GetEntity", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
            return Ok(entity);
        }

        /// <summary>
        /// Insert Entity for authenticated User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///         "Id": "2",
        ///         "Name": "MBWAY",
        ///         "Endpoint": "http://localhost:55059",
        ///         "MaxLimit": 5000.02,
        ///         "EarningPercentage": 2.0,
        ///         "Authentication": {
        ///             "Token": "Basic R0FURVdBWToxMjM0",
        ///             "Username": "GATEWAY",
        ///             "Password": "1234"
        ///     }
        ///     
        /// </remarks>
        /// <param name="entity">Entity to insert</param>
        /// <returns>Entity inserted</returns>
        /// <response code="200">Returns the newly created Entity</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities")]
        public IHttpActionResult PostEntity(Entity entity)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            if (entity.Endpoint != null)
            {
                RestClient client = new RestClient(entity.Endpoint + "/api");
                RestRequest request = new RestRequest("categories", Method.GET);

                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.InternalServerError || response.StatusCode == HttpStatusCode.NotFound)
                {
                    GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PostEntity", "Endpoint need to be reachble", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                    return Content(HttpStatusCode.BadRequest, "Endpoint need to be reachble");
                }
            }
            else
            {
                GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PostEntity", "Endpoint cant be null", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                return Content(HttpStatusCode.BadRequest, "Endpoint cant be null");
            }

            try
            {
                GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.Created.ToString(), "PostEntity", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                return Content(HttpStatusCode.Created, handlerXML.CreateEntity(entity));
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.InternalServerError.ToString(), "PostEntity", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///         "Id": "2",
        ///         "Name": "MBWAY",
        ///         "Endpoint": "http://localhost:55059",
        ///         "MaxLimit": 5000.02,
        ///         "EarningPercentage": 2.0,
        ///         "Authentication": {
        ///             "Token": "Basic R0FURVdBWToxMjM0",
        ///             "Username": "GATEWAY",
        ///             "Password": "1234"
        ///     }
        ///     
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>Entity Updated</returns>
        /// <response code="201">Returns the updated created Entity</response>
        /// <response code="401">Entity does not belongs to authenticated user</response>
        /// <response code="404">If given Entity not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}")]
        public IHttpActionResult PutEntity(string entity_id, Entity entity)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            if (entity.Endpoint != null)
            {
                RestClient client = new RestClient(entity.Endpoint + "/api");
                RestRequest request = new RestRequest("", Method.GET);

                IRestResponse response = client.Execute(request);
                if (!response.IsSuccessful && response.StatusCode != HttpStatusCode.NotFound)
                {
                    GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PutEntity", "Endpoint need to be reachble", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                    return Content(HttpStatusCode.BadRequest, "Endpoint need to be reachble");
                }
            }
            else
            {
                GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PutEntity", "Endpoint cant be null", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                return Content(HttpStatusCode.BadRequest, "Endpoint cant be null");
            }

            try
            {
                handlerXML.UpdateEntity(entity_id, entity);
                GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.Created.ToString(), "PutEntity", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                return Content(HttpStatusCode.Created,handlerXML.GetEntity(entity_id));
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.InternalServerError.ToString(), "PutEntity", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update Authentication of a given Entity
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///         "Authentication": {
        ///             "Token": "Basic R0FURVdBWToxMjM0",
        ///             "Username": "GATEWAY",
        ///             "Password": "1234"
        ///     }
        ///     
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="authentication">Authentication to be updated</param>
        /// <returns>Entity Updated</returns>
        /// <response code="200">Returns the updated created Entity</response>
        /// <response code="401">Entity does not belongs to authenticated user</response>
        /// <response code="404">If given Entity not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/auth")]
        public IHttpActionResult PutEntityAuth(string entity_id,[FromBody] Authentication authentication)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                handlerXML.UpdateEntityAuth(entity_id, authentication);
                GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.OK.ToString(), "PutEntityAuth", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                return Ok(handlerXML.GetEntity(entity_id));
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.InternalServerError.ToString(), "PutEntityAuth", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete a entity based on given ID
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <returns>HTTPResponse</returns>
        /// <response code="200">Returns subjective message</response>
        /// <response code="401">Entity does not belongs to authenticated user</response>
        /// <response code="404">If given Entity not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}")]
        public IHttpActionResult DeleteEntity(string entity_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                handlerXML.DeleteEntity(entity_id);
                GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.OK.ToString(), "DeleteEntity", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                return Ok($"Entity {entity_id} deleted");
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Entities", email, "Gateway", HttpStatusCode.InternalServerError.ToString(), "DeleteEntity", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "entities");
                return InternalServerError(ex);
            }
        }

    }
}
