#region

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace Tabster.Controls
{
    public class StaticTreeView : TreeView
    {
        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;

        public bool AllowRootNodeSelection { get; set; }

        public TreeNode FirstNode
        {
            get
            {
                return Nodes.Count > 0 ? Nodes[0] : null;
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public void SelectPreviousNode(bool defaultToFirstNode = false)
        {
            TreeNode previousNode = null;

            if (SelectedNode != null)
            {
                if (SelectedNode.Index > 0)
                {
                    previousNode = Nodes[SelectedNode.Index - 1];
                }

                else if (SelectedNode.Parent != null && AllowRootNodeSelection)
                {
                    previousNode = SelectedNode.Parent;
                }
            }

            if (previousNode != null)
            {
                SelectedNode = previousNode;
            }

            if (previousNode == null && defaultToFirstNode && FirstNode != null)
                SelectedNode = FirstNode;
        }

        public void SelectNextNode(bool useNested)
        {
            TreeNode nextNode = null;

            if (SelectedNode != null)
            {
                if (useNested && SelectedNode.Nodes.Count > 0)
                {
                    nextNode = SelectedNode.FirstNode;
                }

                else if (SelectedNode.Parent != null && SelectedNode.Parent.Nodes.Count > 1)
                {
                    nextNode = SelectedNode.Parent.Nodes[SelectedNode.Index + 1];
                }
            }

            else if (FirstNode != null)
            {
                nextNode = FirstNode;
            }

            if (nextNode != null)
                SelectedNode = nextNode;
        }

        #region Overrides

        protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
            base.OnBeforeCollapse(e);
        }

        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            if (!AllowRootNodeSelection && e.Node.Nodes.Count > 0)
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
    }
}