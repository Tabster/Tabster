#region

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

        public TabViewerManager(RecentTabs recentTabs)
        {
            Recent = recentTabs;
        }

        public RecentTabs Recent { get; private set; }

        public event TabHandler OnTabOpened;
        public event TabHandler OnTabClosed;

        private TabbedViewer GetViewer(bool createOnNull)
        {
            if ((_viewer != null && _viewer.IsDisposed) || (_viewer == null && createOnNull))
            {
                _viewer = new TabbedViewer();
            }

            return _viewer;
        }

        public void CloseTab(TabFile tabFile)
        {
            if (OnTabClosed != null)
                OnTabClosed(this, tabFile);
        }

        public void LoadTab(TabFile tabFile, bool show)
        {
            var v = GetViewer(true);

            v.LoadTab(tabFile);

            Recent.Add(tabFile);

            if (show)
            {
                v.Show();
                v.BringToFront();
            }

            if (OnTabOpened != null)
                OnTabOpened(this, tabFile);
        }

        public bool IsOpenInViewer(TabFile tab)
        {
            var v = GetViewer(false);
            return v != null && v.AlreadyOpened(tab);
        }
    }
}