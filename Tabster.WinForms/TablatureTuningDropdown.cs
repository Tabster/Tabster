#region

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Tabster.Core.Types;

#endregion

namespace Tabster.WinForms
{
    /// <summary>
    ///     Represents a dropdown list of TabTuning strings.
    /// </summary>
    [DefaultEvent("TuningChanged")]
    public partial class TablatureTuningDropdown : UserControl
    {
        private bool _controlLoaded;

        /// <summary>
        ///     Initializes a new TabTuningDropdown instance.
        /// </summary>
        public TablatureTuningDropdown()
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

            var tunings = _nativeTuningsOnly ? TablatureTuning.GetNativeTunings() : TablatureTuning.GetKnownTunings();

            foreach (var tuning in tunings)
            {
                comboBox1.Items.Add(GetDisplayString(tuning));
            }

            comboBox1.SelectedIndex = 0;
        }

        private static string GetDisplayString(TablatureTuning tuning)
        {
            return tuning == null ? string.Empty : tuning.ToFriendlyString();
        }

        private int GetTuningIndex(TablatureTuning tuning)
        {
            var displayString = GetDisplayString(tuning);

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

        private TablatureTuning GetSelectedTuning()
        {
            if (!HasTuningSelected)
                return TablatureTuning.Undefined;

            var text = comboBox1.Text;

            foreach (var tuning in TablatureTuning.GetKnownTunings())
            {
                var displayString = GetDisplayString(tuning);

                if (text == displayString)
                    return tuning;
            }

            return default(TablatureTuning);
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

            if (TuningChanged != null)
                TuningChanged(this, EventArgs.Empty);
        }

        #region Events

        /// <summary>
        ///     Raised when the tuning is changed.
        /// </summary>
        [Description("Raised when the tuning is changed.")]
        public event EventHandler TuningChanged;

        #endregion

        #region Properties

        private string _defaultText;
        private bool _displayDefault;
        private bool _nativeTuningsOnly;

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
        ///     Determines whether a tab t ype has been selected.
        /// </summary>
        [Browsable(false)]
        [Category("Data")]
        [Description("Determines whether a tab tuning has been selected.")]
        public bool HasTuningSelected
        {
            get { return DisplayDefault ? comboBox1.SelectedIndex > 0 : comboBox1.SelectedIndex >= 0; }
        }

        /// <summary>
        ///     The currently selected tab tuning.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when the default text is selected.</exception>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Data")]
        [Description("The currently selected tab tuning.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TablatureTuning SelectedTuning
        {
            get { return GetSelectedTuning(); }
            set { comboBox1.SelectedIndex = GetTuningIndex(value); }
        }

        /// <summary>
        ///     Only show natively-supported tunings.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Data")]
        [Description("Only show natively-supported tunings.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NativeTuningsOnly
        {
            get { return _nativeTuningsOnly; }
            set
            {
                _nativeTuningsOnly = value;

                PopulateList();
            }
        }

        #endregion
    }
}