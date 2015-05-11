#region

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Tabster.Core.Types;

#endregion

namespace Tabster.WinForms
{
    /// <summary>
    ///     Represents a dropdown list of TabType strings.
    /// </summary>
    [DefaultEvent("TypeChanged")]
    public partial class TabTypeDropdown : UserControl
    {
        private bool _controlLoaded;

        /// <summary>
        ///     Initializes a new TabTypeDropdown instance.
        /// </summary>
        public TabTypeDropdown()
        {
            InitializeComponent();
            PopulateList();
        }

        /// <summary>
        ///     Selects the default text option.
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
            {
                comboBox1.Items.Add(DefaultText);
            }

            var types = _nativeTypesOnly ? TablatureType.GetNativeTypes() : TablatureType.GetKnownTypes();

            foreach (var type in types)
            {
                comboBox1.Items.Add(GetDisplayString(type));
            }

            comboBox1.SelectedIndex = 0;
        }

        private string GetDisplayString(TablatureType type)
        {
            if (type == null)
                return string.Empty;

            var str = type.ToFriendlyString();

            if (UsePluralizedNames && !str.EndsWith("s"))
                str += "s";

            return str;
        }

        private int GetTypeIndex(TablatureType type)
        {
            var displayString = GetDisplayString(type);

            for (var i = 0; i < comboBox1.Items.Count; i++)
            {
                var item = comboBox1.Items[i];

                if (item.ToString() == displayString)
                {
                    return i;
                }
            }

            return 0;
        }

        private TablatureType GetSelectedType()
        {
            if (!HasTypeSelected)
                throw new InvalidOperationException("No suitable type is selected.");

            var text = comboBox1.Text;

            foreach (var type in TablatureType.GetKnownTypes())
            {
                var displayString = GetDisplayString(type);

                if (text == displayString)
                    return type;
            }

            return default(TablatureType);
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
        ///     Raised when the type is changed.
        /// </summary>
        [Description("Raised when the type is changed.")]
        public event EventHandler TypeChanged;

        #endregion

        #region Properties

        private string _defaultText;
        private bool _displayDefault;
        private bool _nativeTypesOnly;
        private bool _usePluralizedNames;

        /// <summary>
        ///     Determines whether to display the default text.
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

                PopulateList();
            }
        }

        /// <summary>
        ///     Default text.
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

                PopulateList();
            }
        }

        /// <summary>
        ///     Pluralizes tab type.
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

                PopulateList();
            }
        }

        /// <summary>
        ///     Determines whether a tab t ype has been selected.
        /// </summary>
        [Browsable(false)]
        [Category("Data")]
        [Description("Determines whether a tab type has been selected.")]
        public bool HasTypeSelected
        {
            get { return DisplayDefault ? comboBox1.SelectedIndex > 0 : comboBox1.SelectedIndex >= 0; }
        }

        /// <summary>
        ///     The currently selected tab type.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when the default text is selected.</exception>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Data")]
        [Description("The currently selected tab type.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TablatureType SelectedType
        {
            get { return GetSelectedType(); }
            set { comboBox1.SelectedIndex = GetTypeIndex(value); }
        }

        /// <summary>
        ///     Only show natively-supported types.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Data")]
        [Description("Only show natively-supported types.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NativeTypesOnly
        {
            get { return _nativeTypesOnly; }
            set
            {
                _nativeTypesOnly = value;

                PopulateList();
            }
        }

        #endregion
    }
}