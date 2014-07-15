#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

#endregion

namespace Tabster.Core
{
    public interface ITabsterFile
    {
        void Load();
        void Save();
    }

    public class ElementNode
    {
        public ElementNode(SortedDictionary<string, string> attributes, string value)
        {
            Attributes = attributes;
            Value = value;
        }

        public SortedDictionary<string, string> Attributes { get; private set; }
        public string Value { get; private set; }
    }

    public abstract class TabsterFile
    {
        private XmlDocument _rawXML;
        public FileInfo FileInfo { get; protected set; }
        public Version FileVersion { get; protected set; }
        protected bool FileFormatOutdated { get; set; }

        protected void BeginFileWrite(string rootNode, string formatVersion, string encoding = "ISO-8859-1")
        {
            _rawXML = new XmlDocument();
            _rawXML.AppendChild(_rawXML.CreateXmlDeclaration("1.0", encoding, null));
            var root = _rawXML.CreateElement(rootNode);
            var versionAttribute = _rawXML.CreateAttribute("version");
            versionAttribute.Value = formatVersion;
            root.Attributes.Append(versionAttribute);
            _rawXML.AppendChild(root);
        }

        protected void FinishFileWrite()
        {
            Save();
        }

        protected void BeginFileRead(Version newFileVersion)
        {
            _rawXML = new XmlDocument();
            _rawXML.Load(FileInfo.FullName);
            FileVersion = GetFormatVersion();
            FileFormatOutdated = FileVersion == null || FileVersion < newFileVersion;
        }

        protected void Save(string filePath = null)
        {
            if (_rawXML != null)
            {
                var isCustomPath = filePath != null;
                FileInfo customPath = null;

                if (isCustomPath)
                    customPath = new FileInfo(filePath);

                var fullSavePath = isCustomPath
                                       ? GenerateUniqueFilename(customPath.DirectoryName, String.Format("{0}{1}", FileInfo.Name, FileInfo.Extension))
                                       : FileInfo.FullName;

                _rawXML.Save(fullSavePath);
                FileInfo = new FileInfo(fullSavePath);
            }
        }

        protected Version GetFormatVersion()
        {
            if (_rawXML != null)
            {
                var versionAttribute = _rawXML.DocumentElement != null && _rawXML.DocumentElement.HasAttribute("version") ? _rawXML.DocumentElement.Attributes["version"] : null;

                if (versionAttribute != null)
                    return new Version(versionAttribute.Value);
            }

            return null;
        }

        protected string ReadNodeValue(string nodeName, bool returnNull = false)
        {
            if (_rawXML != null)
            {
                var matches = _rawXML.GetElementsByTagName(nodeName);
                return matches.Count > 0 ? matches[0].InnerText : (returnNull ? null : String.Empty);
            }

            return returnNull ? null : String.Empty;
        }

        protected string ReadRootAttribute(string name)
        {
            var root = _rawXML.ParentNode;

            if (root != null && root.Attributes != null)
            {
                var attribute = root.Attributes[name];
                return attribute.Value;
            }

            return null;
        }

        protected List<string> ReadChildValues(string parentNodeName)
        {
            if (_rawXML != null)
            {
                var matches = _rawXML.GetElementsByTagName(parentNodeName);

                if (matches.Count > 0)
                {
                    var temp = new List<string>();

                    foreach (XmlNode c in matches[0].ChildNodes)
                    {
                        temp.Add(c.InnerText);
                    }

                    return temp;
                }
            }

            return null;
        }

        protected List<ElementNode> ReadChildNodes(string parentNodeName)
        {
            if (_rawXML != null)
            {
                var matches = _rawXML.GetElementsByTagName(parentNodeName);

                if (matches.Count > 0)
                {
                    var temp = new List<ElementNode>();

                    foreach (XmlNode c in matches[0].ChildNodes)
                    {
                        var attributes = new SortedDictionary<string, string>();

                        if (c.Attributes != null)
                        {
                            foreach (XmlAttribute attribute in c.Attributes)
                            {
                                attributes.Add(attribute.Name, attribute.Value);
                            }
                        }

                        temp.Add(new ElementNode(attributes, c.InnerText));
                    }

                    return temp;
                }
            }

            return null;
        }

        protected XmlNode WriteNode(string name, string innertext = null, XmlNode parentNode = null, SortedDictionary<string, string> attributes = null, bool overwriteDuplicates = true)
        {
            var parent = parentNode ?? _rawXML.DocumentElement;

            //check if node already exists
            var existingnodes = _rawXML.GetElementsByTagName(name);
            var associatedNode = overwriteDuplicates && existingnodes.Count > 0 ? existingnodes[0] : _rawXML.CreateElement(name);
            parent.AppendChild(associatedNode);

            if (innertext != null)
                associatedNode.InnerText = innertext;

            if (attributes != null)
            {
                foreach (var kv in attributes)
                {
                    var att = _rawXML.CreateAttribute(kv.Key);
                    att.Value = kv.Value;
                    associatedNode.Attributes.Append(att);
                }
            }

            return associatedNode;
        }

        public bool Delete()
        {
            try
            {
                File.Delete(FileInfo.FullName);
                return true;
            }

            catch
            {
                return false;
            }
        }

        public static string GenerateUniqueFilename(string directory, string filename)
        {
            //remove invalid file path characters
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var sanitized = new Regex(String.Format("[{0}]", Regex.Escape(regexSearch))).Replace(filename, "");

            var fileName = Path.GetFileNameWithoutExtension(sanitized);
            var fileExt = Path.GetExtension(sanitized);

            var firstTry = Path.Combine(directory, String.Format("{0}{1}", fileName, fileExt));
            if (!File.Exists(firstTry))
                return firstTry;

            for (var i = 1;; ++i)
            {
                var appendedPath = Path.Combine(directory, String.Format("{0} ({1}){2}", fileName, i, fileExt));

                if (!File.Exists(appendedPath))
                    return appendedPath;
            }
        }
    }
}