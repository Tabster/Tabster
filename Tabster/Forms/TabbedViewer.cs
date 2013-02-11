#region

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tabster.Controls;

#endregion

namespace Tabster.Forms
{
    public partial class TabbedViewer : Form
    {
        private readonly List<TabFile> _tabs = new List<TabFile>();

        public TabbedViewer()
        {
            InitializeComponent();
        }

        public bool AlreadyOpened(TabFile tab)
        {
            return _tabs.Find(x => x.FileInfo.FullName.Equals(tab.FileInfo.FullName, StringComparison.OrdinalIgnoreCase)) != null;
        }

        private TabPage GetOpenedTabPage(TabFile tab)
        {
            foreach (TabPage tp in tabControl1.TabPages)
            {
                if (tp.ToolTipText.Equals(tab.FileInfo.FullName, StringComparison.OrdinalIgnoreCase))
                {
                    return tp;
                }
            }

            return null;
        }

        private TabEditor GetSelectedTabEditor()
        {
            return (TabEditor)tabControl1.SelectedTab.Controls[0];
        }

        private static void SetTabHeader(TabPage tp, TabFile tabFile, bool modified)
        {
            tp.Text = string.Format("{0} - {1} ({2}) {3}",
                                    tabFile.TabData.Artist, tabFile.TabData.Title, Global.GetTabString(tabFile.TabData.Type), modified ? "*" : "");
        }

        public void LoadTab(TabFile tab)
        {
            //var openedTab = _tabs.Find(x => x.FileInfo.FullName.Equals(tab.FileInfo.FullName, StringComparison.OrdinalIgnoreCase));

            var openedTab = GetOpenedTabPage(tab);

            if (openedTab != null)
            {
                tabControl1.SelectedTab = openedTab;
            }

            else
            {
                var newTab = new TabPage {ToolTipText = tab.FileInfo.FullName};
                var editor = new TabEditor {Dock = DockStyle.Fill};
                editor.LoadTab(tab);
                newTab.Controls.Add(editor);

                SetTabHeader(newTab, tab, false);

                editor.TabModified += editor_TabModified;

                tabControl1.TabPages.Add(newTab);
                tabControl1.SelectedTab = newTab;

                

                _tabs.Add(tab);
            }
        }

        void editor_TabModified(object sender, EventArgs e)
        {
            Console.WriteLine("modified");

            var editor = ((TabEditor) sender);
            var associatedTab = GetOpenedTabPage(editor.Tab);
            SetTabHeader(associatedTab, editor.Tab, editor.HasBeenModified);
        }

        private void modebtn_Click(object sender, EventArgs e)
        {

        }

        private void colorTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modebtn_Click_1(object sender, EventArgs e)
        {
            var selectedEditor = GetSelectedTabEditor();
            selectedEditor.SwitchMode();
        }
    }
}