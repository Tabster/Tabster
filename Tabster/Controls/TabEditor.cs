#region

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Tabster.Controls
{
    public partial class TabEditor : UserControl
    {
        #region AutoScrollSpeed enum

        public enum AutoScrollSpeed
        {
            Off = 0,
            Slow = 1,
            Medium = 2,
            Fast = 4,
        }

        #endregion

        #region TabMode enum

        public enum TabMode
        {
            Edit,
            View
        }

        #endregion

        #region Constructor

        public TabEditor()
        {
            InitializeComponent();
            _scrollTimer.Tick += _scrollTimer_Tick;
        }

        #endregion

        #region Properties

        private Color _backcolor = Color.White;
        private Color _forecolor = Color.Black;
        private TabMode _mode = TabMode.View;

        private AutoScrollSpeed _scrollSpeed;

        public AutoScrollSpeed ScrollSpeed
        {
            get { return _scrollSpeed; }
            set
            {
                BeginAutoScroll(value);
                _scrollSpeed = value;
            }
        }

        public TabMode Mode
        {
            get { return _mode; }
            set
            {
                if (value != _mode)
                {
                    _mode = value;

                    if (_mode == TabMode.Edit)
                    {
                        textBox1.Enabled = true;
                        textBox1.BringToFront();
                    }

                    else
                    {
                        textBox1.Enabled = false;
                        webBrowser1.BringToFront();
                    }

                    if (ModeChanged != null)
                        ModeChanged(this, EventArgs.Empty);
                }
            }
        }

        public new Color ForeColor
        {
            get { return _forecolor; }
            set
            {
                _forecolor = value;

                if (TabData != null)
                    ReloadHTML();
            }
        }

        public new Color BackColor
        {
            get { return _backcolor; }
            set
            {
                _backcolor = value;

                if (TabData != null)
                    ReloadHTML();
            }
        }

        public Tab TabData { get; private set; }

        public bool HasBeenModified { get; private set; }

        #endregion

        #region Events

        private string _oldContents = "";
        private string _originalContents = "";
        public event EventHandler ModeChanged;
        public event EventHandler TabLoaded;
        public event EventHandler TabModified;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != _oldContents.Length && textBox1.Text != _oldContents && textBox1.Text != _originalContents)
            {
                HasBeenModified = true;

                _oldContents = textBox1.Text;

                TabData.Contents = textBox1.Text;

                if (TabModified != null)
                    TabModified(this, EventArgs.Empty);
            }

            else
            {
                HasBeenModified = false;
            }
        }

        #endregion

        #region Methods

        public void SetDocumentText(string text)
        {
            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.OpenNew(true);
                webBrowser1.Document.Write(text);
            }

            else
            {
                webBrowser1.DocumentText = text;
            }
        }

        public void SwitchMode()
        {
            Mode = Mode == TabMode.Edit ? TabMode.View : TabMode.Edit;
        }

        public void LoadTab(Tab t)
        {
            TabData = t;

            if (TabData != null)
            {
                _originalContents = TabData.Contents;
                textBox1.Text = TabData.Contents;
                ReloadHTML();
            }

            if (TabLoaded != null)
                TabLoaded(this, EventArgs.Empty);
        }

        private void _scrollTimer_Tick(object sender, EventArgs e)
        {
            var height = webBrowser1.Document.Body.ScrollRectangle.Height;
            var multiplier = (int) ScrollSpeed;
            var skip = (multiplier*height)/100;
            webBrowser1.Document.Window.ScrollTo(0, webBrowser1.Document.Body.ScrollTop + skip);
        }

        public void BeginAutoScroll(AutoScrollSpeed speed)
        {
            if (webBrowser1.Document != null)
            {
                if (speed == AutoScrollSpeed.Off)
                {
                    _scrollTimer.Stop();
                }

                else
                {
                    _scrollTimer.Stop();
                    _scrollTimer.Interval = 1000;
                    _scrollTimer.Start();
                }
            }
        }

        private void ReloadHTML()
        {
            /*
            var forecolorHTML = ColorTranslator.ToHtml(ForeColor);
            var backcolorHTML = ColorTranslator.ToHtml(BackColor);

            //var forecolorString = forecolorHTML.Length == 6 ? string.Format("#{0}", forecolorHTML) : forecolorHTML;
            //var backcolorString = backcolorHTML.Length == 6 ? string.Format("#{0}", backcolorHTML) : backcolorHTML;

            /*
            Console.WriteLine("Forecolor: " + forecolorString);
            Console.WriteLine("Backcolor: " + backcolorString);
            */

            var html = tabHTML.Replace("{TAB_CONTENTS}", TabData.Contents);
            SetDocumentText(html);
            Mode = TabMode.View;
        }

        public void Print()
        {
            webBrowser1.Print();
        }

        public void ShowPrintDialog()
        {
            webBrowser1.ShowPrintDialog();
        }

        #endregion

        private const string tabHTML =
            @"
                                         <html>
                                            <head>
                                                <style type=""text/css"">
                                                    pre {
                                                        color: Black; 
                                                        background-color: White;
                                                        font: 12px Courier New,Courier, monospace;
                                                    }
                                                </style>
                                                <script type=""text/javascript"">
                                                    function pageScroll(speed) {
                                                        window.scrollBy(0,speed);
                                                        scrolldelay = setTimeout('pageScroll()',10);
                                                    }
                                                </script>
                                            </head>
                                            <body>
                                                <pre>{TAB_CONTENTS}</pre>
                                            </body>
                                        </html>";

        private readonly Timer _scrollTimer = new Timer();
    }
}