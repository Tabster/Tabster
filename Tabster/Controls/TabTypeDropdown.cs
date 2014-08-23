#region

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tabster.Core.Types;

#endregion

namespace Tabster.Controls
{
    public partial class TabTypeDropdown : UserControl
    {
        private readonly List<TabType> types = new List<TabType>();
        private bool _controlLoaded;
        private bool _placeholderRemoved;

        public TabTypeDropdown()
        {
            InitializeComponent();

            foreach (TabType type in Enum.GetValues(typeof (TabType)))
            {
                types.Add(type);
            }
        }

        private void PopulateList()
        {
            var selectedText = !DisplayPlaceholder || (DisplayPlaceholder && comboBox1.SelectedIndex > 0)
                                   ? comboBox1.SelectedText
                                   : null;

            comboBox1.Items.Clear();

            foreach (var type in types)
            {
                comboBox1.Items.Add(GetDisplayString(type));
            }

            if (DisplayPlaceholder)
                ShowPlaceholder(false);

            if (selectedText != null)
            {
                comboBox1.Text = selectedText;
            }

            else
            {
                if (DisplayPlaceholder)
                    comboBox1.Text = PlaceholderText;
                else
                    comboBox1.SelectedIndex = 0;
            }
        }

        private string GetDisplayString(TabType type)
        {
            var str = type.ToFriendlyString();

            if (UsePluralizedNames && !str.EndsWith("s"))
                str += "s";

            return str;
        }

        private int GetTypeIndex(TabType type)
        {
            return types.IndexOf(type);
        }

        private void SelectIndex(int index)
        {
            if (index >= 0 && index < comboBox1.Items.Count)
                comboBox1.SelectedIndex = index;
        }

        private void ShowPlaceholder(bool select)
        {
            comboBox1.Items.Insert(0, PlaceholderText);

            if (select)
                comboBox1.SelectedIndex = 0;
        }

        private void HidePlaceholder()
        {
            var selectedIndex = comboBox1.SelectedIndex;
            comboBox1.Items.RemoveAt(0);
            SelectIndex(--selectedIndex);
        }

        private void TabTypeDropdown_Load(object sender, EventArgs e)
        {
            _controlLoaded = true;

            PopulateList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DisplayPlaceholder && comboBox1.SelectedIndex > 0 && !_placeholderRemoved)
            {
                HidePlaceholder();
                _placeholderRemoved = true;
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            if (DisplayPlaceholder)
                HidePlaceholder();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            if (DisplayPlaceholder)
                ShowPlaceholder(comboBox1.SelectedIndex == -1);
        }

        #region Properties

        private bool _displayPlaceholder;
        private string _placeholderText;

        private bool _usePluralizedNames;

        public bool DisplayPlaceholder
        {
            get { return _displayPlaceholder; }
            set
            {
                _displayPlaceholder = value;
                if (_controlLoaded)
                    PopulateList();
            }
        }

        public string PlaceholderText
        {
            get { return _placeholderText; }
            set
            {
                _placeholderText = value;

                if (_controlLoaded)
                    PopulateList();
            }
        }

        public bool UsePluralizedNames
        {
            get { return _usePluralizedNames; }
            set
            {
                _usePluralizedNames = value;

                if (_controlLoaded)
                    PopulateList();
            }
        }

        public bool HasTypeSelected
        {
            get { return DisplayPlaceholder ? comboBox1.SelectedIndex > 0 : comboBox1.SelectedIndex >= 0; }
        }

        public TabType SelectedType
        {
            get { return types.Find(x => GetDisplayString(x) == comboBox1.SelectedText); }
            set
            {
                var index = GetTypeIndex(value);
                if (index >= 0 && index < comboBox1.Items.Count)
                    comboBox1.SelectedIndex = GetTypeIndex(value);
            }
        }

        #endregion
    }
}