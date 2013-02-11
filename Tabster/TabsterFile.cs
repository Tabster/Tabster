#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

#endregion

namespace Tabster
{
    public interface ITabsterFile
    {
        void Load();
        void Save();
    }

    public abstract class TabsterFile
    {
        public FileInfo FileInfo { get; protected set; }
        public Version FileVersion { get; protected set; }
        private XmlDocument RawXml { get; set; }
        protected bool FileFormatOutdated { get; set; }

        protected void BeginFileWrite(string rootNode, string formatVersion, string encoding = "ISO-8859-1")
        {
            RawXml = new XmlDocument();
            RawXml.AppendChild(RawXml.CreateXmlDeclaration("1.0", encoding, null));
            var root = RawXml.CreateElement(rootNode);
            var versionAttribute = RawXml.CreateAttribute("version");
            versionAttribute.Value = formatVersion;
            root.Attributes.Append(versionAttribute);
            RawXml.AppendChild(root);
        }

        protected void FinishFileWrite()
        {
            Save();
        }

        protected void Save(string filePath = null)
        {
            if (RawXml != null)
            {
                var isCustomPath = filePath != null;
                FileInfo customPath = null;

                if (isCustomPath)
                    customPath = new FileInfo(filePath);

                var fullSavePath = isCustomPath
                                       ? Global.GenerateUniqueFilename(customPath.DirectoryName, string.Format("{0}{1}", FileInfo.Name, FileInfo.Extension))
                                       : FileInfo.FullName;

                RawXml.Save(fullSavePath);
                FileInfo = new FileInfo(fullSavePath);
            }
        }

        protected Version GetFormatVersion()
        {
            if (RawXml != null)
            {
                var versionAttribute = RawXml.DocumentElement != null && RawXml.DocumentElement.HasAttribute("version") ? RawXml.DocumentElement.Attributes["version"] : null;

                if (versionAttribute != null)
                    return new Version(versionAttribute.Value);
            }

            return null;
        }

        protected string ReadNodeValue(string nodeName, bool returnNull = false)
        {
            if (RawXml != null)
            {
                var matches = RawXml.GetElementsByTagName(nodeName);
                return matches.Count > 0 ? matches[0].InnerText : (returnNull ? null : string.Empty);
            }

            return returnNull ? null : string.Empty;
        }

        protected List<string> ReadChildValues(string parentNodeName)
        {
            if (RawXml != null)
            {
                var matches = RawXml.GetElementsByTagName(parentNodeName);

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

        protected XmlNode WriteNode(string name, string innertext = null, XmlNode parentNode = null, SortedDictionary<string, string> attributes = null)
        {      
            //check if node already exists
            var existingnodes = RawXml.GetElementsByTagName(name);

            XmlNode associatedNode;

            if (existingnodes.Count > 0)
            {
                associatedNode = existingnodes[0];
            }

            else
            {
                var parent = parentNode ?? RawXml.DocumentElement;
                associatedNode = RawXml.CreateElement(name);
                parent.AppendChild(associatedNode);
            }

            if (innertext != null)
                associatedNode.InnerText = innertext;

            if (attributes != null)
            {
                foreach (var kv in attributes)
                {
                    var att = RawXml.CreateAttribute(kv.Key);
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

        public void RenameFile(string newName)
        {
            var newFilePath = Global.GenerateUniqueFilename(FileInfo.DirectoryName, newName);
            File.Copy(FileInfo.FullName, newFilePath);
            File.Delete(FileInfo.FullName);
            FileInfo = new FileInfo(newFilePath);
        }
    }
}