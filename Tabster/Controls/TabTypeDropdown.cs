#region

using System;
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
        private bool _controlLoaded;

        /// <summary>
        ///   Initializes a new TabTypeDropdown instance.
        /// </summary>
        public TabTypeDropdown()
        {
            InitializeComponent();
            PopulateList();
        }

        /// <summary>
        /// Selects the default text option.
        /// </summary>
        public void SelectDefault()
        {
            if (DisplayDefault)
                comboBox1.SelectedIndex = 0;
        }

        private void PopulateList()
        {
            comboBox1.Items.Clear();

            if (DisplayDefault)
                comboBox1.Items.Add(DefaultText);

            foreach (TabType type in Enum.GetValues(typeof (TabType)))
            {
                comboBox1.Items.Add(GetDisplayString(type));
            }

            comboBox1.SelectedIndex = 0;
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
            var str = GetDisplayString(type);

            for (var i = 0; i < comboBox1.Items.Count; i++)
            {
                var item = comboBox1.Items[i];

                if (item.ToString() == str)
                {
                    return i;
                }
            }

            return -1;
        }

        protected override void OnLoad(EventArgs e)
        {
            _controlLoaded = true;
            base.OnLoad(e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_controlLoaded)
                return;

            if (DisplayDefault && comboBox1.SelectedIndex == 0)
                return;

            if (TypeChanged != null)
                TypeChanged(this, EventArgs.Empty);
        }

        #region Events

        /// <summary>
        /// Raised when the type is changed.
        /// </summary>
        [Description("Raised when the type is changed.")]
        public event EventHandler TypeChanged;

        #endregion

        #region Properties

        private string _defaultText;
        private bool _displayDefault;
        private TabType _selectedType;

        private bool _usePluralizedNames;

        /// <summary>
        ///   Determines whether to display the default text.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Determines whether to display the default text.")]
        public bool DisplayDefault
        {
            get { return _displayDefault; }
            set
            {
                _displayDefault = value;

                if (_controlLoaded)
                    PopulateList();
            }
        }

        /// <summary>
        ///   Default text.
        /// </summary>
        [Browsable(true)]
        [Category("Data")]
        [Description("Default text.")]
        public string DefaultText
        {
            get { return _defaultText; }
            set
            {
                _defaultText = value;

                if (_controlLoaded)
                    PopulateList();
            }
        }

        /// <summary>
        ///   Pluralizes tab type.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
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
        [Description("Determines whether a tab type has been selected.")]
        public bool HasTypeSelected
        {
            get { return DisplayDefault ? comboBox1.SelectedIndex > 0 : comboBox1.SelectedIndex >= 0; }
        }

        /// <summary>
        ///   The currently selected tab type.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when the default text is selected.</exception>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Data")]
        [Description("The currently selected tab type.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabType SelectedType
        {
            get
            {
                if (!HasTypeSelected)
                    throw new InvalidOperationException("No suitable type is selected.");
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                comboBox1.SelectedIndex = GetTypeIndex(value);
            }
        }

        #endregion
    }
}