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


        /// <summary>
        /// Search for sufix
        /// </summary>
        /// <returns>A list of all sufixs</returns>
        /// <response code="200">Returns the Sufixs found. Returns null if you are not authorized</response>
        [Route("api/endpointssufixs")]
        public IEnumerable<EndpointSufix> GetEndpointssufixs()
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            HandlerXML handlerXML = new HandlerXML(endpointssufixsPath);
            GeneralLogsController.PostGeneralLog("EndpointSufixs", email, "Gateway", HttpStatusCode.OK.ToString(), "Getendpointssufixs", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "endpointssufixs");
            return handlerXML.GetEndpointSufixs();
        }

        /// <summary>
        /// Search for a sufix based on given content
        /// </summary>
        /// <remarks>
        /// Based on unique key of endpoint sufix contains '/' we need to consume body as primary key
        /// 
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "Content": "/api/xpto"
        ///     }
        ///     
        /// </remarks>
        /// <param name="sufix">Struct with Sufix Content</param>
        /// <returns>Sufix found</returns>
        /// <response code="200">Returns the Sufix found</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">If the Sufix was not found</response>
        [Route("api/endpointssufixs/sufix")]
        public IHttpActionResult GetEndpointSufix(EndpointSufix sufix)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            HandlerXML handlerXML = new HandlerXML(endpointssufixsPath);
            if (sufix == null || sufix.Content == null || sufix.Content == "")
            {
                GeneralLogsController.PostGeneralLog("EndpointSufixs", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "GetEndpointSufix", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                return Content(HttpStatusCode.BadRequest, "Invalid inputs");
            }
            EndpointSufix sufix_r = handlerXML.GetEndpointSufix(sufix.Content);

            if (sufix_r == null)
            {
                GeneralLogsController.PostGeneralLog("EndpointSufixs", email, "Gateway", HttpStatusCode.NotFound.ToString(), "GetEndpointSufix", $"Sufix {sufix.Content} not found", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "endpointssufixs");
                return Content(HttpStatusCode.NotFound, $"Sufix {sufix.Content} not found");
            }
            GeneralLogsController.PostGeneralLog("EndpointSufixs", email, "Gateway", HttpStatusCode.OK.ToString(), "GetEndpointSufix", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "endpointssufixs");
            return Ok(sufix_r);
        }

        /// <summary>
        /// Create Sufix
        /// </summary>
        /// <remarks>
        /// Based on unique key of endpoint sufix contains '/' we need to consume body as primary key
        /// 
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Content": "/api/xpto"
        ///     }
        ///     
        /// </remarks>
        /// <param name="sufix">Sufix to insert</param>
        /// <returns>Sufix inserted</returns>
        /// <response code="200">Returns the newly created Sufix</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/endpointssufixs")]
        public IHttpActionResult PostEndpointSufix(EndpointSufix sufix)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            HandlerXML handlerXML = new HandlerXML(endpointssufixsPath);

            try
            {
                if (sufix == null || sufix.Content == null || sufix.Content == "")
                {
                    GeneralLogsController.PostGeneralLog("EndpointSufixs", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PostEndpointSufix", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                handlerXML.CreateEndpointSufix(sufix);
                GeneralLogsController.PostGeneralLog("EndpointSufixs", email, "Gateway", HttpStatusCode.OK.ToString(), "PostEndpointSufix", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "endpointssufixs");
                return Content(HttpStatusCode.Created, sufix);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("EndpointSufixs", email, "Gateway", HttpStatusCode.InternalServerError.ToString(), "PostEndpointSufix", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "endpointssufixs");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete a sufix based on given content
        /// </summary>
        /// <remarks>
        /// Based on unique key of endpoint sufix contains '/' we need to consume body as primary key
        /// 
        /// Sample request:
        ///
        ///     DELETE
        ///     {
        ///        "Content": "/api/xpto"
        ///     }
        ///     
        /// </remarks>
        /// <param name="sufix">Struct with Sufix Content</param>
        /// <returns>HTTPResponse</returns> 
        /// <response code="200">Returns subjective message</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/endpointssufixs")]
        public IHttpActionResult DeleteEndpointSufix(EndpointSufix sufix)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            HandlerXML handlerXML = new HandlerXML(endpointssufixsPath);

            try
            {
                if (sufix == null || sufix.Content == null || sufix.Content == "")
                {
                    GeneralLogsController.PostGeneralLog("EndpointSufixs", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "DeleteEndpointSufix", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                handlerXML.DeleteEndpointSufix(sufix.Content);
                GeneralLogsController.PostGeneralLog("EndpointSufixs", email, "Gateway", HttpStatusCode.OK.ToString(), "DeleteEndpointSufix", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "endpointssufixs");
                return Ok($"EndPoint {sufix.Content} Deleted");
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("EndpointSufixs", email, "Gateway", HttpStatusCode.InternalServerError.ToString(), "DeleteEndpointSufix", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "endpointssufixs");
                return InternalServerError(ex);
            }
        }
    }
}
