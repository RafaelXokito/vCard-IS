using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vCardGateway.Models;

namespace vCardGateway.Controllers
{
    [BasicAuthentication]
    public class EndpointsSufixsController : ApiController
    {
        private string endpointssufixsPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\EndpointsSufixs.xml";

        [Route("api/endpointssufixs")]
        public IEnumerable<EndpointSufix> Getendpointssufixs()
        {
            HandlerXML handlerXML = new HandlerXML(endpointssufixsPath);

            return handlerXML.GetEndpointSufixs();
        }

        [Route("api/endpointssufixs/{content}")]
        public IHttpActionResult GetEndpointSufix(string content)
        {
            HandlerXML handlerXML = new HandlerXML(endpointssufixsPath);

            EndpointSufix entity = handlerXML.GetEndpointSufix(content);

            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [Route("api/endpointssufixs")]
        public IHttpActionResult PostEndpointSufix(EndpointSufix sufix)
        {
            HandlerXML handlerXML = new HandlerXML(endpointssufixsPath);

            try
            {
                handlerXML.CreateEndpointSufix(sufix);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/endpointssufixs/{content}")]
        public IHttpActionResult PutEndpointSufix(string content, EndpointSufix entity)
        {
            HandlerXML handlerXML = new HandlerXML(endpointssufixsPath);

            try
            {
                handlerXML.UpdateEndpointSufix(content, entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/endpointssufixs/{content}")]
        public IHttpActionResult DeleteEndpointSufix(string content)
        {
            HandlerXML handlerXML = new HandlerXML(endpointssufixsPath);

            try
            {
                handlerXML.DeleteEndpointSufix(content);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
