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
            timestamp.InnerText = dateTimestamp.ToString("g");

            doc.AppendChild(root);

            root.AppendChild(message);
            root.AppendChild(status);
            root.AppendChild(timestamp);

            return doc.OuterXml;
        }

        public static bool SendMessage(MqttClient m_cClient, string topic, string message)
        {
            if (!m_cClient.IsConnected)
            {
                m_cClient.Connect(Guid.NewGuid().ToString());
                if (!m_cClient.IsConnected)
                {
                    return false;
                }
            }

            if (message.Trim().Length <= 0)
            {
                return false;
            }
            m_cClient.Publish(topic, Encoding.UTF8.GetBytes(message));

            return true;
        }
    }
}
