#region

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tabster.Core.FileTypes;
using Tabster.Forms;

#endregion

namespace Tabster.Controls
{
    public class TreeViewExtended : TreeView
    {
        
    }

    public class Sidebar : TreeView
    {
        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;

        private readonly Font ChildFont = new Font("Microsoft Sans Serif", 9F);
        private readonly Font ParentFont = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        public TreeNode NodeAllTabs;
        public TreeNode NodeBassTabs;
        public TreeNode NodeDrumTabs;
        public TreeNode NodeGuitarChords;
        public TreeNode NodeGuitarTabs;
        public TreeNode NodeLibrary;
        public TreeNode NodeMyDownloads;
        public TreeNode NodeMyFavorites;
        public TreeNode NodeMyImports;
        public TreeNode NodeMyTabs;
        public TreeNode NodePlaylists;
        public TreeNode NodeUkuleleTabs;

        public ContextMenu PlaylistMenu = new ContextMenu();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public void LoadNodes()
        {
            NodeAllTabs = new TreeNode("All Tabs") {Name = "node_alltabs", NodeFont = ChildFont};
            NodeMyTabs = new TreeNode("My Tabs") {Name = "node_mytabs", NodeFont = ChildFont};
            NodeMyDownloads = new TreeNode("My Downloads") {Name = "node_mydownloads", NodeFont = ChildFont};
            NodeMyImports = new TreeNode("My Imports") {Name = "node_myimports", NodeFont = ChildFont};
            NodeMyFavorites = new TreeNode("My Favorites") {Name = "node_myfavorites", NodeFont = ChildFont};
            NodeGuitarTabs = new TreeNode("Guitar Tabs") {Name = "node_guitartabs", NodeFont = ChildFont};
            NodeGuitarChords = new TreeNode("Guitar Chords") {Name = "node_guitarchords", NodeFont = ChildFont};
            NodeBassTabs = new TreeNode("Bass Tabs") {Name = "node_basstabs", NodeFont = ChildFont};
            NodeDrumTabs = new TreeNode("Drum Tabs") {Name = "node_drumtabs", NodeFont = ChildFont};
            NodeUkuleleTabs = new TreeNode("Ukulele Tabs") {Name = "node_ukuleletabs", NodeFont = ChildFont};

            NodeLibrary = new TreeNode("Library", new[]
                                                      {
                                                          NodeAllTabs,
                                                          NodeMyTabs,
                                                          NodeMyDownloads,
                                                          NodeMyImports,
                                                          NodeMyFavorites,
                                                          NodeGuitarTabs,
                                                          NodeGuitarChords,
                                                          NodeBassTabs,
                                                          NodeDrumTabs,
                                                          NodeUkuleleTabs
                                                      }) {Name = "node_library", NodeFont = ParentFont};

            NodePlaylists = new TreeNode("Playlists") {Name = "node_playlists", NodeFont = ParentFont};

            Nodes.Clear();
            Nodes.AddRange(new[]
                               {
                                   NodeLibrary,
                                   NodePlaylists
                               });

            ExpandAll();

            FullRowSelect = true;
            HideSelection = false;
            Indent = 15;
            ItemHeight = 24;
            LineColor = Color.Empty;
            ShowLines = false;
            ShowPlusMinus = false;
            Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, ((0)));
        }

        public void UpdateLibraryCount(MainForm.LibraryType type, int count)
        {
            switch (type)
            {
                case MainForm.LibraryType.AllTabs:
                    NodeLibrary.Text = string.Format("Library ({0})", count);
                    break;
                    /*
                case Form1.LibraryType.MyTabs:
                    NodeMyTabs.Text = string.Format("My Tabs ({0})", count);
                    break;
                case Form1.LibraryType.MyImports:
                    NodeMyImports.Text = string.Format("My Imports ({0})", count);
                    break;
                case Form1.LibraryType.MyFavorites:
                    NodeAllTabs.Text = string.Format("My Favorites Tabs ({0})", count);
                    break;
                */
                case MainForm.LibraryType.GuitarTabs:
                    NodeGuitarTabs.Text = string.Format("Guitar Tabs ({0})", count);
                    break;
                case MainForm.LibraryType.GuitarChords:
                    NodeGuitarChords.Text = string.Format("Guitar Chords ({0})", count);
                    break;
                case MainForm.LibraryType.BassTabs:
                    NodeBassTabs.Text = string.Format("Bass Tabs ({0})", count);
                    break;
                case MainForm.LibraryType.DrumTabs:
                    NodeDrumTabs.Text = string.Format("Drum Tabs ({0})", count);
                    break;
                case MainForm.LibraryType.UkuleleTabs:
                    NodeUkuleleTabs.Text = string.Format("Ukulele Tabs ({0})", count);
                    break;
            }
        }

        #region Overrides

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            //playlist has been deleted, change index and remove node
            if (PlaylistNodeSelected())
            {
                var playlist_path = SelectedNode.Tag.ToString();

                if (!File.Exists(playlist_path))
                {
                    var toRemove = SelectedNode;
                    SelectPreviousPlaylist();
                    toRemove.Remove();

                    var p = Program.libraryManager.FindPlaylistByPath(playlist_path);
                    Program.libraryManager.Remove(p, true);
                    return;
                }
            }

            base.OnAfterSelect(e);
        }

        protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
        {
            e.Cancel = true;

            base.OnBeforeCollapse(e);
        }

        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            if (e.Node.IsExpanded || e.Node.Nodes.Count > 0)
            {
                e.Cancel = true;
            }

            base.OnBeforeSelect(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            SendMessage(Handle, TVM_SETEXTENDEDSTYLE, (IntPtr) TVS_EX_DOUBLEBUFFER, (IntPtr) TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
        }

        #endregion

        #region Playlists

        public void Add(TablaturePlaylistDocument p)
        {
            var node = new TreeNode(p.Name) {Tag = p.FileInfo.FullName, NodeFont = ChildFont};
            NodePlaylists.Nodes.Add(node);
            ExpandAll();
        }

        public void Remove(TablaturePlaylistDocument p)
        {
            foreach (TreeNode node in NodePlaylists.Nodes)
            {
                if (node.Tag.ToString().Equals(p.FileInfo.FullName, StringComparison.InvariantCultureIgnoreCase))
                {
                    NodePlaylists.Nodes.Remove(node);
                    Program.libraryManager.Remove(p, true);
                    break;
                }
            }
        }

        public bool PlaylistNodeSelected()
        {
            return SelectedNode != null && SelectedNode.Parent == Nodes["node_playlists"];
        }

        public TablaturePlaylistDocument SelectedPlaylist()
        {
            return PlaylistNodeSelected() ? Program.libraryManager.FindPlaylistByPath(SelectedNode.Tag.ToString()) : null;
        }

        public void SelectPreviousPlaylist()
        {
            if (SelectedNode != null)
            {
                SelectedNode = SelectedNode.Index > 0 ? NodePlaylists.Nodes[SelectedNode.Index - 1] : NodeAllTabs;
            }
        }

        #endregion
    }
}