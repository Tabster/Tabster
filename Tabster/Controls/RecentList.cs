#region

using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Tabster.Core.FileTypes;

#endregion

namespace Tabster.Controls
{
    public class RecentToolStripMenuItem : ToolStripMenuItem
    {
        private bool _hasClearButton;

        public RecentToolStripMenuItem()
        {
            ShowClear = true;
            MaxItems = 10;
        }

        public string FilePath { get; set; }
        public bool ShowClear { get; set; }
        public int MaxItems { get; set; }

        public event EventHandler OnItemClicked;

        private bool IsRecent(TablatureDocument tab, out int index)
        {
            for (var i = 0; i < DropDownItems.Count; i++)
            {
                var item = DropDownItems[i] as ToolStripMenuItem;

                if (item == null) //seperator
                    break;

                var path = item.ToolTipText;

                if (tab.FileInfo.FullName.Equals(path, StringComparison.InvariantCultureIgnoreCase))
                {
                    index = i;
                    return true;
                }
            }

            index = 0;
            return false;
        }

        public void Add(TablatureDocument tab, bool save = true)
        {
            //check if it's already on the list
            int existingIndex;
            if (IsRecent(tab, out existingIndex))
            {
                DropDownItems.RemoveAt(existingIndex);
            }

            var item = new ToolStripMenuItem(string.Format("{0} - {1}", tab.Artist, tab.Title)) {ToolTipText = tab.FileInfo.FullName};
            item.Click += item_clicked;
            ((ToolStripDropDownMenu) item.DropDown).ShowImageMargin = false;
            ((ToolStripDropDownMenu) item.DropDown).ShowCheckMargin = false;
            DropDownItems.Insert(0, item);

            if (DropDownItems.Count == MaxItems)
            {
                DropDownItems.RemoveAt(MaxItems - 1);
            }

            Enabled = true;

            if (ShowClear)
            {
                AddClearButton();
            }

            if (save)
                Save();
        }

        private void AddClearButton()
        {
            if (!_hasClearButton)
            {
                DropDownItems.Add(new ToolStripSeparator());
                var clearMenuItem = new ToolStripMenuItem("Clear Items") {ToolTipText = "Clears all recent items."};
                clearMenuItem.Click += Clear;
                DropDownItems.Add(clearMenuItem);

                _hasClearButton = true;
            }
        }

        private void Save()
        {
            var doc = new XmlDocument();

            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));

            var root = doc.CreateElement("recent");
            doc.AppendChild(root);

            foreach (var item in DropDownItems)
            {
                var menuItem = item as ToolStripMenuItem;

                if (menuItem != null)
                {
                    var elem = doc.CreateElement("item");
                    elem.InnerText = menuItem.ToolTipText;
                    root.AppendChild(elem);
                }

                else
                {
                    break; //seperator
                }
            }

            doc.Save(FilePath);
        }

        public void Load()
        {
            if (!File.Exists(FilePath))
                return;

            var doc = new XmlDocument();
            doc.Load(FilePath);
            var files = doc.GetElementsByTagName("recent")[0].ChildNodes;

            DropDownItems.Clear();

            if (files.Count > 0)
            {
                var processor = new TabsterDocumentProcessor<TablatureDocument>(TablatureDocument.FILE_VERSION, true);

                foreach (XmlNode file in files)
                {
                    var tabDoc = processor.Load(file.InnerText);

                    if (tabDoc != null)
                    {
                        Add(tabDoc);
                    }
                }

                if (ShowClear)
                {
                    AddClearButton();
                }
            }
        }

        public void Clear(object sender = null, EventArgs e = null)
        {
            if (DropDownItems.Count > 0)
            {
                DropDownItems.Clear();
                var doc = new XmlDocument();
                doc.Load(FilePath);
                doc.GetElementsByTagName("recent")[0].RemoveAll();
                doc.Save(FilePath);
                Enabled = false;
                _hasClearButton = false;
            }
        }

        private void item_clicked(object sender, EventArgs e)
        {
            if (OnItemClicked != null)
            {
                OnItemClicked(sender, e);
            }
        }
    }
}