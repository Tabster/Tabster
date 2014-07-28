#region

using System;
using System.Drawing;
using System.Windows.Forms;
using Tabster.Properties;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    public partial class SplashScreen : Form
    {
        public bool SplashComplete;
        public int _Progress;

        public string[] status = { "Loading plugins...", "Loading tabs...", "Loading playlists...", "Checking for updates..." };

        public SplashScreen()
        {
            InitializeComponent();
            pictureBox1.Image = Resources.guitar128;
            lblversion.Text = string.Format("v{0}", new Version(Application.ProductVersion).ToShortString());

            simpleProgressBar1.Maximum = status.Length*(simpleProgressBar1.Maximum/status.Length);

            lblversion.ForeColor = Color.Gray;
            BringToFront();
            timer1.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var borderColor = Color.Black;
            const int borderWidth = 1;
            const ButtonBorderStyle borderStyle = ButtonBorderStyle.Solid;

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                    borderColor, borderWidth, borderStyle,
                                    borderColor, borderWidth, borderStyle,
                                    borderColor, borderWidth, borderStyle,
                                    borderColor, borderWidth, borderStyle);
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