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



        [Route("api/entities/{entity_id}/defaultcategories")]
        public IHttpActionResult PostDefaultCategories(string entity_id, DefaultCategory defaultCategory)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("defaultcategories", Method.POST, DataFormat.Json);

                request.AddJsonBody(new { name = defaultCategory.Name, type = defaultCategory.Type });
                request.AddHeader("Authorization", entity.Authentication.Token);
                IRestResponse<DefaultCategory> response = client.Execute<DefaultCategory>(request);
                if (response.IsSuccessful)
                {
                    return Ok(response.Data);
                }
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                return Content(response.StatusCode, dataDefaultCategory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //TODO PUT

        [Route("api/entities/{entity_id}/defaultcategories")]
        public IHttpActionResult DeleteDefaultCategories(string entity_id, DefaultCategory defaultCategory)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest requestGet = new RestRequest("defaultcategories", Method.GET);
                requestGet.AddHeader("Authorization", entity.Authentication.Token);
                requestGet.AddParameter("name", defaultCategory.Name);
                requestGet.AddParameter("type", defaultCategory.Type);

                IRestResponse<List<DefaultCategory>> responseGet = client.Execute<List<DefaultCategory>>(requestGet);
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(responseGet.Content);
                List<DefaultCategory> auxList = new List<DefaultCategory>();
                if (responseGet.Data.Count == 0 && dataDefaultCategory.data != null)
                    dataDefaultCategory = dataDefaultCategory.data;
                else if (responseGet.Data[0].Id == 0)
                    dataDefaultCategory = dataDefaultCategory.data;
                else
                    auxList = responseGet.Data;

                if (responseGet.Data.Count == 0 || responseGet.Data[0].Id == 0)
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
                        RestRequest requestDelete = new RestRequest("defaultcategories/" + auxList[0].Id, Method.DELETE);
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
                            dynamic dataresponseForceDelete = JsonConvert.DeserializeObject(responseForceDelete.Content);
                            return Content(responseForceDelete.StatusCode, responseForceDelete);
                        }
                        dynamic dataresponseDelete = JsonConvert.DeserializeObject(responseGet.Content);
                        return Content(responseGet.StatusCode, dataresponseDelete);
                    }
                }
                dynamic dataresponseGet = JsonConvert.DeserializeObject(responseGet.Content);
                return Content(responseGet.StatusCode, dataresponseGet);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/entities/{entity_id}/defaultcategories")]
        public IHttpActionResult GetDefaultCategories(string entity_id)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("defaultcategories", Method.GET);

                request.AddHeader("Authorization", Request.Headers.Authorization.ToString());
                IRestResponse response = client.Execute(request);
                dynamic dataDefaultCategory = JsonConvert.DeserializeObject(response.Content);
                if (dataDefaultCategory != null)
                {
                    return Content(response.StatusCode, dataDefaultCategory);
                }
                return Content(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/entities/{entity_id}/defaultcategories/{category_id}")]
        public IHttpActionResult GetDefaultCategoriy(string entity_id, int category_id)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"defaultcategories/{category_id}", Method.GET);

                request.AddHeader("Authorization", entity.Authentication.Token);
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
