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
    public class EndpointsController : ApiController
    {
        private string endpointPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\endpoints.xml";

        [Route("api/endpoints")]
        public IEnumerable<Endpoint> GetEndpoints()
        {
            HandlerXML handlerXML = new HandlerXML(endpointPath);

            return handlerXML.GetEndpoints();
        }

        [Route("api/endpoints/{name}")]
        public IHttpActionResult GetEndpoint(string name)
        {
            HandlerXML handlerXML = new HandlerXML(endpointPath);

            Endpoint endpoint = handlerXML.GetEndpointByName(name);

            if (endpoint == null)
            {
                return NotFound();
            }
            return Ok(endpoint);
        }

        [Route("api/endpoints")]
        public IHttpActionResult PostEndpoint(Endpoint endpoint)
        {
            HandlerXML handlerXML = new HandlerXML(endpointPath);

            try
            {
                handlerXML.CreateEndpoint(endpoint);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/endpoints/{name}")]
        public IHttpActionResult PutEndpoint(string name, Endpoint endpoint)
        {
            HandlerXML handlerXML = new HandlerXML(endpointPath);

            try
            {
                handlerXML.UpdateEndpoint(name, endpoint);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/endpoints/{name}")]
        public IHttpActionResult DeleteEndpoint(string name)
        {
            HandlerXML handlerXML = new HandlerXML(endpointPath);

            try
            {
                handlerXML.DeleteEndpointByName(name);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
