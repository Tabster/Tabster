#region

using System;
using System.Drawing;
using System.Windows.Forms;
using Tabster.Controls;

#endregion

namespace Tabster.Forms
{
    public partial class PopoutWindow : Form
    {
        public PopoutWindow(TabFile tab)
        {
            InitializeComponent();
            tabEditor1.LoadTab(tab);
            tabEditor1.ModeChanged += tabEditor1_ModeChanged;
        }

        private void tabEditor1_ModeChanged(object sender, EventArgs e)
        {
            Console.WriteLine("event");

            if (tabEditor1.Mode == TabEditor.TabMode.Edit)
            {
                modebtn.Text = "View";
            }
            if (tabEditor1.Mode == TabEditor.TabMode.View)
            {
                modebtn.Text = "Edit";
            }
        }

        private void modebtn_Click(object sender, EventArgs e)
        {
            //tabEditor1.SwitchMode();
        }

        private void colorTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabEditor1.ForeColor = Color.White;
            tabEditor1.BackColor = Color.Black;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}