#region

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using NS_Common;

#endregion

namespace Tabster.Forms
{
    public partial class TabViewer : Form
    {
        #region Constructor

        private readonly FormState formState = new FormState();
        private string original_title;

        public TabViewer(string accessMethod, string tabPath)
        {
            InitializeComponent();
            Icon = Common.GetAssemblyIcon(Assembly.GetExecutingAssembly().Location);

            if (tabControl1.TabPages.Count == 0)
            {
                StartPosition = FormStartPosition.CenterParent;
                NewTab(accessMethod, tabPath);
            }
        }

        private void TabViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            var TabsChanged = false;

            var unsaved_Tabs = "";

            //cycle through all open tabs
            //if any of them need saving, set TabsChanged to true
            foreach (TabPage tp in tabControl1.TabPages)
            {
                if (CompareHash(tp) == false)
                {
                    unsaved_Tabs = unsaved_Tabs + tp.Tag + "\r\n";
                    TabsChanged = true;
                }
            }

            //if there are tabs that need saved, show dialog
            if (TabsChanged)
            {
                switch (
                    MessageBox.Show(
                        string.Format(
                            "Changes have been made to the following tab(s):\r\n\r\n{0}\r\nAre you sure you want to quit without saving?",
                            unsaved_Tabs), "Close Tab", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                }
            }
        }

        #endregion

        #region Methods

        public void NewTab(string accessMethod, string tabPath)
        {
                //check if tab is already open
                foreach (TabPage page in tabControl1.TabPages)
                {
                    if (page.ToolTipText == tabPath)
                    {
                        tabControl1.SelectedTab = page;
                        return;
                    }
                }

                var doc = new XmlDocument();
                doc.Load(tabPath);

                var artist = doc.GetElementsByTagName("artist")[0].InnerText;
                var song = doc.GetElementsByTagName("song")[0].InnerText;
                var tab = doc.GetElementsByTagName("tab")[0].InnerText;
                var type = doc.GetElementsByTagName("type")[0].InnerText;

                //increment view count
                doc.GetElementsByTagName("views")[0].InnerText =
                    (Convert.ToInt32(doc.GetElementsByTagName("views")[0].InnerText) + 1).ToString();
                doc.Save(tabPath);

                //initialize the new tab/textbox/webbrowser
                var tp = new TabPage();
                var title = string.Format("{0} - {1} ({2})", artist, song, type);

                tp.Text = title;
                tp.ToolTipText = tabPath;
                tp.Tag = title;


                //webbrowser
                var webpage = new WebBrowser
                                  {
                                      Parent = tp,
                                      AllowNavigation = false,
                                      AllowWebBrowserDrop = false,
                                      IsWebBrowserContextMenuEnabled = false,
                                      ScriptErrorsSuppressed = true,
                                      WebBrowserShortcutsEnabled = false,
                                      Dock = DockStyle.Fill,
                                      TabIndex = 0
                                  };
                webpage.PreviewKeyDown += webpage_PreviewKeyDown;
                webpage.DocumentText =
                    string.Format(
                        @"<html><head><style type=""text/css"">
            pre {{ color:black;font:12px Courier New,Courier,monospace; }}</style>
            </head><body><pre>{0}</pre></body></html>",
                        tab);

                //textbox
                var txtedit = new TextBox
                                  {
                                      ForeColor = Color.Black,
                                      BackColor = Color.White,
                                      ScrollBars = ScrollBars.Both,
                                      AcceptsTab = true,
                                      Parent = tp,
                                      Dock = DockStyle.Fill,
                                      Font =
                                          new Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0),
                                      Multiline = true,
                                      Text = tab,
                                      ShortcutsEnabled = true
                                  };
                txtedit.TextChanged += txtedit_Textchanged;
                txtedit.MouseClick += txtedit_MouseClick;
                txtedit.Leave += txtedit_Leave;
                txtedit.Enter += txtedit_Enter;
                txtedit.KeyDown += txtedit_KeyDown;
                txtedit.TabIndex = 1;
                txtedit.Enabled = false;

                //store hash of tab in textbox tag
                txtedit.Tag = ComputeHash(txtedit.Text);

                //add the tab page and select it
                tabControl1.TabPages.Add(tp);
                tabControl1.SelectedTab = tp;

                tabControl1_SelectedIndexChanged(null, null);
                BringToFront();
                webpage.Select();

                if (accessMethod == "CreateTab")
                {
                    modebtn.PerformClick();
                }
        }


