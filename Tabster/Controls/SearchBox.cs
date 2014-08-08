#region

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Tabster.Controls
{
    internal class SearchBox : ToolStripTextBox
    {
        #region Delegates

        public delegate void NewSearchHandler(object sender, string value);

        #endregion

        private const int _delayInterval = 250;

        private readonly Timer _delayTimer = new Timer();
        private ToolStripButton _clearButton;
        private string _previousSearch = "";

        public SearchBox()
        {
            _delayTimer.Interval = DelayInterval;
            _delayTimer.Tick += _delayTimer_Tick;
        }

        public string DefaultText { get; set; }
        public string SearchValue { get; private set; }

        public ToolStripButton ClearButton
        {
            get { return _clearButton; }
            set
            {
                _clearButton = value;
                ClearButton.Click -= ClearButton_Click;
                ClearButton.Click += ClearButton_Click;
            }
        }

        public bool IsFilterSet { get; private set; }
        public bool FilterReset { get; private set; }

        public int DelayInterval
        {
            get { return _delayInterval; }
        }

        public event NewSearchHandler OnNewSearch;

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Reset(false);
            RestoreDefault();
        }

        private void _delayTimer_Tick(object sender, EventArgs e)
        {
            _delayTimer.Stop();

            SearchValue = Text.Trim();

            if (OnNewSearch != null)
                OnNewSearch(this, Text.Trim());
        }

        private void RestoreDefault()
        {
            ForeColor = Color.DarkGray;
            Text = DefaultText;
        }

        public void Reset(bool silent)
        {
            Text = DefaultText;

            FilterReset = true;

            if (!silent && OnNewSearch != null)
                OnNewSearch(this, null);

            FilterReset = false;

            IsFilterSet = false;
        }

        protected override void OnLeave(EventArgs e)
        {
            if (!IsFilterSet)
            {
                ClearButton.Visible = false;
                RestoreDefault();
            }

            base.OnLeave(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            if (IsFilterSet)
            {
                SelectAll();
            }

            else
            {
                Clear();
                ForeColor = SystemColors.WindowText;
            }

            base.OnEnter(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            _delayTimer.Stop();

            var hasSearchableContents = false;

            var trimmed = Text.Trim();

            //cleared
            if (trimmed.Length == 0 && _previousSearch != "")
            {
                if (OnNewSearch != null)
                    OnNewSearch(this, null);
            }

            if (trimmed.Length > 0 && trimmed != DefaultText.Trim())
            {
                hasSearchableContents = true;
            }

            if (ClearButton != null)
                ClearButton.Visible = hasSearchableContents;

            if (hasSearchableContents)
                _delayTimer.Start();

            IsFilterSet = hasSearchableContents;
            _previousSearch = trimmed;

            base.OnTextChanged(e);
        }
    }
}