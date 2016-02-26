#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;

#endregion

namespace Tabster.Controls
{
    internal class RecentToolStripMenuItem : MenuItem
    {
        private readonly MenuItem _clearMenuItem = new MenuItem();

        private readonly List<MenuItem> _consecutiveItems = new List<MenuItem>();
        private readonly List<RecentToolStripMenuElement> _items = new List<RecentToolStripMenuElement>();
        private readonly MenuItem _openAllMenuItem = new MenuItem();

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
            _clearMenuItem.Click += delegate
            {
                Clear();

                if (OnClearItemClicked != null)
                    OnClearItemClicked(this, EventArgs.Empty);
            };
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
            set
            {
                _maxDisplayItems = value;

                if (_items.Count > _maxDisplayItems)
                    _items.RemoveRange(_maxDisplayItems, _items.Count - _maxDisplayItems);

                PopulateItems();
            }
        }

        public bool PrependItemNumbers
        {
            get { return _prependItemNumbers; }
            set { _prependItemNumbers = value; }
        }

        public event EventHandler OnItemClicked;
        public event EventHandler OnClearItemClicked;
        public event EventHandler OnAllItemsOpened;

        public void Add(FileInfo file, string displayName = null)
        {
            if (!file.Exists)
                return;

            Remove(file, false);

            if (_items.Count == _maxDisplayItems)
                _items.RemoveAt(_items.Count - 1);

            _items.Insert(0, new RecentToolStripMenuElement(file, displayName));

            PopulateItems();
        }

        public void Remove(FileInfo file, bool repopulateDisplayItems = true)
        {
            _items.RemoveAll(x => x.File.FullName.Equals(file.FullName, StringComparison.OrdinalIgnoreCase));
            
            if (repopulateDisplayItems)
                PopulateItems();
        }

        public void Clear()
        {
            _items.Clear();

            if (DisplayMode == RecentFilesDisplayMode.Child)
                ClearChildItems();

            if (DisplayMode == RecentFilesDisplayMode.Consecutive)
                ClearConsecutiveItems();
        }

        public bool Exists(FileInfo file, out int index)
        {
            for (var i = 0; i < _items.Count; i++)
            {
                var f = _items[i].File;

                if (f.FullName.Equals(file.FullName, StringComparison.OrdinalIgnoreCase))
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
            var menuItems = new List<MenuItem>();

            for (var i = 0; i < _items.Count; i++)
            {
                if (i == MaxDisplayItems)
                    break;

                var item = _items[i];

                var displayName = string.IsNullOrEmpty(item.DisplayName) ? item.File.FullName : item.DisplayName;

                if (PrependItemNumbers)
                    displayName = string.Format("{0}: {1}", i + 1, displayName);

                var menuItem = new MenuItem(displayName) {Tag = item.File.FullName};

                menuItem.Click += (s, e) =>
                {
                    if (OnItemClicked != null)
                        OnItemClicked(s, e);
                };

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

        private void PopulateChildItems(IEnumerable<MenuItem> items)
        {
            foreach (var item in items)
            {
                PopulateChildItem(item);
            }

            if (DisplayOpenAllOption || DisplayClearOption)
                MenuItems.Add(new MenuItemSeperator());

            if (DisplayOpenAllOption)
                MenuItems.Add(_openAllMenuItem);

            if (DisplayClearOption)
                MenuItems.Add(_clearMenuItem);
        }

        private void PopulateChildItem(MenuItem item)
        {
            Visible = true;
            MenuItems.Add(0, item);
            Enabled = MenuItems.Count > 0;
        }

        private void ClearChildItems()
        {
            MenuItems.Clear();
            Enabled = false;
        }

        #endregion

        #region RecentFilesDisplayMode.Consecutive Methods

        private void PopulateConsecutiveItems(IEnumerable<MenuItem> items)
        {
            Visible = false;

            var index = Parent.MenuItems.IndexOf(this);

            var count = index;

            PopulateConsecutiveItem(index, new MenuItemSeperator());

            foreach (var item in items)
            {
                PopulateConsecutiveItem(index, item);
                count++;
            }

            if (DisplayOpenAllOption || DisplayClearOption)
            {
                PopulateConsecutiveItem(count, new MenuItemSeperator());
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

            PopulateConsecutiveItem(index, new MenuItemSeperator());
        }

        private void PopulateConsecutiveItem(int index, MenuItem item)
        {
            Parent.MenuItems.Add(index, item);
            _consecutiveItems.Add(item);
        }

        private void ClearConsecutiveItems()
        {
            var owner = (Parent as MenuItem);

            foreach (var item in _consecutiveItems)
            {
                owner.MenuItems.Remove(item);
            }

            _consecutiveItems.Clear();
        }

        #endregion

        #region Nested type: RecentToolStripMenuElement

        internal class RecentToolStripMenuElement
        {
            public RecentToolStripMenuElement(FileInfo file, string displayName)
            {
                File = file;
                DisplayName = displayName;
            }

            public FileInfo File { get; private set; }
            public string DisplayName { get; private set; }
            public MenuItem MenuItem { get; set; }
        }

        #endregion

        internal class MenuItemSeperator : MenuItem
        {
            public MenuItemSeperator()
            {
                Text = "-";
            }
        }

        #region RecentFilesDisplayMode enum

        public enum RecentFilesDisplayMode
        {
            Child,
            Consecutive
        }

        #endregion
    }
}