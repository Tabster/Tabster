#region

using System.Collections.Generic;
using System.Linq;
using System.Xml;

#endregion

namespace Tabster.Data.Xml
{
    internal static class XmlDocumentExtensions
    {
        public static XmlNode GetElementByTagName(this XmlDocument doc, string name)
        {
            var elems = doc.GetElementsByTagName(name);
            return elems.Count > 0 ? elems[0] : null;
        }

        public static string GetAttributeValue(this XmlNode node, string name, string defaultValue = null)
        {
            if (node.Attributes != null)
            {
                var attr = node.Attributes[name];
                if (attr != null)
                    return attr.Value;
            }

            return defaultValue;
        }

        public static void SetAttributeValue(this XmlDocument doc, XmlNode node, string name, string value)
        {
            var attribute = doc.Attributes[name];

            if (attribute != null)
                attribute = doc.CreateAttribute(name);

            attribute.Value = value;
        }

        public static List<XmlNode> GetChildNodes(this XmlDocument doc, XmlNode node)
        {
            var nodes = new List<XmlNode>();

            if (node != null && node.HasChildNodes)
                nodes.AddRange(node.ChildNodes.Cast<XmlNode>());

            return nodes;
        }

        public static string GetNodeValue(this XmlDocument doc, string nodeName, string defaultValue = null)
        {
            var elem = doc.GetElementByTagName(nodeName);
            return elem != null ? elem.InnerText : defaultValue;
        }

        public static string GetNodeValues(this XmlDocument doc, string[] nodeNames, string defaultValue = null)
        {
            foreach (var name in nodeNames)
            {
                var value = doc.GetNodeValue(name);

                if (value != null)
                    return value;
            }

            return defaultValue;
        }

        public static void WriteNode(this XmlDocument doc, string name, string value = null, string parentNode = null, SortedDictionary<string, string> attributes = null, bool preventNodeDuplication = true)
        {
            XmlNode parent = doc.DocumentElement;

            if (parentNode != null)
            {
                var elem = doc.GetElementByTagName(parentNode);

                if (elem != null)
                {
                    parent = elem;
                }
            }

            XmlNode workingNode = null;

            if (preventNodeDuplication)
            {
                workingNode = doc.GetElementByTagName(name);
            }

            if (workingNode == null)
                workingNode = doc.CreateElement(name);

            if (value != null)
                workingNode.InnerText = value;

            if (attributes != null)
            {
                foreach (var kv in attributes)
                {
                    var att = doc.CreateAttribute(kv.Key);
                    att.Value = kv.Value;

                    workingNode.Attributes.Append(att);
                }
            }

            parent.AppendChild(workingNode);
        }
    }
}