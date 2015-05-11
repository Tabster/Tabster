#region

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Tabster.Core.Types;

#endregion

namespace Tabster.WinForms
{
    /// <summary>
    ///     Represents a dropdown list of rating strings.
    /// </summary>
    [DefaultEvent("RatingChanged")]
    public partial class TablatureRatingDropdown : UserControl
    {
        private bool _controlLoaded;

        /// <summary>
        ///     Initializes a new TablatureRatingDropdown instance.
        /// </summary>
        public TablatureRatingDropdown()
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

            foreach (TablatureRating rating in Enum.GetValues(typeof (TablatureRating)))
            {
                var str = GetDisplayString(rating);

                if (!string.IsNullOrEmpty(str))
                    comboBox1.Items.Add(str);
            }

            comboBox1.SelectedIndex = 0;
        }

        private static string GetDisplayString(TablatureRating rating)
        {
            var num = rating.ToInt();
            const char c = '★';
            return rating == TablatureRating.None ? null : new string(c, num);
        }

        private int GetRatingIndex(TablatureRating rating)
        {
            var displayString = GetDisplayString(rating);

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

        private TablatureRating GetSelectedRating()
        {
            var text = comboBox1.Text;

            foreach (TablatureRating rating in Enum.GetValues(typeof (TablatureRating)))
            {
                var displayString = GetDisplayString(rating);

                if (text == displayString)
                    return rating;
            }

            return default(TablatureRating);
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

            if (RatingChanged != null)
                RatingChanged(this, EventArgs.Empty);
        }

        #region Events

        /// <summary>
        ///     Raised when the rating is changed.
        /// </summary>
        [Description("Raised when the rating is changed.")]
        public event EventHandler RatingChanged;

        #endregion

        #region Properties

        private string _defaultText;
        private bool _displayDefault;

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
        ///     The currently selected rating.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when the default text is selected.</exception>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Data")]
        [Description("The currently selected rating.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TablatureRating SelectedRating
        {
            get { return GetSelectedRating(); }
            set { comboBox1.SelectedIndex = GetRatingIndex(value); }
        }

        #endregion
    }
}