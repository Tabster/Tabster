#region

using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

#endregion

namespace Tabster.Controls
{
    public class RecentToolStripMenuItem : ToolStripMenuItem
    {
        private string _filepath;
        private int _maxitems = 10;
        private bool _showclear = true;

        public string FilePath
        {
            get { return _filepath; }
            set
            {
                _filepath = value;

                if (_filepath != null)
                {
                    Load();
                }
            }
        }

        public bool ShowClear
        {
            get { return _showclear; }
            set { _showclear = value; }
        }

        public int MaxItems
        {
            get { return _maxitems; }
            set { _maxitems = value; }
        }


        public event EventHandler OnItemClicked;

        /// <summary>
        /// Clears all recent items.
        /// </summary>
        public void Clear()
        {
            DropDownItems.Clear();
            var doc = new XmlDocument();
            doc.Load(_filepath);
            doc.GetElementsByTagName("recent")[0].RemoveAll();
            doc.Save(_filepath);
            Enabled = false;
        }

        public void Add(TabFile tab)
        {
            var doc = new XmlDocument();
            doc.Load(_filepath);

            var files = doc.GetElementsByTagName("recent")[0].ChildNodes;

            if (files.Count >= _maxitems)
            {

            }

            doc.Save(_filepath);
        }

        private void AddMenuItem(TabFile tab)
        {
            var item = new ToolStripMenuItem(string.Format("{0} - {1}", tab.TabData.Artist, tab.TabData.Title)) { ToolTipText = tab.FileInfo.FullName};
            item.Click += item_clicked;
            //menuItemMRU.DropDownItems.Add(item);
            ((ToolStripDropDownMenu) item.DropDown).ShowImageMargin = false;
            ((ToolStripDropDownMenu) item.DropDown).ShowCheckMargin = false;

            DropDownItems.Add(item);
        }

        private void Load()
        {
            if (_filepath == null || !File.Exists(_filepath))
                return;

            var doc = new XmlDocument();
            doc.Load(_filepath);
            var files = doc.GetElementsByTagName("recent")[0].ChildNodes;

            DropDownItems.Clear();

            if (files.Count > 0)
            {
                foreach (XmlNode file in files)
                {
                    TabFile tab;

                    if (TabFile.TryParse(file.InnerText, out tab))
                    {
                        AddMenuItem(tab);
                    }
                }


                if (_showclear)
                {
                    DropDownItems.Add(new ToolStripSeparator());
                    var clearMenuItem = new ToolStripMenuItem("Clear") {ToolTipText = "Clears all recent items."};
                    clearMenuItem.Click += ClearRecentItems;
                    DropDownItems.Add(clearMenuItem);
                }
            }

            if (DropDownItems.Count == 0)
            {
                Enabled = false;
            }
        }

        public void ClearRecentItems(object sender, EventArgs e)
        {
            Clear();
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