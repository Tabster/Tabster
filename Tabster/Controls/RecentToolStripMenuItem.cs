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
        private readonly List<RecentMenuItem> _items = new List<RecentMenuItem>();
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

        public ReadOnlyCollection<RecentMenuItem> Items
        {
            get { return _items.AsReadOnly(); }
        }

        public bool DisplayClearOption
        {
            get { return _displayClearOption; }
            set
            {
                _displayClearOption = value;
                PopulateItems();
            }
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

        public bool DisplayOpenAllOption
        {
            get { return _displayOpenAllOption; }
            set
            {
                _displayOpenAllOption = value;
                PopulateItems();
            }
        }

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
            set
            {
                _displayMode = value;
                PopulateItems();
            }
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
            set
            {
                _prependItemNumbers = value;
                PopulateItems();
            }
        }

        public event EventHandler OnItemClicked;
        public event EventHandler OnClearItemClicked;
        public event EventHandler OnAllItemsOpened;

        public void Add(RecentMenuItem menuItem)
        {
            if (!menuItem.FileInfo.Exists)
                return;

            Remove(menuItem, false);

            if (_items.Count == _maxDisplayItems)
                _items.RemoveAt(_items.Count - 1);

            _items.Insert(0, menuItem);

            menuItem.Click += (s, e) =>
            {
                if (OnItemClicked != null)
                    OnItemClicked(s, e);
            };

            PopulateItems();
        }

        public void Remove(RecentMenuItem menuItem, bool repopulateDisplayItems = true)
        {
            _items.Remove(menuItem);
            _items.RemoveAll(x => x.FileInfo.FullName.Equals(menuItem.FileInfo.FullName, StringComparison.OrdinalIgnoreCase));

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

        private void PopulateItems()
        {
            // update item numbers
            for (var i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                var itemText = string.IsNullOrEmpty(item.DisplayText) ? item.FileInfo.FullName : item.DisplayText;
                item.Text = PrependItemNumbers ? string.Format("{0}: {1}", i + 1, itemText) : itemText;
            }

            ClearChildItems();
            ClearConsecutiveItems();

            if (DisplayMode == RecentFilesDisplayMode.Child)
                PopulateChildItems();

            if (DisplayMode == RecentFilesDisplayMode.Consecutive)
                PopulateConsecutiveItems();
        }

        #region RecentFilesDisplayMode.Child Methods

        private void PopulateChildItems()
        {
            if (_items.Count == 0)
                return;

            for (var i = 0; i < _items.Count; i++)
            {
                if (i == MaxDisplayItems)
                    break;

                PopulateChildItem(_items[i]);
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

        private readonly List<int> _consecutiveItemIndexes = new List<int>();
        private bool _displayOpenAllOption;

        private void PopulateConsecutiveItems()
        {
            Visible = false;

            if (_items.Count == 0)
                return;

            var itemIndex = GetCurrentIndex();

            PopulateConsecutiveItem(itemIndex, new MenuItemSeperator());
            itemIndex++;

            foreach (var item in _items)
            {
                if (itemIndex == MaxDisplayItems)
                    break;

                PopulateConsecutiveItem(itemIndex, item);
                itemIndex++;
            }

            if (DisplayOpenAllOption || DisplayClearOption)
            {
                PopulateConsecutiveItem(itemIndex, new MenuItemSeperator());
                itemIndex++;
            }

            if (DisplayOpenAllOption)
            {
                PopulateConsecutiveItem(itemIndex, _openAllMenuItem);
                itemIndex++;
            }

            if (DisplayClearOption)
            {
                PopulateConsecutiveItem(itemIndex, _clearMenuItem);
                itemIndex++;
            }

            PopulateConsecutiveItem(itemIndex, new MenuItemSeperator());
        }

        private void PopulateConsecutiveItem(int index, MenuItem item)
        {
            Parent.MenuItems.Add(index, item);
            _consecutiveItemIndexes.Add(index);
        }

        private void ClearConsecutiveItems()
        {
            var owner = (Parent as MenuItem);
            _consecutiveItemIndexes.Reverse();

            foreach (var index in _consecutiveItemIndexes)
            {
                owner.MenuItems.RemoveAt(index);
            }

            _consecutiveItemIndexes.Clear();
        }

        private int GetCurrentIndex()
        {
            return Parent.MenuItems.IndexOf(this);
        }

        #endregion

        /// <summary>
        ///     Represents an individual menu item separator.
        /// </summary>
        internal class MenuItemSeperator : MenuItem
        {
            public MenuItemSeperator()
            {
                Text = "-";
            }
        }

        /// <summary>
        ///     Represents an individual recent menu item.
        /// </summary>
        public class RecentMenuItem : MenuItem
        {
            /// <summary>
            ///     Initializes a new RecentMenuItem.
            /// </summary>
            /// <param name="fileInfo">The respective FileInfo for the menu item.</param>
            public RecentMenuItem(FileInfo fileInfo)
            {
                FileInfo = fileInfo;
            }

            /// <summary>
            ///     Respective file info reference.
            /// </summary>
            public FileInfo FileInfo { get; private set; }

            /// <summary>
            ///     Text to display on menu item.
            /// </summary>
            public string DisplayText { get; set; }
        }

        #region RecentFilesDisplayMode enum

        /// <summary>
        ///     Display mode for menu item.
        /// </summary>
        public enum RecentFilesDisplayMode
        {
            /// <summary>
            ///     Items are displayed using a parent/child model.
            /// </summary>
            Child,

            /// <summary>
            ///     Iteams are displayed consecutively, being appended after the parent item.
            /// </summary>
            Consecutive
        }

        #endregion
    }
}