        private void ParseTab(string tabContents)
        {
            GetCurrentTextBox();

            var html =
                string.Format(
                    @"<html><head><style type=""text/css"">
            pre {{ color:black;font:12px Courier New,Courier,monospace; }}</style>
            </head><body><pre>{0}</pre></body></html>",
                    tabContents);

            if (GetCurrentWebBrowser().IsDisposed)
            {
                //webbrowser has been disposed
            }

            //Required to bypass bug with dynamically changing documenttext
            if (GetCurrentWebBrowser().Document != null)
            {
                GetCurrentWebBrowser().Document.OpenNew(true);
                GetCurrentWebBrowser().Document.Write(html);
            }
            else
            {
                GetCurrentWebBrowser().Navigate("about:blank");
                GetCurrentWebBrowser().DocumentText = html;
            }
        }

        private WebBrowser GetCurrentWebBrowser()
        {
            WebBrowser returnweb = null;

            foreach (Control control in tabControl1.SelectedTab.Controls)
            {
                if (control is WebBrowser)
                {
                    returnweb = (WebBrowser) control;
                    break;
                }
            }

            return returnweb;
        }

        private TextBox GetCurrentTextBox()
        {
            TextBox returntxt = null;

            foreach (Control control in tabControl1.SelectedTab.Controls)
            {
                if (control is TextBox)
                {
                    returntxt = (TextBox) control;
                    break;
                }
            }

            return returntxt;
        }

        private static string ComputeHash(string input)
        {
            return input.GetHashCode().ToString();
        }

        /// <summary>
        ///   Compares the two hashes to see if there are changes.  The original one stored in the textbox, the new one is created
        ///   by using ComputeHash() of the current textbox text.
        /// </summary>
        private static bool CompareHash(TabPage tp)
        {
            TextBox tabpage_textbox = null;

            foreach (Control control in tp.Controls)
            {
                if (control is TextBox)
                {
                    tabpage_textbox = (TextBox) control;
                    break;
                }
            }

            var original_tab_hash = tabpage_textbox.Tag.ToString();
            var current_tab_hash = ComputeHash(tabpage_textbox.Text);

            return original_tab_hash == current_tab_hash;
        }

