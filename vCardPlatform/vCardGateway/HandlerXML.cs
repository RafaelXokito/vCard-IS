using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using vCardGateway.Models;

namespace vCardGateway
{
    public class HandlerXML
    {
        public HandlerXML(string xmlFile)
        {
            XmlFilePath = xmlFile;
        }

        public HandlerXML(string xmlFile, string xsdFile)
        {
            XmlFilePath = xmlFile;
            XsdFilePath = xsdFile;
        }

        public string XmlFilePath { get; set; }
        public string XsdFilePath { get; set; }

        private bool isValid = true;
        private string validationMessage;
        public string ValidationMessage
        {
            get { return validationMessage; }
        }

        #region ENTITIES
        public List<Entity> GetEntities(string name = null)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);
            
            List<Entity> entities = new List<Entity>();
            string query = "/entities/entity";
            if (name != null)
            {
                query += $"[name= '{name}']";
            }
            XmlNodeList nodeList = doc.SelectNodes(query);

            foreach (XmlNode node in nodeList)
            {
                XmlNode authNode = node.SelectSingleNode($"/entities/entity[id='{node["id"].InnerText}']/authentication");
                Entity entity = new Entity
                {
                    Id = node["id"].InnerText,
                    Name = node["name"].InnerText,
                    Endpoint = node["endpoint"].InnerText,
                    MaxLimit = Convert.ToDecimal(node["maxlimit"].InnerText),
                    EarningPercentage = Convert.ToDecimal(node["earningpercentage"].InnerText),
                    Authentication = new Authentication
                    {
                        Token = authNode["token"].InnerText,
                        Username = authNode["username"].InnerText,
                        Password = authNode["password"].InnerText,
                    }
                };

                entities.Add(entity);
            }
            return entities;
        }

        public Entity GetEntity(string id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"/entities/entity[id='{id}']");

            if (node == null) return null;

            XmlNode authNode = node.SelectSingleNode($"/entities/entity[id='{node["id"].InnerText}']/authentication");

            Entity entity = new Entity
            {
                Id = node["id"].InnerText,
                Name = node["name"].InnerText,
                Endpoint = node["endpoint"].InnerText,
                MaxLimit = Convert.ToDecimal(node["maxlimit"].InnerText),
                EarningPercentage = Convert.ToDecimal(node["earningpercentage"].InnerText),
                Authentication = new Authentication
                {
                    Token = authNode["token"].InnerText,
                    Username = authNode["username"].InnerText,
                    Password = authNode["password"].InnerText,
                }
            };

            return entity;
        }
        
        public Entity GetEntityByName(string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"/entities/entity[name='{name}']");

            if (node == null) return null;
            XmlNode authNode = node.SelectSingleNode($"/entities/entity[id='{node["id"].InnerText}']/authentication");

            Entity entity = new Entity
            {
                Id = node["id"].InnerText,
                Name = node["name"].InnerText,
                Endpoint = node["endpoint"].InnerText,
                MaxLimit = Convert.ToDecimal(node["maxlimit"].InnerText),
                EarningPercentage = Convert.ToDecimal(node["earningpercentage"].InnerText),
                Authentication = new Authentication
                {
                    Token = authNode["token"].InnerText,
                    Username = authNode["username"].InnerText,
                    Password = authNode["password"].InnerText,
                }
            };

            return entity;
        }

        public Entity GetEntityByEndPoint(string endpoint)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"/entities/entity[endpoint='{endpoint}']");

            if (node == null) return null;
            XmlNode authNode = node.SelectSingleNode($"/entities/entity[id='{node["id"].InnerText}']/authentication");

            Entity entity = new Entity
            {
                Id = node["id"].InnerText,
                Name = node["name"].InnerText,
                Endpoint = node["endpoint"].InnerText,
                MaxLimit = Convert.ToDecimal(node["maxlimit"].InnerText),
                EarningPercentage = Convert.ToDecimal(node["earningpercentage"].InnerText),
                Authentication = new Authentication
                {
                    Token = authNode["token"].InnerText,
                    Username = authNode["username"].InnerText,
                    Password = authNode["password"].InnerText,
                }
            };

            return entity;
        }

        public Entity CreateEntity(Entity entity)
        {
            if (GetEntity(entity.Id) != null)
            {
                throw new Exception("A entity with that name already exists");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode root = doc.SelectSingleNode($"/entities");

            XmlElement newEntity = doc.CreateElement("entity");
            root.AppendChild(newEntity);

            XmlElement id = doc.CreateElement("id");
            id.InnerText = Guid.NewGuid().ToString();
            newEntity.AppendChild(id);

            XmlElement name = doc.CreateElement("name");
            name.InnerText = entity.Name;
            newEntity.AppendChild(name);

            XmlElement endpoint = doc.CreateElement("endpoint");
            endpoint.InnerText = entity.Endpoint;
            newEntity.AppendChild(endpoint);

            XmlElement maxlimit = doc.CreateElement("maxlimit");
            maxlimit.InnerText = Convert.ToString(entity.MaxLimit);
            newEntity.AppendChild(maxlimit);

            XmlElement earningpercentage = doc.CreateElement("earningpercentage");
            earningpercentage.InnerText = Convert.ToString(entity.EarningPercentage);
            newEntity.AppendChild(earningpercentage);

            XmlElement authentication = doc.CreateElement("authentication");
            XmlElement token = doc.CreateElement("token");
            token.InnerText = Convert.ToString(entity.Authentication.Token);
            authentication.AppendChild(token);
            XmlElement username = doc.CreateElement("username");
            username.InnerText = Convert.ToString(entity.Authentication.Username);
            authentication.AppendChild(username);
            XmlElement password = doc.CreateElement("password");
            password.InnerText = Convert.ToString(entity.Authentication.Password);
            authentication.AppendChild(password);
            newEntity.AppendChild(authentication);

            doc.Save(XmlFilePath);
            return entity;
        }

        public void UpdateEntity(string id, Entity entity)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"/entities/entity[id='{id}']");

            if (node == null)
            {
                throw new Exception("The entity that you are looking for, does not exist");
            }

            if (entity.Name != null)
                node["name"].InnerText = entity.Name;

            if (entity.Endpoint != null)
                node["endpoint"].InnerText = entity.Endpoint;

            if (entity.MaxLimit != 0)
                node["maxlimit"].InnerText = Convert.ToString(entity.MaxLimit);

            if (entity.EarningPercentage != 0)
                node["earningpercentage"].InnerText = Convert.ToString(entity.EarningPercentage);

            if (entity.Authentication != null) { 
                XmlNode authNode = node.SelectSingleNode($"/entities/entity[id='{node["id"].InnerText}']/authentication");
                
                if (entity.Authentication.Token != null)
                    authNode["token"].InnerText = Convert.ToString(entity.Authentication.Token);

                if (entity.Authentication.Username != null)
                    authNode["username"].InnerText = Convert.ToString(entity.Authentication.Username);

                if (entity.Authentication.Password != null)
                    authNode["password"].InnerText = Convert.ToString(entity.Authentication.Password);
            }

            doc.Save(XmlFilePath);
        }

        public void UpdateEntityAuth(string id, Authentication authentication)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"/entities/entity[id='{id}']");

            if (node == null)
            {
                throw new Exception("The entity that you are looking for, does not exist");
            }

            XmlNode authNode = node.SelectSingleNode($"/entities/entity[id='{node["id"].InnerText}']/authentication");

            authNode["token"].InnerText = Convert.ToString(authentication.Token);

            authNode["username"].InnerText = Convert.ToString(authentication.Username);

            authNode["password"].InnerText = Convert.ToString(authentication.Password);

            doc.Save(XmlFilePath);
        }

        public void DeleteEntity(string id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"/entities/entity[id='{id}']");

            if (node == null)
            {
                throw new Exception("The entity that you are looking for, does not exist");
            }

            XmlNode root = node.ParentNode;

            if (root.RemoveChild(node) == null)
            {
                throw new Exception("The entity couldn't be deleted with success");
            }

            doc.Save(XmlFilePath);
        }
        #endregion

        #region VALIDATE XML WITH SCHEMA FILE
        public bool ValidateXML()
        {
            isValid = true;
            validationMessage = "XML document is valid";
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(XmlFilePath);
                ValidationEventHandler eventHandler = new ValidationEventHandler(MyValidateMethod);
                doc.Schemas.Add(null, XsdFilePath);
                doc.Validate(eventHandler);
            }
            catch (XmlException ex)
            {
                isValid = false;
                validationMessage = string.Format("ERROR: {0}", ex.ToString());
            }
            return isValid;
        }


        private void MyValidateMethod(object sender, ValidationEventArgs args)
        {
            isValid = false;
            switch (args.Severity)
            {
                case XmlSeverityType.Error:
                    validationMessage = string.Format("ERROR: {0}", args.Message);
                    break;
                case XmlSeverityType.Warning:
                    validationMessage = string.Format("WARNING: {0}", args.Message);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ENDPOINTSSUFIXS
        public List<EndpointSufix> GetEndpointSufixs()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            List<EndpointSufix> sufixs = new List<EndpointSufix>();
            XmlNodeList nodeList = doc.SelectNodes("//sufix");

            foreach (XmlNode node in nodeList)
            {
                EndpointSufix sufix = new EndpointSufix
                {
                    Content = node.InnerText,
                };

                sufixs.Add(sufix);
            }
            return sufixs;
        }

        public EndpointSufix GetEndpointSufix(string content)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"//sufix[.='{content}']");

            if (node == null) return null;

            EndpointSufix sufix = new EndpointSufix
            {
                Content = node.InnerText,
            };

            return sufix;
        }

        public void CreateEndpointSufix(EndpointSufix sufix)
        {
            if (GetEndpointSufix(sufix.Content) != null)
            {
                throw new Exception("A sufix like that already exists");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode root = doc.SelectSingleNode($"/sufixs");

            XmlElement newsufix = doc.CreateElement("sufix");
            newsufix.InnerText = sufix.Content;
            root.AppendChild(newsufix);

            doc.Save(XmlFilePath);
        }

        public void UpdateEndpointSufix(string content, EndpointSufix sufix)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"//sufix[.='{content}']");

            if (node == null)
            {
                throw new Exception("The sufix that you are looking for, does not exist");
            }

            if (sufix.Content != null)
                node.InnerText = sufix.Content;

            doc.Save(XmlFilePath);
        }

        public void DeleteEndpointSufix(string content)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"//sufix[.='{content}']");

            if (node == null)
            {
                throw new Exception("The sufix that you are looking for, does not exist");
            }

            XmlNode root = node.ParentNode;

            if (root.RemoveChild(node) == null)
            {
                throw new Exception("The sufix couldn't be deleted with success");
            }

            doc.Save(XmlFilePath);
        }
        #endregion
    }
}