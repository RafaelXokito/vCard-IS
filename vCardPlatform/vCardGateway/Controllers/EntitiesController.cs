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
    public class EntitiesController : ApiController
    {
        private string entitiesPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Entities.xml";

        [Route("api/entities")]
        public IEnumerable<Entity> GetEntities()
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            return handlerXML.GetEntities();
        }

        [Route("api/entities/{name}")]
        public IHttpActionResult GetEntity(string name)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            Entity entity = handlerXML.GetEntityByName(name);

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

        [Route("api/entities/{name}")]
        public IHttpActionResult PutEntity(string name, Entity entity)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                handlerXML.UpdateEntity(name, entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/entities/{name}")]
        public IHttpActionResult DeleteEntity(string name)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                handlerXML.DeleteEntityByName(name);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
