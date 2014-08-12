#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;

#endregion

namespace Tabster.Controls
{
    internal class RecentToolStripMenuItem : ToolStripMenuItem
    {
        #region RecentFilesDisplayMode enum

        public enum RecentFilesDisplayMode
        {
            Child,
            Consecutive
        }

        #endregion

        private readonly ToolStripMenuItem _clearMenuItem = new ToolStripMenuItem();

        private readonly List<ToolStripItem> _consecutiveItems = new List<ToolStripItem>();
        private readonly List<RecentToolStripMenuElement> _items = new List<RecentToolStripMenuElement>();
        private readonly ToolStripMenuItem _openAllMenuItem = new ToolStripMenuItem();

        private string _clearOptionText = "Clear All Recent Items";

        private bool _displayClearOption = true;
        private RecentFilesDisplayMode _displayMode = RecentFilesDisplayMode.Child;
        private int _maxDisplayItems = 10;
        private string _openAllOptionText = "Open All Recent Items";
        private bool _prependItemNumbers = true;

        public RecentToolStripMenuItem()
        {
            _openAllMenuItem.Text = OpenAllOptionText;

            _openAllMenuItem.Click += delegate
                                          {
                                              if (OnAllItemsOpened != null)
                                                  OnAllItemsOpened(this, EventArgs.Empty);
                                          };

            _clearMenuItem.Text = ClearOptionText;
            _clearMenuItem.Click += delegate { Clear(); };
        }

        public ReadOnlyCollection<RecentToolStripMenuElement> Items
        {
            get { return _items.AsReadOnly(); }
        }

        public bool DisplayClearOption
        {
            get { return _displayClearOption; }
            set { _displayClearOption = value; }
        }

        public string ClearOptionText
        {
            get { return _clearOptionText; }
            set
            {
                _clearOptionText = value;
                _clearMenuItem.Text = value;
            }
        }

        public bool DisplayOpenAllOption { get; set; }

        public string OpenAllOptionText
        {
            get { return _openAllOptionText; }
            set
            {
                _openAllOptionText = value;
                _openAllMenuItem.Text = value;
            }
        }

        public RecentFilesDisplayMode DisplayMode
        {
            get { return _displayMode; }
            set { _displayMode = value; }
        }

        public int MaxDisplayItems
        {
            get { return _maxDisplayItems; }
            set { _maxDisplayItems = value; }
        }

        public bool PrependItemNumbers
        {
            get { return _prependItemNumbers; }
            set { _prependItemNumbers = value; }
        }

        public event EventHandler OnItemClicked;
        public event EventHandler OnItemsCleared;
        public event EventHandler OnAllItemsOpened;

        public void Add(FileInfo file, string displayName = null, bool repopulateDisplayItems = true)
        {
            Remove(file);
            _items.Insert(0, new RecentToolStripMenuElement(file, displayName));

            if (repopulateDisplayItems)
                PopulateItems();
        }

