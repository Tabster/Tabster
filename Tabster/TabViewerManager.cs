#region

using System;
using Tabster.Forms;

#endregion

namespace Tabster
{
    public class TabViewerManager
    {
        private TabbedViewer _viewer = new TabbedViewer();

        public event EventHandler OnTabClosed;

        public void TabClosed(TabFile tab)
        {
            if (OnTabClosed != null)
                OnTabClosed(this, EventArgs.Empty);
        }

        private TabbedViewer GetViewer(bool createOnNull)
        {
            if (_viewer != null && _viewer.IsDisposed)
            {
                _viewer = new TabbedViewer();
            }

            if (_viewer == null && createOnNull)
            {
                _viewer = new TabbedViewer();
            }

            return _viewer;
        }

        public void LoadTab(TabFile tab, bool show)
        {
            var v = GetViewer(true);

            v.LoadTab(tab);

            if (show)
            {
                v.Show();
                v.BringToFront();
            }
        }



        public bool IsOpenInViewer(TabFile tab)
        {
            var v = GetViewer(false);
            return v != null && v.AlreadyOpened(tab);
        }
    }
}