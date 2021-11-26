using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Endpoint> GetEndpoints()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);
            
            List<Endpoint> endpoints = new List<Endpoint>();
            XmlNodeList nodeList = doc.SelectNodes("/endpoints/endpoint");

            foreach (XmlNode node in nodeList)
            {
                Endpoint endpoint = new Endpoint
                {
                    Name = node["name"].InnerText,
                    Url = node["url"].InnerText
                };
                endpoints.Add(endpoint);
            }
            return endpoints;
        }

        public Endpoint GetEndpointByName(string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"/endpoints/endpoint[name='{name}']");

            if (node == null) return null;

            Endpoint endpoint = new Endpoint
            {
                Name = node["name"].InnerText,
                Url = node["url"].InnerText
            };

            return endpoint;
        }

        public void createEndpoint(Endpoint endpoint)
        {
            if (GetEndpointByName(endpoint.Name) != null)
            {
                throw new Exception("A endpoint with that name already exists");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode root = doc.SelectSingleNode($"/endpoints");

            XmlElement newEndpoint = doc.CreateElement("endpoint");
            root.AppendChild(newEndpoint);

            XmlElement name = doc.CreateElement("name");
            name.InnerText = endpoint.Name;
            newEndpoint.AppendChild(name);

            XmlElement url = doc.CreateElement("url");
            url.InnerText = endpoint.Url;
            newEndpoint.AppendChild(url);

            doc.Save(XmlFilePath);
        }

        public void deleteEndpointByName(string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"/endpoints/endpoint[name='{name}']");

            if (node == null)
            {
                throw new Exception("The endpoint that you are looking for, does not exist");
            }

            XmlNode root = node.ParentNode;

            if (root.RemoveChild(node) == null)
            {
                throw new Exception("The endpoint couldn't be deleted with success");
            }

            doc.Save(XmlFilePath);
        }

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
    }
}