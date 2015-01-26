#region

using System;
using System.Collections.Generic;

using System.Linq;
using System.Xml;

#endregion

namespace Tabster.Data
{
    internal static class XmlDocumentExtensions
    {
        public static XmlNode GetElementByTagName(this XmlDocument doc, string name)
        {
            var elems = doc.GetElementsByTagName(name);
            return elems.Count > 0 ? elems[0] : null;
        }
    }

    internal class TabsterXmlDocument
    {
        private readonly string _rootNode;
        private Version _version = new Version("1.0");

        private XmlDocument _xmlDoc = new XmlDocument();
        private XmlDocument _xmlDocTemp;

        public TabsterXmlDocument(string rootNode)
        {
            _rootNode = rootNode;
        }

        public Version Version
        {
            get { return _version; }
            set { _version = value; }
        }

        private void PrepareTempDocument()
        {
            _xmlDocTemp = new XmlDocument();
            _xmlDocTemp.AppendChild(_xmlDocTemp.CreateXmlDeclaration("1.0", "ISO-8859-1", null));
            var root = _xmlDocTemp.CreateElement(_rootNode);

            var versionAttribute = _xmlDocTemp.CreateAttribute("version");
            versionAttribute.Value = Version.ToString();
            root.Attributes.Append(versionAttribute);

            _xmlDocTemp.AppendChild(root);
        }

        public void Load(string fileName)
        {
            _xmlDoc = new XmlDocument();
            _xmlDoc.Load(fileName);

            Version = new Version(_xmlDoc.DocumentElement.Attributes["version"].Value);
        }

        public void Save(string fileName)
        {
            _xmlDoc = _xmlDocTemp;
            _xmlDoc.Save(fileName);
            _xmlDocTemp = null;
        }

        public string TryReadNodeValue(string name, string defaultValue = null)
        {
            var elem = _xmlDoc.GetElementByTagName(name);
            return elem != null ? elem.InnerText : defaultValue;
        }

        public string TryReadNodeValues(string[] names, string defaultValue = null)
        {
            foreach (var name in names)
            {
                var value = TryReadNodeValue(name);

                if (value != null)
                {
                    return value;
                }
            }

            return defaultValue;
        }

        public List<XmlNode> ReadChildNodes(string name)
        {
            var nodes = new List<XmlNode>();

            var elem = _xmlDoc.GetElementByTagName(name);

            if (elem != null && elem.HasChildNodes)
            {
                nodes.AddRange(elem.ChildNodes.Cast<XmlNode>());
            }

            return nodes;
        }

        public List<string> ReadChildNodeValues(string name)
        {
            var values = new List<string>();

            var elem = _xmlDoc.GetElementByTagName(name);

            if (elem != null && elem.HasChildNodes)
            {
                values.AddRange(from XmlNode child in elem.ChildNodes select child.InnerText);
            }

            return values;
        }

        public void WriteNode(string name, string value = null, string parentNode = null, SortedDictionary<string, string> attributes = null, bool preventNodeDuplication = true)
        {
            if (_xmlDocTemp == null)
                PrepareTempDocument();

            XmlNode parent = _xmlDocTemp.DocumentElement;

            if (parentNode != null)
            {
                var elem = _xmlDocTemp.GetElementByTagName(parentNode);

                if (elem != null)
                {
                    parent = elem;
                }
            }

            XmlNode workingNode = null;

            if (preventNodeDuplication)
            {
                workingNode = _xmlDocTemp.GetElementByTagName(name);
            }

            if (workingNode == null)
                workingNode = _xmlDocTemp.CreateElement(name);

            if (value != null)
                workingNode.InnerText = value;

            if (attributes != null)
            {
                foreach (var kv in attributes)
                {
                    var att = _xmlDocTemp.CreateAttribute(kv.Key);
                    att.Value = kv.Value;

                    workingNode.Attributes.Append(att);
                }
            }

            parent.AppendChild(workingNode);
        }
    }
}