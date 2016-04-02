#region

using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tabster.Core.Types;
using Tabster.Utilities;

#endregion

namespace Tabster.Update
{
    public partial class UpdateDialog : Form
    {
        private readonly Release _newestRelease;
        private readonly UpdateResponse _updateResponse;

        public UpdateDialog(UpdateResponse updateResponse)
        {
            InitializeComponent();

            _updateResponse = updateResponse;

            _newestRelease = _updateResponse.Releases.First();

            DisplayReleaseInfo();
        }

        private void DisplayReleaseInfo()
        {
            foreach (var changelog in _updateResponse.Changelog)
            {
                var i = treeView1.Nodes.Add(new TreeNode(string.Format("Version {0}", changelog.Version)) {Tag = changelog.Version});
                var node = treeView1.Nodes[i];
                node.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
                node.Text = node.Text;

                foreach (var change in changelog.Changes)
                {
                    node.Nodes.Add(new TreeNode(change));
                }
            }

            if (treeView1.Nodes.Count > 0)
            {
                foreach (TreeNode node in treeView1.Nodes)
                {
                    var version = (TabsterVersion) node.Tag;
                    if (version > TabsterEnvironment.GetVersion())
                    {
                        node.Expand();
                    }
                }

                treeView1.Nodes[0].EnsureVisible();
            }
        }

        private void btnDownloadPage_Click(object sender, EventArgs e)
        {
            if (_newestRelease != null && !string.IsNullOrEmpty(_newestRelease.ReleasePage))
                Process.Start(_newestRelease.ReleasePage);
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}