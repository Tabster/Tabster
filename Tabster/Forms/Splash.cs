#region

using System;
using System.Drawing;
using System.Windows.Forms;
using Tabster.Properties;

#endregion

namespace Tabster.Forms
{
    public partial class Splash : Form
    {
        public bool SplashComplete;
        public int _Progress;

        public string[] status = {
                                     "Loading startup events...", "Organizing library content...", "Checking for updates...", "Loading playlists...",
                                     "Scanning tabs and loading library...", "Performing caching..."
                                 };

        public Splash()
        {
            InitializeComponent();
            pictureBox1.Image = Resources.guitar128;
            lblversion.Text = string.Format("v{0}", Application.ProductVersion);

            simpleProgressBar1.Maximum = status.Length*(simpleProgressBar1.Maximum/status.Length);

            lbldisclaimer.ForeColor = Color.Gray;
            lblversion.ForeColor = Color.Gray;
            BringToFront();
            timer1.Start();
        }

        private void UpdateProgress()
        {
            if (simpleProgressBar1.Value != simpleProgressBar1.Maximum)
            {
                lblloading.Text = status[_Progress/(simpleProgressBar1.Maximum/status.Length)];
                _Progress++;
                simpleProgressBar1.Value++;
            }

            if (_Progress == 840)
            {
                timer1.Enabled = false;
                Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateProgress));
            }
        }
    }
}