        private void CloseTab(TabPage tp)
        {
                //hashes are the same
                if (CompareHash(tp))
                {
                    //only one tab is open, no need to show dialog
                    if (tabControl1.TabPages.Count == 1)
                    {
                        Dispose();
                    }

                        //multiple tabs are open
                    else
                    {
                        GetCurrentTextBox();
                        GetCurrentWebBrowser();
                        tp.Dispose();
                        tabControl1.TabPages.Remove(tp);
                    }
                }

                    //hashes are not the same
                else
                {
                    //only one tab is open
                    //close form
                    if (tabControl1.TabPages.Count == 1)
                    {
                        switch (
                            MessageBox.Show(
                                "Changes have been made to this tab.\r\nAre you sure you want to quit without saving?",
                                "Close Tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                                //user doesn't want to save
                            case DialogResult.Yes:
                                Close();
                                return;
                                //cancel
                            case DialogResult.No:
                                break;
                        }
                    }

                        //multiple tabs are open
                    else
                    {
                        switch (
                            MessageBox.Show(
                                "Changes have been made to one or more tabs.\r\nAre you sure you want to quit without saving?",
                                "Close Tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                                //user doesn't want to save
                            case DialogResult.Yes:
                                GetCurrentTextBox();
                                GetCurrentWebBrowser();
                                tp.Dispose();
                                tabControl1.TabPages.Remove(tp);
                                return;
                                //cancel
                            case DialogResult.No:
                                return;
                        }
                    }
                }
            
        }

        private void UpdateItems()
        {
            //go by whether the control is enabled rather than if it's focused
            //otherwise menu items will be pointless as they'll be disabled upon Leave

            var txtedit = GetCurrentTextBox();

            if (txtedit.Enabled)
            {
                undoToolStripMenuItem.Enabled = txtedit.CanUndo;
                selectAllToolStripMenuItem.Enabled = true;

                if (txtedit.SelectedText.Length > 0)
                {
                    cutToolStripMenuItem.Enabled = true;
                    copyToolStripMenuItem.Enabled = true;
                    deleteToolStripMenuItem.Enabled = true;
                }
            }

            else
            {
                undoToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                pasteToolStripMenuItem.Enabled = false;
                selectAllToolStripMenuItem.Enabled = false;
            }
        }

        #endregion

        #region Events

        private void modebtn_Click(object sender, EventArgs e)
        {
            var thiswebpage = GetCurrentWebBrowser();
            var txtedit = GetCurrentTextBox();

            //enabling/disabling txtedit fixes bug with TAB erasing textbox text
            if (modebtn.Text == "Edit Mode")
            {
                modebtn.Text = "View Mode";
                txtedit.Enabled = true;
                txtedit.BringToFront();
                tabControl1.SelectedTab.Text = original_title + " (Edit Mode)";

                //message regarding <span> tags in chords
                if (txtedit.Text.Contains("<span>"))
                {
                    MessageBox.Show(
                        "Warning: Do not delete any <span> or </span> tags while in\r\nedit mode. These are uses to properly display the chords\r\nwhile in viewing mode.  Thank you.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            else
            {
                modebtn.Text = "Edit Mode";
                txtedit.Enabled = false;
                thiswebpage.BringToFront();
                tabControl1.SelectedTab.Text = original_title;
            }

            UpdateItems();
        }

        private void webpage_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                fullScreenToolStripMenuItem.PerformClick();
            }
        }

        private void txtedit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                fullScreenToolStripMenuItem.PerformClick();
            }
        }

        private void txtedit_Enter(object sender, EventArgs e)
        {
            UpdateItems();
        }

        private void txtedit_Leave(object sender, EventArgs e)
        {
            UpdateItems();
        }

        private void txtedit_MouseClick(object sender, MouseEventArgs e)
        {
            UpdateItems();
        }

        private void txtedit_Textchanged(object sender, EventArgs e)
        {
            var txtedit = GetCurrentTextBox();

            UpdateItems();

            var currentTextHash = ComputeHash(txtedit.Text);

            //no changes have been made since last save/load
            tabControl1.SelectedTab.Text =
                string.Format(txtedit.Tag.ToString() == currentTextHash ? "{0} (Edit Mode)" : "{0} (Edit Mode) *",
                              original_title);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Text = string.Format("Tabster - {0}", tabControl1.SelectedTab.Text);
            original_title = tabControl1.SelectedTab.Tag.ToString();
            modebtn.Text = tabControl1.SelectedTab.Text.Contains("(Edit Mode)") ? "View Mode" : "Edit Mode";
        }

        private void closeTabContextMenuItem_Click(object sender, EventArgs e)
        {
            CloseTab(tabControl1.SelectedTab);
        }

        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                for (var i = tabControl1.TabPages.Count - 1; i >= 0; i--)
                {
                    if (tabControl1.GetTabRect(i).Contains(e.Location))
                    {
                        var tab = (sender as TabControl).TabPages[i];

                        //used for debugging
                        //MessageBox.Show(tab.Text);
                        CloseTab(tab);

                        //(sender as TabControl).TabPages.Remove((sender as TabControl).TabPages[i]);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Menu

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
                var txtedit = GetCurrentTextBox();

                //if it is a newly created tab
                if (tabControl1.SelectedTab.ToolTipText.Contains(Program.libraryManager.TemporaryDirectory))
                {
                    //move the file from  the temp location into the default directory
                    var fi = new FileInfo(tabControl1.SelectedTab.ToolTipText);
                    var newlocation = Program.libraryManager.LibraryDirectory + fi.Name;
                    File.Move(tabControl1.SelectedTab.ToolTipText, newlocation);
                    tabControl1.SelectedTab.ToolTipText = newlocation;

                    //re-hash the tab and store it in the textbox tag
                    txtedit.Tag = ComputeHash(txtedit.Text);
                    txtedit_Textchanged(null, null);
                    ParseTab(txtedit.Text);
                }

                    //it is not a new tab, save normally
                else
                {
                    var doc = new XmlDocument();
                    doc.Load(tabControl1.SelectedTab.ToolTipText);
                    var tabelem = doc.GetElementsByTagName("tab")[0] as XmlElement;
                    tabelem.InnerText = txtedit.Text;
                    doc.Save(tabControl1.SelectedTab.ToolTipText);

                    //re-hash the tab and store it in the textbox tag
                    txtedit.Tag = ComputeHash(txtedit.Text);
                    txtedit_Textchanged(null, null);
                    ParseTab(txtedit.Text);
                }
            
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentWebBrowser().ShowPrintDialog();
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseTab(tabControl1.SelectedTab);
        }

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fullScreenToolStripMenuItem.Text == "Full Screen")
            { 
                fullScreenToolStripMenuItem.Text = "Restore";

                formState.Maximize(this);
            }

            else
            {
                fullScreenToolStripMenuItem.Text = "Full Screen";

                formState.Restore(this);
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var txtedit = GetCurrentTextBox();
            txtedit.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var txtedit = GetCurrentTextBox();
            txtedit.ClearUndo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var txtedit = GetCurrentTextBox();
            txtedit.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var txtedit = GetCurrentTextBox();
            txtedit.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var txtedit = GetCurrentTextBox();
            txtedit.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var txtedit = GetCurrentTextBox();
            txtedit.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var txtedit = GetCurrentTextBox();
            txtedit.SelectAll();
        }

        #endregion
    }
}