        public void Remove(FileInfo file)
        {
            _items.RemoveAll(x => x.File.FullName.Equals(file.FullName, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Clear()
        {
            _items.Clear();

            if (DisplayMode == RecentFilesDisplayMode.Child)
            {
                ClearChildItems();
            }

            if (DisplayMode == RecentFilesDisplayMode.Consecutive)
            {
                ClearConsecutiveItems();
            }

            if (OnItemsCleared != null)
            {
                OnItemsCleared(this, EventArgs.Empty);
            }
        }

        public bool Exists(FileInfo file, out int index)
        {
            for (var i = 0; i < _items.Count; i++)
            {
                var f = _items[i].File;

                if (f.FullName.Equals(file.FullName, StringComparison.InvariantCultureIgnoreCase))
                {
                    index = i;
                    return true;
                }
            }

            index = -1;
            return false;
        }

        private void PopulateItems()
        {
            var menuItems = new List<ToolStripItem>();

            for (var i = 0; i < _items.Count; i++)
            {
                if (i == MaxDisplayItems)
                    break;

                var item = _items[i];

                var displayName = string.IsNullOrEmpty(item.DisplayName) ? item.File.FullName : item.DisplayName;

                if (PrependItemNumbers)
                    displayName = string.Format("{0}: {1}", i + 1, displayName);

                var useTooltip = !displayName.Equals(item.File.FullName, StringComparison.InvariantCultureIgnoreCase);

                var menuItem = new ToolStripMenuItem(displayName) { Tag = item.File.FullName, ToolTipText = item.File.FullName, AutoToolTip = useTooltip };

                menuItem.Click += (s, e) =>
                                      {
                                          if (OnItemClicked != null)
                                              OnItemClicked(s, e);
                                      };

                ((ToolStripDropDownMenu) menuItem.DropDown).ShowImageMargin = false;
                ((ToolStripDropDownMenu) menuItem.DropDown).ShowCheckMargin = false;

                item.MenuItem = menuItem;

                menuItems.Insert(0, menuItem);
            }

            //clear existing items
            ClearChildItems();
            ClearConsecutiveItems();

            if (DisplayMode == RecentFilesDisplayMode.Child)
            {
                PopulateChildItems(menuItems);
            }

            if (DisplayMode == RecentFilesDisplayMode.Consecutive)
            {
                PopulateConsecutiveItems(menuItems);
            }
        }

        #region RecentFilesDisplayMode.Child Methods

        private void PopulateChildItems(IEnumerable<ToolStripItem> items)
        {
            foreach (var item in items)
            {
                PopulateChildItem(item);
            }

            if (DisplayOpenAllOption || DisplayClearOption)
            {
                DropDownItems.Add(new ToolStripSeparator());
            }

            if (DisplayOpenAllOption)
            {
                DropDownItems.Add(_openAllMenuItem);
            }

            if (DisplayClearOption)
            {
                DropDownItems.Add(_clearMenuItem);
            }
        }

        private void PopulateChildItem(ToolStripItem item)
        {
            Visible = true;
            DropDownItems.Insert(0, item);
            Enabled = DropDownItems.Count > 0;
        }

        private void ClearChildItems()
        {
            DropDownItems.Clear();
            Enabled = false;
        }

        #endregion

        #region RecentFilesDisplayMode.Consecutive Methods

        private void PopulateConsecutiveItems(IEnumerable<ToolStripItem> items)
        {
            Visible = false;
            var index = ((ToolStripMenuItem)OwnerItem).DropDownItems.IndexOf(this);

            var count = index;

            PopulateConsecutiveItem(index, new ToolStripSeparator());

            foreach (var item in items)
            {
                PopulateConsecutiveItem(index, item);
                count++;
            }

            if (DisplayOpenAllOption || DisplayClearOption)
            {
                PopulateConsecutiveItem(count, new ToolStripSeparator());
                count++;
            }

            if (DisplayOpenAllOption)
            {
                PopulateConsecutiveItem(count, _openAllMenuItem);
                count++;
            }

            if (DisplayClearOption)
            {
                PopulateConsecutiveItem(count, _clearMenuItem);
                count++;
            }

            PopulateConsecutiveItem(index, new ToolStripSeparator());
        }

        private void PopulateConsecutiveItem(int index, ToolStripItem item)
        {
            ((ToolStripMenuItem) OwnerItem).DropDownItems.Insert(index, item);
            _consecutiveItems.Add(item);
        }

        private void ClearConsecutiveItems()
        {
            var owner = (OwnerItem as ToolStripMenuItem);

            foreach (var item in _consecutiveItems)
            {
                owner.DropDownItems.Remove(item);
            }

            _consecutiveItems.Clear();
        }

        #endregion

        #region Nested type: RecentToolStripMenuElement

        public class RecentToolStripMenuElement
        {
            public RecentToolStripMenuElement(FileInfo file, string displayName)
            {
                File = file;
                DisplayName = displayName;
            }

            public FileInfo File { get; private set; }
            public string DisplayName { get; private set; }
            public ToolStripMenuItem MenuItem { get; set; }
        }

        #endregion
    }
}