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

        [Route("api/entities")]
        public IEnumerable<Entity> GetEntities()
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            return handlerXML.GetEntities();
        }

        [Route("api/entities/{id}")]
        public IHttpActionResult GetEntity(string id)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            Entity entity = handlerXML.GetEntity(id);

            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [Route("api/entities")]
        public IHttpActionResult PostEntity(Entity entity)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                handlerXML.CreateEntity(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/entities/{id}/defaultcategories")]
        public IHttpActionResult PostEntityDefaultCategories(string id, DefaultCategory defaultCategory)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("defaultcategories", Method.POST, DataFormat.Json);

                request.AddJsonBody(defaultCategory);

                IRestResponse<DefaultCategory> response = client.Execute<DefaultCategory>(request);
                if (response.IsSuccessful)
                {
                    return Ok(response.Data);
                }
                return BadRequest(response.Content);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/entities/{id}/defaultcategories")]
        public IHttpActionResult DeleteEntityDefaultCategories(string id, DefaultCategory defaultCategory)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest requestGet = new RestRequest("defaultcategories", Method.GET);
                requestGet.AddParameter("name", defaultCategory.Name);
                requestGet.AddParameter("type", defaultCategory.Type);

                IRestResponse<List<DefaultCategory>> responseGet = client.Execute<List<DefaultCategory>>(requestGet);
                if (responseGet.IsSuccessful)
                {
                    if (responseGet.Data.Count > 0)
                    {
                        RestRequest requestDelete = new RestRequest("defaultcategories/"+ responseGet.Data[0].Id, Method.DELETE);

                        IRestResponse responseDelete = client.Execute(requestDelete);
                        if (responseDelete.IsSuccessful)
                        {
                            return Ok();
                        }
                        return BadRequest(responseGet.Content);
                    }
                }
                return BadRequest(responseGet.Content);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/entities/{id}")]
        public IHttpActionResult PutEntity(string id, Entity entity)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                handlerXML.UpdateEntity(id, entity);
                return Ok(handlerXML.GetEntity(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/entities/{id}")]
        public IHttpActionResult DeleteEntity(string id)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                handlerXML.DeleteEntity(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
