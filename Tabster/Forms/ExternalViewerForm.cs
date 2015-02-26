#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tabster.Controls;
using Tabster.Core.Types;
using Tabster.Data;
using ToolStripRenderer = Tabster.Controls.ToolStripRenderer;

#endregion

namespace Tabster.Forms
{
    internal partial class ExternalViewerForm : Form
    {
        #region Delegates

        public delegate void TabHandler(object sender, TablatureDocument tablatureDocument);

        #endregion

        private readonly Form _owner;
        private readonly List<TabInstance> _tabInstances = new List<TabInstance>();
        private bool _isFullscreen;
        private FormBorderStyle _previousBorderStyle;
        private FormWindowState _previousWindowState;

        public ExternalViewerForm()
        {
            InitializeComponent();

            controlsToolStrip.Renderer = new ToolStripRenderer();
        }

        public ExternalViewerForm(Form owner) : this()
        {
            _owner = owner;
        }

        #region Public Methods

        public bool IsFileOpen(TablatureDocument doc)
        {
            return GetTabInstance(doc) != null;
        }

        public void LoadTablature(TablatureDocument doc)
        {
            var instance = IsFileOpen(doc) ? GetTabInstance(doc) : CreateTabInstance(doc);

            if (!Visible)
            {
                if (_owner != null)
                {
                    StartPosition = FormStartPosition.Manual;
                    Location = new Point(_owner.Location.X + (_owner.Width - Width)/2,
                        _owner.Location.Y + (_owner.Height - Height)/2);

                    Show(_owner);
                }

                else
                {
                    Show();
                }
            }

            SelectTabInstance(instance);
        }

        #endregion

        public event TabHandler TabClosed;
        public event TabHandler TabOpened;

        private TabInstance CreateTabInstance(TablatureDocument doc)
        {
            var editor = new BasicTablatureTextEditor {Dock = DockStyle.Fill, ReadOnly = false};
            var instance = new TabInstance(doc, editor);

            _tabInstances.Add(instance);
            tabControl1.TabPages.Add(instance.Page);

            editor.ContentsModified += editor_ContentsModified;
            editor.TablatureLoaded += editor_TablatureLoaded;
            editor.LoadTablature(doc);

            if (TabOpened != null)
                TabOpened(this, doc);

            return instance;
        }

        private void editor_ContentsModified(object sender, EventArgs e)
        {
            UpdateInstanceControls(GetInstance((BasicTablatureTextEditor) sender));
        }

        private void editor_TablatureLoaded(object sender, EventArgs e)
        {
            UpdateInstanceControls(GetInstance((BasicTablatureTextEditor) sender));
        }

        private void SelectTabInstance(TabInstance instance)
        {
            tabControl1.SelectedTab = instance.Page;
            instance.Editor.Focus();
        }

        private TabInstance GetTabInstance(TablatureDocument doc)
        {
            return _tabInstances.Find(x => x.File.FileInfo.FullName.Equals(doc.FileInfo.FullName));
        }

        private TabInstance GetSelectedInstance()
        {
            var selectedTab = tabControl1.SelectedTab;
            return _tabInstances.Find(x => x.Page == selectedTab);
        }

        private void UpdateInstanceControls(TabInstance instance)
        {
            savebtn.Enabled = instance.Editor.Modified;
            instance.Page.ImageIndex = instance.Editor.Modified ? 1 : 0;
        }

        private bool CloseInstance(TabInstance instance, bool closeIfLast)
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

            if (TabClosed != null)
                TabClosed(this, instance.File);

            if (closeIfLast && tabControl1.TabPages.Count == 0)
            {
                Close();
            }

            return true;
        }

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
                CloseInstance(instance, true);
        }

        private void TabbedViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //remove in reverse order
            for (var i = _tabInstances.Count - 1; i > -1; i--)
            {
                var instance = _tabInstances[i];

                var result = CloseInstance(instance, false);

                if (!result)
                {
                    e.Cancel = true;
                    break;
                }
            }

            //hide instead of closing
            Hide();
            e.Cancel = true;
        }

        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                for (var i = tabControl1.TabPages.Count - 1; i >= 0; i--)
                {
                    if (tabControl1.GetTabRect(i).Contains(e.Location))
                    {
                        var tabPage = ((TabControl) sender).TabPages[i];

                        var match = _tabInstances.Find(x => x.Page == tabPage);

                        if (match != null)
                        {
                            CloseInstance(match, true);
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
                instance.Modified = false;
                savebtn.Enabled = false;
            }
        }

        private TabInstance GetInstance(BasicTablatureTextEditor editor)
        {
            return _tabInstances.FirstOrDefault(instance => instance.Editor == editor);
        }
    }

    internal class TabInstance
    {
        public TabInstance(TablatureDocument file, BasicTablatureTextEditor editor = null)
        {
            File = file;

            Page = new EllipsizedTabPage {Text = file.ToFriendlyString(), ToolTipText = file.FileInfo.FullName};

            Editor = editor ?? new BasicTablatureTextEditor {Dock = DockStyle.Fill};

            Page.Controls.Add(Editor);

            Editor.LoadTablature(file);
        }

        public TabPage Page { get; private set; }
        public BasicTablatureTextEditor Editor { get; private set; }
        public TablatureDocument File { get; private set; }

        public bool Modified
        {
            get { return Editor.Modified; }
            set { Editor.Modified = value; }
        }
    }
}