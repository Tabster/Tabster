#region

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Tabster.Controls;
using Tabster.Forms;

#endregion

namespace Tabster
{
    public class TabViewerManager
    {
        #region Delegates

        public delegate void TabHandler(object sender, TabFile tabFile);

        #endregion

        private TabbedViewer _viewer = new TabbedViewer();

        public event TabHandler OpenedExternally;
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
            var v = GetViewer(true);

            bool openedExternall, isNew;
            var editor = TryGetEditor(tabFile, out openedExternall, out isNew);

            v.LoadTab(tabFile, editor);

            if (show)
            {
                v.Show();
            }
        }

        public bool IsOpenedExternally(TabFile tab)
        {
            var v = GetViewer(false);
            return v != null && v.AlreadyOpened(tab);
        }

        private readonly Dictionary<TabFile, TabEditor> _editors = new Dictionary<TabFile, TabEditor>();

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
            var editor = new TabEditor {Dock = System.Windows.Forms.DockStyle.Fill};
            _editors[tab] = editor;
            return editor;
        }
    }
}