using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace vCardPlatform
{
    class HandlerXML
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

        public List<string> GetUsers()
        {
            List<string> users = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNodeList nodes = doc.SelectNodes("/users");

            foreach (XmlNode item in nodes)
            {
                users.Add(item.InnerText);
            }

            return users;
        }

        public Boolean UpdateNameOfUser(string oldname, string newname)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode nome = doc.SelectSingleNode($"/users/user[name='{oldname}']");

            if (nome != null)
            {
                nome.InnerText = newname;
                doc.Save(XmlFilePath);
                return true;
            }
            return false;
        }

        public Boolean UpdatePasswordOfUser(string oldpass, string newpass)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode password = doc.SelectSingleNode($"/users/user[password='{oldpass}']");

            if (password != null)
            {
                password.InnerText = newpass;
                doc.Save(XmlFilePath);
                return true;
            }
            return false;
        }

        public Boolean UpdateEmailOfUser(string oldmail, string newmail)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode mail = doc.SelectSingleNode($"/users/user[email='{oldmail}']");

            if (mail != null)
            {
                mail.InnerText = newmail;
                doc.Save(XmlFilePath);
                return true;
            }
            return false;
        }

        public Boolean UpdateCodeOfUser(string oldpass, string newcode)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFilePath);

            XmlNode code = doc.SelectSingleNode($"/users/user[password='{oldpass}']");

            if (code != null)
            {
                code.InnerText = newcode;
                doc.Save(XmlFilePath);
                return true;
            }
            return false;
        }

        public bool ValidateXML()
        {
            isValid = true;
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
    }
}
