#region

using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

#endregion

namespace Tabster.Forms
{
    public partial class Repair : Form
    {
        public Repair(TabFileCollection corruptedFiles)
        {
            InitializeComponent();

            /*

            foreach (var f in corruptedFiles)
            {
                var item = new ListViewItem {Text = Path.GetFileNameWithoutExtension(f.FilePath)};
                item.SubItems.Add(f.Reason == InvalidLibraryItemReason.Corrupt ? "Corrupt" : "Missing");
                item.SubItems.Add(Path.GetFileNameWithoutExtension(f.FilePath));
                item.ToolTipText = f.FilePath;
                listView1.Items.Add(item);
            }
             * 
             * */
        }

        private void RepairTabs()
        {
        }
    }
}