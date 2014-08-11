#region

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tabster.Controls;
using Tabster.Core.FileTypes;
using Tabster.Forms;

#endregion

namespace Tabster
{
    public class TabViewerManager
    {
        #region Delegates

        public delegate void TabHandler(object sender, TablatureDocument TablatureDocument);

        #endregion

        private readonly Dictionary<TablatureDocument, TablatureEditor> _editors = new Dictionary<TablatureDocument, TablatureEditor>();

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

        public void Restore(TablatureDocument doc)
        {
            if (TabClosed != null)
                TabClosed(this, doc);
        }

        public void LoadExternally(TablatureDocument doc, bool show, bool forceFront = true)
        {
            var viewer = GetViewer(true);

            bool openedExternall, isNew;
            var editor = TryGetEditor(doc, out openedExternall, out isNew);

            viewer.LoadTab(doc, editor);

            if (show && !viewer.Visible)
            {
                var mainForm = Program.instanceController.MainForm;
                viewer.StartPosition = FormStartPosition.Manual;
                viewer.Location = new Point(mainForm.Location.X + (mainForm.Width - viewer.Width)/2, mainForm.Location.Y + (mainForm.Height - viewer.Height)/2);
                viewer.Show();
            }
        }

        public bool IsOpenedExternally(TablatureDocument tab)
        {
            var v = GetViewer(false);
            return v != null && v.AlreadyOpened(tab);
        }

        public TablatureEditor TryGetEditor(TablatureDocument tab, out bool openedExternally, out bool isNew)
        {
            if (_editors.ContainsKey(tab))
            {
                openedExternally = IsOpenedExternally(tab);
                isNew = false;
                return _editors[tab];
            }

            openedExternally = false;
            isNew = true;
            var editor = new TablatureEditor {Dock = DockStyle.Fill};
            _editors[tab] = editor;
            return editor;
        }
    }
}