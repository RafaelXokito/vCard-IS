using MBWayAPI;
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

        [Route("api/entities/{id}")]
        public IHttpActionResult PutEntity(string id, Entity entity)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                handlerXML.UpdateEntity(id, entity);
                return Ok();
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
