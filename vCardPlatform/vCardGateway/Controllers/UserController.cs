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
    public class UserController : ApiController
    {
        private string entitiesPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Entities.xml";

        /// <summary>
        /// Try to signin with entity user credentials
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Username": "900000001",
        ///        "Password": "1234"
        ///     }
        ///     
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="credentials">User Credentials</param>
        /// <returns>User auth token</returns>
        /// <response code="200">User auth token generated</response>
        /// <response code="400">User credentials are wrong</response>
        [Route("api/entities/{entity_id}/signin")]
        public IHttpActionResult PostSignin(string entity_id, [FromBody] Credentials credentials)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                if (credentials == null)
                {
                    GeneralLogsController.PostGeneralLog("Users", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PostSignin", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"signin", Method.POST, DataFormat.Json);
                request.AddJsonBody(credentials);
                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse<User> response = client.Execute<User>(request);
                dynamic dataDefaultUser = JsonConvert.DeserializeObject(response.Content);
                if (dataDefaultUser != null)
                {
                    GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "PostSignin", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                    return Content(response.StatusCode, dataDefaultUser);
                }
                GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "PostSignin", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Users", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "PostSignin", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");

                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for all users based on User authenticated
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <returns>A list of all users</returns>
        /// <response code="200">Returns the users found. Returns null if you are not authorized</response>
        [Route("api/entities/{entity_id}/users")]
        public IHttpActionResult GetUsers(string entity_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("users", Method.GET);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse response = client.Execute(request);
                dynamic dataDefaultUser = JsonConvert.DeserializeObject(response.Content);
                if (dataDefaultUser != null)
                {
                    GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "GetUsers", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                    return Content(response.StatusCode, dataDefaultUser);
                }
                GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "GetUsers", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                return Content(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString());
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Users", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "GetUsers", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for user based on User authenticated
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="user_id">User ID</param>
        /// <returns>A users</returns>
        /// <response code="200">Returns the User found.</response>
        [Route("api/entities/{entity_id}/users/{user_id}")]
        public IHttpActionResult GetUser(string entity_id, int user_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"users/{user_id}", Method.GET);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse<User> response = client.Execute<User>(request);
                //if (response.IsSuccessful && response.Data != null)
                //{
                //    return Content(response.StatusCode, response.Data);
                //}
                dynamic dataDefaultUser = JsonConvert.DeserializeObject(response.Content);
                GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "GetUser", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                if (dataDefaultUser != null)
                {
                    return Content(response.StatusCode, dataDefaultUser);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Users", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "GetUser", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Insert User for authenticated User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Photo": Base64(Image)
        ///     }
        ///     
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="username">User Phone Number</param>
        /// <param name="user">User with photo param as base64 string</param>
        /// <returns>User updated</returns>
        /// <response code="200">Returns the updated created User</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/users/{username}/photo")]
        public IHttpActionResult PostUserPhoto(string entity_id, string username, User user)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                if (user == null || user.Photo == null)
                {
                    GeneralLogsController.PostGeneralLog("Users", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PostUserPhoto", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"users/{username}/photo", Method.POST, DataFormat.Json);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                request.AddJsonBody(user);
                IRestResponse response = client.Execute(request);
                dynamic dataDefaultUser = JsonConvert.DeserializeObject(response.Content);
                GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "PostUserPhoto", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                if (dataDefaultUser != null)
                {
                    return Content(response.StatusCode, dataDefaultUser);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Users", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "PostUserPhoto", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Create User into given entity
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///       "Username": "900000001",
        ///       "Password": "1234",
        ///       "Name": "Jhon",
        ///       "Email": "900000001@mail.pt",
        ///       "ConfirmationCode": "1234"
        ///     }
        ///     
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="user">User to insert</param>
        /// <returns>User inserted</returns>
        /// <response code="200">Returns the newly created User</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/users")]
        public IHttpActionResult PostUser(string entity_id, User user)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                if (user == null || user.Username == null || user.Username == ""
                                 || user.Password == null || user.Password == ""
                                 || user.Name == null || user.Name == ""
                                 || user.Email == null || user.Email == ""
                                 || user.ConfirmationCode == null || user.ConfirmationCode == "")
                {
                    GeneralLogsController.PostGeneralLog("Users", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PostUser", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }

                if (!IsValidPhone(user.Username))
                {
                    GeneralLogsController.PostGeneralLog("Users", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PostUser", $"Phone Number must match portuguese phone number", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return BadRequest($"Phone Number must match portuguese phone number");
                }

                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("users", Method.POST, DataFormat.Json);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                request.AddJsonBody(user);
                IRestResponse<User> response = client.Execute<User>(request);
                dynamic dataDefaultUser = JsonConvert.DeserializeObject(response.Content);
                GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "PostUser", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                if (dataDefaultUser != null)
                {
                    return Content(response.StatusCode, dataDefaultUser);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Users", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "PostUser", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                return InternalServerError(ex);
            }
        }

        public bool IsValidPhone(string Phone)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;
                var r = new Regex(@"^([9][1236])[0-9]*$");
                return r.IsMatch(Phone);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update entity User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///       "Name": "Jhon",
        ///       "Email": "900000001@mail.pt",
        ///     }
        ///     
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="username">User Phone Number</param>
        /// <param name="user">User to insert</param>
        /// <returns>User inserted</returns>
        /// <response code="200">Returns the newly updated User</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/users/{username}")]
        public IHttpActionResult PutUser(string entity_id, string username, User user)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                if (user == null || user.Name == null || user.Name == ""
                                 || user.Email == null || user.Email == "")
                {
                    GeneralLogsController.PostGeneralLog("Users", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PutUser", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"users/{username}", Method.PUT, DataFormat.Json);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                request.AddJsonBody(user);
                IRestResponse<User> response = client.Execute<User>(request);
                dynamic dataDefaultUser = JsonConvert.DeserializeObject(response.Content);
                GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "PutUser", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                if (dataDefaultUser != null)
                {
                    return Content(response.StatusCode, dataDefaultUser);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Users", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "PutUser", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Update authenticated User Maximum Limit
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH
        ///     {
        ///         "MaximumLimit": "1234"
        ///     }
        ///
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="username">User Phone Number</param>
        /// <param name="user">Secret struct body used to update</param>
        /// <returns>User Updated</returns>
        /// <response code="200">Returns the updated updated User</response>
        /// <response code="401">User does not belongs to authenticated user</response>
        /// <response code="404">If given User not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/users/{username}/maxlimit")]
        public IHttpActionResult PatchUserMaxLimit(string entity_id, string username, [FromBody] User user)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                if (user == null || user.MaximumLimit <= 0)
                {
                    GeneralLogsController.PostGeneralLog("Users", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PatchUserMaxLimit", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }

                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"users/{username}/maxlimit", Method.PATCH, DataFormat.Json);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                request.AddJsonBody (user);
                IRestResponse<User> response = client.Execute<User>(request);
                dynamic dataDefaultUser = JsonConvert.DeserializeObject(response.Content);
                GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "PatchUserMaxLimit", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                if (dataDefaultUser != null)
                {
                    return Content(response.StatusCode, dataDefaultUser);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Users", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "PatchUserMaxLimit", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Update authenticated User Password
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH
        ///     {
        ///         "Password": "1234",
        ///         "NewPassword": "1234",
        ///     }
        ///
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="username">User Phone Number</param>
        /// <param name="secret">Secret struct body used to update</param>
        /// <returns>User Updated</returns>
        /// <response code="200">Returns the updated updated User</response>
        /// <response code="401">User does not belongs to authenticated user</response>
        /// <response code="404">If given User not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/users/{username}/password")]
        public IHttpActionResult PatchUserPassword(string entity_id, string username, [FromBody] Secret secret)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                if (secret == null || secret.Password == null || secret.Password == ""
                                 || secret.NewPassword == null || secret.NewPassword == "")
                {
                    GeneralLogsController.PostGeneralLog("Users", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PatchUserPassword", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"users/{username}/password", Method.PATCH, DataFormat.Json);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                request.AddJsonBody(secret);
                IRestResponse<User> response = client.Execute<User>(request);
                dynamic dataDefaultUser = JsonConvert.DeserializeObject(response.Content);
                GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "PatchUserPassword", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                if (dataDefaultUser != null)
                {
                    return Content(response.StatusCode, dataDefaultUser);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Users", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "PatchUserPassword", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Update authenticated User Confirmation Code
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH
        ///     {
        ///         "Password": "1234",
        ///         "NewConfirmationCode": "1234",
        ///     }
        ///
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="username">User Phone Number</param>
        /// <param name="secret">Secret struct to be updated</param>
        /// <returns>User Updated</returns>
        /// <response code="200">Returns the updated updated User</response>
        /// <response code="401">User does not belongs to authenticated user</response>
        /// <response code="404">If given User not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/users/{username}/confirmationcode")]
        public IHttpActionResult PatchUserConfirmationCode(string entity_id, string username, [FromBody] Secret secret)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                if (secret == null || secret.Password == null || secret.Password == ""
                                 || secret.NewConfirmationCode == null || secret.NewConfirmationCode == "")
                {
                    GeneralLogsController.PostGeneralLog("Users", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PatchUserConfirmationCode", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"users/{username}/confirmationcode", Method.PATCH, DataFormat.Json);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                request.AddJsonBody(secret);
                IRestResponse<User> response = client.Execute<User>(request);
                dynamic dataDefaultUser = JsonConvert.DeserializeObject(response.Content);
                GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "PatchUserConfirmationCode", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                if (dataDefaultUser != null)
                {
                    return Content(response.StatusCode, dataDefaultUser);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Users", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "PatchUserConfirmationCode", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Delete a user based on given ID and on User authenticated
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="user_id">User ID</param>
        /// <returns>HTTPResponse</returns>
        /// <response code="200">Returns subjective message</response>
        /// <response code="401">User does not belongs to authenticated user</response>
        /// <response code="404">If given User not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/users/{user_id}")]
        public IHttpActionResult DeleteUsers(string entity_id, int user_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"users/{user_id}", Method.DELETE);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);

                IRestResponse response = client.Execute(request);
                dynamic dataDefaultUser = JsonConvert.DeserializeObject(response.Content);
                GeneralLogsController.PostGeneralLog("Users", "N/A", entity.Name, response.StatusCode.ToString(), "DeleteUsers", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                if (dataDefaultUser != null)
                {
                    return Content(response.StatusCode, dataDefaultUser);
                }
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Users", "N/A", "GATEWAY", HttpStatusCode.InternalServerError.ToString(), "DeleteUsers", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds), "users");
                return InternalServerError(ex);
            }
        }
    }
}
