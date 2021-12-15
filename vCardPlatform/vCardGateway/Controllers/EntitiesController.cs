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

        [Route("api/entities")]
        public IEnumerable<Entity> GetEntities([FromUri] string name = null)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            return handlerXML.GetEntities(name);
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

        [Route("api/entities/{id}/users/{username}/photo")]
        public IHttpActionResult PostEntityUserPhoto(string id, string username,User user)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"users/{username}/photo", Method.POST, DataFormat.Json);

                request.AddHeader("Authorization", entity.Authentication.Token);
                request.AddBody(user);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return Ok(response.Content);
                }
                return BadRequest(response.Content);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/entities/{id}/users")]
        public IHttpActionResult PostEntityUsers(string id, User user)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("users", Method.POST, DataFormat.Json);

                request.AddHeader("Authorization", entity.Authentication.Token);
                request.AddJsonBody(user);
                IRestResponse<User> response = client.Execute<User>(request);
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

        [Route("api/entities/{id}/users")]
        public IHttpActionResult GetEntityUsers(string id)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("users", Method.GET);

                request.AddHeader("Authorization", entity.Authentication.Token);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return Ok(response.Content);
                }
                return BadRequest(response.Content);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/entities/{id}/defaultcategories")]
        public IHttpActionResult GetEntityDefaultCategories(string id)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("defaultcategories", Method.GET);

                request.AddHeader("Authorization", entity.Authentication.Token);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    return Ok(response.Content);
                }
                return BadRequest(response.Content);
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

                request.AddJsonBody(new { name= defaultCategory.Name, type = defaultCategory.Type });
                request.AddHeader("Authorization", entity.Authentication.Token);
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
                requestGet.AddHeader("Authorization", entity.Authentication.Token);
                requestGet.AddParameter("name", defaultCategory.Name);
                requestGet.AddParameter("type", defaultCategory.Type);
                
                IRestResponse<List<DefaultCategory>> responseGet = client.Execute<List<DefaultCategory>>(requestGet);
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(responseGet.Content); ;
                List<DefaultCategory> auxList = new List<DefaultCategory>();
                if (dataDefaultCategory.data != null)
                    dataDefaultCategory = dataDefaultCategory.data;
                foreach (var item in dataDefaultCategory)
                {
                    auxList.Add(new DefaultCategory
                    {
                        Id = item.id,
                        Name = item.name,
                        Type = item.type,
                    });
                }
                if (responseGet.IsSuccessful)
                {
                    if (auxList.Count > 0)
                    {
                        RestRequest requestDelete = new RestRequest("defaultcategories/"+ auxList[0].Id, Method.DELETE);
                        requestDelete.AddHeader("Authorization", entity.Authentication.Token);
                        IRestResponse responseDelete = client.Execute(requestDelete);
                        if (responseDelete.IsSuccessful)
                        {
                            RestRequest requestForceDelete = new RestRequest("defaultcategories/" + auxList[0].Id, Method.POST);
                            requestForceDelete.AddHeader("Authorization", entity.Authentication.Token);
                            IRestResponse responseForceDelete = client.Execute(requestForceDelete);
                            if (responseDelete.IsSuccessful)
                            {
                                return Ok();
                            }
                            return BadRequest(responseGet.Content);
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
