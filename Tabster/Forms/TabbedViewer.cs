#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Tabster.Controls;

#endregion

namespace Tabster.Forms
{
    public partial class TabbedViewer : Form
    {
        private readonly List<TabInstance> _tabInstances = new List<TabInstance>();
        private readonly FormState formState = new FormState();

        public TabbedViewer()
        {
            InitializeComponent();

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            if (tabControl1.TabPages.Count == 0)
            {
                StartPosition = FormStartPosition.CenterParent;
            }
        }

        #region Methods

        public bool AlreadyOpened(TabFile tab)
        {
            TabPage t;
            return AlreadyOpened(tab, out t);
        }

        public bool AlreadyOpened(TabFile tab, out TabPage associatedTabPage)
        {
            var instance = _tabInstances.Find(x => x.File.FileInfo.FullName.Equals(tab.FileInfo.FullName, StringComparison.OrdinalIgnoreCase));
            associatedTabPage = instance != null ? instance.Page : null;
            return instance != null;
        }

        private TabInstance GetSelectedInstance()
        {
            var selectedTab = tabControl1.SelectedTab;
            return _tabInstances.Find(x => x.Page == selectedTab);
        }

        public void LoadTab(TabFile tabFile, TabEditor editor)
        {
            TabPage tp;
            var alreadyOpened = AlreadyOpened(tabFile, out tp);

            if (alreadyOpened)
            {
                tabControl1.SelectedTab = tp;
            }

            else
            {
                var instance = new TabInstance(tabFile, editor);
                instance.SetHeader(false);
                _tabInstances.Add(instance);

                tabControl1.TabPages.Add(instance.Page);
                tabControl1.SelectedTab = instance.Page;

                tabControl1_SelectedIndexChanged(null, null);
            }
        }

        private bool CloseTab(TabInstance instance, bool closeIfLast)
        {
            var saveBeforeClosing = true;

            if (instance.Modified)
            {
                var result = MessageBox.Show(string.Format("Save modified changes for {0}?", instance.File.TabData.GetName()), "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

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

        #region Events

        #endregion

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var instance = GetSelectedInstance();

            if (instance != null)
                instance.File.Save();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var instance = GetSelectedInstance();

            if (instance != null)
                instance.Editor.ShowPrintDialog();
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var instance = GetSelectedInstance();

            if (instance != null)
                CloseTab(instance, true);
        }

        private void modebtn_Click_1(object sender, EventArgs e)
        {
            var instance = GetSelectedInstance();

            if (instance != null)

                instance.Editor.SwitchMode();
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
                Text = string.Format("Tabster - {0}", tabControl1.SelectedTab.Text);
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
    }

    public class TabInstance
    {
        public TabInstance(TabFile file, TabEditor editor = null)
        {
            File = file;
            Page = new TabPage {Text = file.TabData.GetName(), ToolTipText = file.FileInfo.FullName};
            Editor = editor ?? new TabEditor { Dock = DockStyle.Fill };

            Page.Controls.Add(Editor);

            Editor.LoadTab(file.TabData);
            Editor.TabModified += editor_TabModified;
        }

        public TabPage Page { get; private set; }
        public TabEditor Editor { get; private set; }
        public TabFile File { get; private set; }
        public bool Modified { get; private set; }

        private void editor_TabModified(object sender, EventArgs e)
        {
            Modified = Editor.HasBeenModified;

            if (Editor.HasBeenModified)
            {
                SetHeader(true);
            }
        }

        public void SetHeader(bool modified)
        {
            Page.Text = string.Format("{0} {1}", File.TabData.GetName(), modified ? "*" : "");
        }
    }
}