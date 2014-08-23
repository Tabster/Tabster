#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Tabster.Core.Types;

#endregion

namespace Tabster.Controls
{
    /// <summary>
    ///   Represents a dropdown list of TabType strings.
    /// </summary>
    [DefaultEvent("TypeChanged")]
    public partial class TabTypeDropdown : UserControl
    {
        private readonly List<TabType> types = new List<TabType>();
        private bool _controlLoaded;
        private bool _placeholderRemoved;

        /// <summary>
        ///   Initializes a new TabTypeDropdown instance.
        /// </summary>
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

            if (TypeChanged != null)
                TypeChanged(this, EventArgs.Empty);
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

        #region Events

        [Description("Raised when the type is changed.")]
        public event EventHandler TypeChanged;

        #endregion

        #region Properties

        private bool _displayPlaceholder;
        private string _placeholderText;

        private bool _usePluralizedNames;

        /// <summary>
        ///   Determines whether to display the placeholder text.
        /// </summary>
        [Browsable(false)]
        [Category("Data")]
        [Description("Determines whether to display the placeholder text.")]
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

        /// <summary>
        ///   Placeholder text.
        /// </summary>
        [Browsable(false)]
        [Category("Data")]
        [Description("Placeholder text.")]
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

        /// <summary>
        ///   Pluralizes tab type.
        /// </summary>
        [Browsable(false)]
        [Category("Data")]
        [Description("Pluralizes tab type.")]
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

        /// <summary>
        ///   Determines whether a tab t ype has been selected.
        /// </summary>
        [Browsable(false)]
        [Category("Data")]
        [Description("Determines whether a tab t ype has been selected.")]
        public bool HasTypeSelected
        {
            get { return DisplayPlaceholder ? comboBox1.SelectedIndex > 0 : comboBox1.SelectedIndex >= 0; }
        }

        /// <summary>
        ///   The currently selected tab type.
        /// </summary>
        [Browsable(true)]
        [Category("Data")]
        [Description("The currently selected tab type.")]
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