#region

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Tabster.Controls
{
    public partial class TabEditor : UserControl
    {
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

        #region AutoScrollSpeed enum

        public enum AutoScrollSpeed
        {
            Off = 0,
            Slow = 1,
            Medium = 3,
            Fast = 5,
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
        }

        #endregion

        #region Properties

        private TabMode _mode = TabMode.View;
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

        private Color _backcolor = Color.White;
        private Color _forecolor = Color.Black;

        public new Color ForeColor
        {
            get { return _forecolor; }
            set
            {
                _forecolor = value;

                if (Tab != null)
                    ReloadHTML();
            }
        }

        public new Color BackColor
        {
            get { return _backcolor; }
            set
            {
                _backcolor = value;

                if (Tab != null)
                    ReloadHTML();
            }
        }

        public TabFile Tab { get; private set; }

        public bool HasBeenModified { get; private set; }

        #endregion

        #region Events

        public event EventHandler ModeChanged;
        public event EventHandler TabLoaded;
        public event EventHandler TabClosed;
        public event EventHandler TabModified;

        private string _oldContents = "";

        private void textBox1_TextChanged(object sender, EventArgs e)
        {  
            if (textBox1.Text.Length != _oldContents.Length && textBox1.Text != _oldContents)
            {
                HasBeenModified = true;

                _oldContents = textBox1.Text;

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

        public void SwitchMode()
        {
            Mode = Mode == TabMode.Edit ? TabMode.View : TabMode.Edit;
        }

        public void LoadTab(TabFile t)
        {
            Tab = t;

            if (Tab != null)
            {
                textBox1.Text = Tab.TabData.Contents;
                ReloadHTML();

                if (Parent is Form)
                {
                    (Parent).Text = string.Format("{0} - {1}", Tab.TabData.Artist, Tab.TabData.Title);
                }

                Mode = TabMode.View;
            }

            if (TabLoaded != null)
                TabLoaded(this, EventArgs.Empty);
        }

        public void ScrollBy(AutoScrollSpeed speed)
        {
            if (webBrowser1.Document != null)
            {
                if (speed != AutoScrollSpeed.Off)
                {
                    var i = (int) speed;

                    /*
                   

                    var js = string.Format("javascript:var s = function() {{ window.scrollBy(0,{0}); setTimeout(s, 100); }}; s();", i);
                    Console.WriteLine("javascript: " + js);
                    webBrowser1.Navigate(js);
                    //webBrowser1.Navigate(string.Format("javascript:function pageScroll() {{ window.scrollBy(0,{0}); scrolldelay = setTimeout('pageScroll()',10); }}", i));*/

                    webBrowser1.Document.InvokeScript("pageScroll", new object[] {new[] {i.ToString()}});

                    Console.WriteLine("testing scrool");
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

            var html = tabHTML.Replace("{TAB_CONTENTS}", Tab.TabData.Contents);

            //System.IO.File.WriteAllText(@"C:\users\nate\desktop\test.html", html);

            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.OpenNew(true);
                webBrowser1.Document.Write(html);
            }

            else
            {
                webBrowser1.DocumentText = html;
            }


            /*
            var guid = Guid.NewGuid();


            if (string.IsNullOrEmpty(UniqueFilename) == false)
            {
                if (File.Exists(UniqueFilename))
                {
                    File.Delete(UniqueFilename);
                }
            }

            var path = string.Format("{0}{1}.html", Global.TempDirectory, guid);
            File.WriteAllText(path, html);

            UniqueFilename = path;

            webBrowser1.Navigate(path);
            */

            //System.IO.File.WriteAllText("C:\\OMGLOLXD.TXT", html);
        }

        /*
        public void ComputeHash()
        {
            _currentHash = textBox1.Text.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }*/

        public void Print()
        {
            webBrowser1.Print();
        }

        #endregion


    }
}