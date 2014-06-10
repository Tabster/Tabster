#region

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tabster.Controls;
using Tabster.Core;
using Tabster.Forms;

#endregion

namespace Tabster
{
    public class TabViewerManager
    {
        #region Delegates

        public delegate void TabHandler(object sender, TabFile tabFile);

        #endregion

        private readonly Dictionary<TabFile, TabEditor> _editors = new Dictionary<TabFile, TabEditor>();

        private TabbedViewer _viewer;

        public event TabHandler TabOpened;
        public event TabHandler TabClosed;

        private TabbedViewer GetViewer(bool createOnNull)
        {
            if ((_viewer != null && _viewer.IsDisposed) || (_viewer == null && createOnNull))
            {
                _viewer = new TabbedViewer();
            }

            return _viewer;
        }

        public void Restore(TabFile tabFile)
        {
            if (TabClosed != null)
                TabClosed(this, tabFile);
        }

        public void LoadExternally(TabFile tabFile, bool show, bool forceFront = true)
        {
            var viewer = GetViewer(true);

            bool openedExternall, isNew;
            var editor = TryGetEditor(tabFile, out openedExternall, out isNew);

            viewer.LoadTab(tabFile, editor);

            if (show && !viewer.Visible)
            {
                var mainForm = Program.instanceController.MainForm;
                viewer.StartPosition = FormStartPosition.Manual;
                viewer.Location = new Point(mainForm.Location.X + (mainForm.Width - viewer.Width)/2, mainForm.Location.Y + (mainForm.Height - viewer.Height)/2);
                viewer.Show();
            }
        }

        public bool IsOpenedExternally(TabFile tab)
        {
            var v = GetViewer(false);
            return v != null && v.AlreadyOpened(tab);
        }

        public TabEditor TryGetEditor(TabFile tab, out bool openedExternally, out bool isNew)
        {
            if (_editors.ContainsKey(tab))
            {
                openedExternally = IsOpenedExternally(tab);
                isNew = false;
                return _editors[tab];
            }

            openedExternally = false;
            isNew = true;
            var editor = new TabEditor {Dock = DockStyle.Fill};
            _editors[tab] = editor;
            return editor;
        }
    }
}