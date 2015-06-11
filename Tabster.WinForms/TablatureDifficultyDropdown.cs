#region

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Tabster.Core.Types;

#endregion

namespace Tabster.WinForms
{
    /// <summary>
    ///     Represents a dropdown list of TabDifficulty strings.
    /// </summary>
    [DefaultEvent("DifficultyChanged")]
    public partial class TablatureDifficultyDropdown : UserControl
    {
        private bool _controlLoaded;

        /// <summary>
        ///     Initializes a new TabDifficultyDropdown instance.
        /// </summary>
        public TablatureDifficultyDropdown()
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

            var difficultys = _nativeDifficultysOnly ? TablatureDifficulty.GetNativeDifficulties() : TablatureDifficulty.GetKnownDifficulties();

            foreach (var difficulty in difficultys)
            {
                comboBox1.Items.Add(GetDisplayString(difficulty));
            }

            comboBox1.SelectedIndex = 0;
        }

        private static string GetDisplayString(TablatureDifficulty difficulty)
        {
            return difficulty == null ? string.Empty : difficulty.ToFriendlyString();
        }

        private int GetDifficultyIndex(TablatureDifficulty difficulty)
        {
            var displayString = GetDisplayString(difficulty);

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

        private TablatureDifficulty GetSelectedDifficulty()
        {
            if (!HasDifficultySelected)
                return TablatureDifficulty.Undefined;

            var text = comboBox1.Text;

            foreach (var difficulty in TablatureDifficulty.GetKnownDifficulties())
            {
                var displayString = GetDisplayString(difficulty);

                if (text == displayString)
                    return difficulty;
            }

            return default(TablatureDifficulty);
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

            if (DifficultyChanged != null)
                DifficultyChanged(this, EventArgs.Empty);
        }

        #region Events

        /// <summary>
        ///     Raised when the difficulty is changed.
        /// </summary>
        [Description("Raised when the difficulty is changed.")]
        public event EventHandler DifficultyChanged;

        #endregion

        #region Properties

        private string _defaultText;
        private bool _displayDefault;
        private bool _nativeDifficultysOnly;

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
        [Description("Determines whether a tab difficulty has been selected.")]
        public bool HasDifficultySelected
        {
            get { return DisplayDefault ? comboBox1.SelectedIndex > 0 : comboBox1.SelectedIndex >= 0; }
        }

        /// <summary>
        ///     The currently selected tab difficulty.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when the default text is selected.</exception>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Data")]
        [Description("The currently selected tab difficulty.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TablatureDifficulty SelectedDifficulty
        {
            get { return GetSelectedDifficulty(); }
            set { comboBox1.SelectedIndex = GetDifficultyIndex(value); }
        }

        /// <summary>
        ///     Only show natively-supported difficultys.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Data")]
        [Description("Only show natively-supported difficultys.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NativeDifficultysOnly
        {
            get { return _nativeDifficultysOnly; }
            set
            {
                _nativeDifficultysOnly = value;

                PopulateList();
            }
        }

        #endregion
    }
}