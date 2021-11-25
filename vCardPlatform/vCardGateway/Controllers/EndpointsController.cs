using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace vCardGateway.Controllers
{
    public class EndpointsController : ApiController
    {
        [Route("api/endpoints")]
        public IEnumerable<EndPoint> GetEndpoints()
        {
            List<EndPoint> endpoints = new List<EndPoint>();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\endpoints.xml");

            XmlNodeList nodeList = doc.SelectNodes("//endpoint");

            return endpoints;
        }

        [Route("api/endpoint")]
        public IHttpActionResult GetEndpoint()
        {
            return Ok();
        }
    }
}
