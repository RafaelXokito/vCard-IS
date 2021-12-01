﻿using System;
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
        public List<Entity> GetEntities()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);
            
            List<Entity> entities = new List<Entity>();
            XmlNodeList nodeList = doc.SelectNodes("/entities/entity");

            foreach (XmlNode node in nodeList)
            {
                Entity entity = new Entity
                {
                    Id = node["id"].InnerText,
                    Name = node["name"].InnerText,
                    Endpoint = node["endpoint"].InnerText,
                    MaxLimit = Convert.ToInt32(node["maxlimit"].InnerText),
                    Categories = new List<Category>()
                };

                XmlNodeList categoriesNodeList = node.SelectNodes("/categories/category");

                foreach (XmlNode categoryNode in categoriesNodeList)
                {
                    Category category = new Category();

                    string typeString = categoryNode.Attributes["type"].InnerText;
                    category.Type = (CategoryType)Enum.Parse(typeof(CategoryType), typeString);

                    category.Name = categoryNode.InnerText;

                    entity.Categories.Add(category);
                }

                entities.Add(entity);
            }
            return entities;
        }

        public Entity GetEntityByName(string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"/entities/entity[name='{name}']");

            if (node == null) return null;

            Entity entity = new Entity
            {
                Id = node["id"].InnerText,
                Name = node["name"].InnerText,
                Endpoint = node["endpoint"].InnerText,
                MaxLimit = Convert.ToInt32(node["maxlimit"].InnerText),
                Categories = new List<Category>()
            };

            XmlNodeList categoriesNodeList = node.SelectNodes("/categories/category");

            foreach (XmlNode categoryNode in categoriesNodeList)
            {
                Category category = new Category();

                string typeString = categoryNode.Attributes["type"].InnerText;
                category.Type = (CategoryType)Enum.Parse(typeof(CategoryType), typeString);

                category.Name = categoryNode.InnerText;

                entity.Categories.Add(category);
            }

            return entity;
        }

        public void CreateEntity(Entity entity)
        {
            if (GetEntityByName(entity.Name) != null)
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

            XmlElement categories = doc.CreateElement("categories");

            foreach (Category cat in entity.Categories)
            {
                XmlElement category = doc.CreateElement("category");

                category.InnerText = cat.Name;
                category.SetAttribute("type", cat.Type.ToString());

                categories.AppendChild(category);
            }

            newEntity.AppendChild(categories);

            doc.Save(XmlFilePath);
        }

        public void UpdateEntity(string name, Entity entity)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"/entities/entity[name='{name}']");

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

            if (entity.Categories != null)
            {
                //Given that categories if required, else need to verify node["categories"] is not null
                XmlElement categories = node["categories"];
                if (node["categories"] == null)
                {
                    categories = doc.CreateElement("categories");
                    node.AppendChild(categories);
                }
                categories.RemoveAll();
                
                foreach (Category category in entity.Categories)
                {
                    XmlElement categoryElement = doc.CreateElement("category");

                    categoryElement.InnerText = category.Name;
                    categoryElement.SetAttribute("type", category.Type.ToString());

                    categories.AppendChild(categoryElement);
                }
            }

            doc.Save(XmlFilePath);
        }

        public void DeleteEntityByName(string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode node = doc.SelectSingleNode($"/entities/entity[name='{name}']");

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
    }
}