using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using System.Xml;

namespace vCardGateway
{
    class Log
    {
        public static string BuildMessage(string strMessage, string strStatus, DateTime dateTimestamp)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("log");

            XmlElement message = doc.CreateElement("message");
            message.InnerText = strMessage;

            XmlElement status = doc.CreateElement("status");
            status.InnerText = strStatus;

            XmlElement timestamp = doc.CreateElement("timestamp");
            timestamp.InnerText = dateTimestamp.ToString("G");

            doc.AppendChild(root);

            root.AppendChild(message);
            root.AppendChild(status);
            root.AppendChild(timestamp);

            return doc.OuterXml;
        }
    }
}
