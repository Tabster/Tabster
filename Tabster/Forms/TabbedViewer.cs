#region

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tabster.Controls;
using Tabster.Core.Data;
using Tabster.Core.Types;
using Tabster.Utilities.Extensions;
using ToolStripRenderer = Tabster.Controls.ToolStripRenderer;

#endregion

namespace Tabster.Forms
{
    public partial class TabbedViewer : Form
    {
        private readonly List<TabInstance> _tabInstances = new List<TabInstance>();
        private bool _isFullscreen;
        private FormBorderStyle _previousBorderStyle;
        private FormWindowState _previousWindowState;

        public TabbedViewer()
        {
            InitializeComponent();

            controlsToolStrip.Renderer = new ToolStripRenderer();
        }

        #region Methods

        public bool AlreadyOpened(TablatureDocument doc)
        {
            TabPage t;
            return AlreadyOpened(doc, out t);
        }

        public bool AlreadyOpened(TablatureDocument doc, out TabPage associatedTabPage)
        {
            var instance = _tabInstances.Find(x => x.File.FileInfo.FullName.Equals(doc.FileInfo.FullName, StringComparison.OrdinalIgnoreCase));
            associatedTabPage = instance != null ? instance.Page : null;
            return instance != null;
        }

        private TabInstance GetSelectedInstance()
        {
            var selectedTab = tabControl1.SelectedTab;
            return _tabInstances.Find(x => x.Page == selectedTab);
        }

        private void autoScrollChange(object sender, EventArgs e)
        {
            var instance = GetSelectedInstance();

            if (instance != null)
            {
                var item = ((ToolStripMenuItem) sender);
                var text = item.Text;

                foreach (ToolStripMenuItem menuItem in toolStripButton3.DropDownItems)
                {
                    menuItem.Checked = menuItem.Text == item.Text;
                }

                instance.Editor.AutoScroll = text == "On";
            }
        }

        public void LoadTab(TablatureDocument tabDocument, BasicTablatureTextEditor editor)
        {
            TabPage tp;
            var alreadyOpened = AlreadyOpened(tabDocument, out tp);

            if (alreadyOpened)
            {
                tabControl1.SelectedTab = tp;
            }

            else
            {
                var instance = new TabInstance(tabDocument, editor);
                instance.SetHeader(false);

                _tabInstances.Add(instance);

                tabControl1.TabPages.Add(instance.Page);
                tabControl1.SelectedTab = instance.Page;

                editor.ReadOnly = false;

                editor.ContentsModified += editor_TabModified;
                
                tabControl1_SelectedIndexChanged(null, null);
            }
        }

        private void editor_TabModified(object sender, EventArgs e)
        {
            savebtn.Enabled = ((BasicTablatureTextEditor) sender).Modified;
        }

        private bool CloseTab(TabInstance instance, bool closeIfLast)
        {
            var saveBeforeClosing = true;

            if (instance.Modified)
            {
                var result = MessageBox.Show(string.Format("Save modified changes for {0}?", instance.File.ToFriendlyString()), "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                    return false;
                if (result == DialogResult.No)
                    saveBeforeClosing = false;
            }

            if (saveBeforeClosing)
            {
                instance.File.Save();
            }

            tabControl1.TabPages.Remove(instance.Page);
            _tabInstances.Remove(instance);
            Program.TabHandler.Restore(instance.File);

            if (closeIfLast && tabControl1.TabPages.Count == 0)
            {
                Close();
            }

            return true;
        }

        #endregion

        private void PrintTab(object sender, EventArgs e)
        {
            var instance = GetSelectedInstance();

            if (instance != null)
                instance.Editor.Print();
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var instance = GetSelectedInstance();

            if (instance != null)
                CloseTab(instance, true);
        }

        private void TabbedViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            var modifiedInstances = _tabInstances.FindAll(x => x.Modified);

            //remove in reverse order
            for (var i = modifiedInstances.Count - 1; i > -1; i--)
            {
                var instance = _tabInstances[i];
                var result = CloseTab(instance, false);

                if (!result)
                {
                    e.Cancel = true;
                    break;
                }
            }

            //remove in reverse order
            for (var i = _tabInstances.Count - 1; i > -1; i--)
            {
                var instance = _tabInstances[i];
                var result = CloseTab(instance, false);

                if (!result)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                for (var i = tabControl1.TabPages.Count - 1; i >= 0; i--)
                {
                    if (tabControl1.GetTabRect(i).Contains(e.Location))
                    {
                        var tc = sender as TabControl;
                        var tabPage = tc.TabPages[i];

                        var match = _tabInstances.Find(x => x.Page == tabPage);

                        if (match != null)
                        {
                            CloseTab(match, true);
                        }

                        break;
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                offToolStripMenuItem.PerformClick();
                Text = string.Format("{0} - {1}", Application.ProductName, tabControl1.SelectedTab.Text);

                savebtn.Enabled = GetSelectedInstance().Modified;
            }
        }

        private void ToggleFullscreen(object sender = null, EventArgs e = null)
        {
            if (_isFullscreen)
            {
                FormBorderStyle = _previousBorderStyle;
                WindowState = _previousWindowState;

                _isFullscreen = false;
                fullscreenbtn.Text = "Full Screen";
            }

            else
            {
                _previousBorderStyle = FormBorderStyle;
                _previousWindowState = WindowState;

                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                _isFullscreen = true;
                fullscreenbtn.Text = "Restore";
            }
        }

        private void TabbedViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                ToggleFullscreen();
            }

            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.S)
                {
                    if (savebtn.Enabled)
                        savebtn.PerformClick();
                }

                if (e.KeyCode == Keys.P)
                {
                    printbtn.PerformClick();
                }
            }
        }

        private void SaveTab(object sender, EventArgs e)
        {
            var instance = GetSelectedInstance();

            if (instance != null)
            {
                instance.File.Contents = instance.Editor.Text;
                instance.File.Save();
                //instance.Editor.ModificationCheck();
                savebtn.Enabled = false;
            }
        }
    }

    public class TabInstance
    {
        public TabInstance(TablatureDocument file, BasicTablatureTextEditor editor = null)
        {
            File = file;
            Page = new TabPage {Text = file.ToFriendlyString(), ToolTipText = file.FileInfo.FullName};
            Editor = editor ?? new BasicTablatureTextEditor {Dock = DockStyle.Fill};

            Page.Controls.Add(Editor);

            Editor.LoadTab(file);
            Editor.ContentsModified += editor_TabModified;
        }

        public TabPage Page { get; private set; }
        public BasicTablatureTextEditor Editor { get; private set; }
        public TablatureDocument File { get; private set; }

        public bool Modified
        {
            get { return Editor.Modified; }
        }

        private void editor_TabModified(object sender, EventArgs e)
        {
            SetHeader(Editor.Modified);
        }

        public void SetHeader(bool modified)
        {
            Page.Text = string.Format("{0} {1}", File.ToFriendlyString(), modified ? "*" : "");
        }
    }
}