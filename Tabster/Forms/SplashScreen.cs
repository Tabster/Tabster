#region

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    internal partial class SplashScreen : Form
    {
        private readonly bool _safeMode;

        public SplashScreen()
        {
            InitializeComponent();

#if PORTABLE
            lblPortable.Visible = true;
#endif

            if (_safeMode)
                lblSafeMode.Visible = true;

            RoundBorderForm(this);

            lblProgress.Text = string.Empty;

            lblVersion.Text = string.Format("v{0}", new Version(Application.ProductVersion).ToShortVersionString());
            lblCopyright.Text = BrandingUtilities.GetCopyrightString(Assembly.GetExecutingAssembly());
            lblVersion.ForeColor = Color.Gray;
            BringToFront();
        }

        public SplashScreen(bool safeMode) : this()
        {
            _safeMode = safeMode;
        }

        public static void RoundBorderForm(Form frm)
        {
            var bounds = new Rectangle(0, 0, frm.Width, frm.Height);
            const int cornerRadius = 18;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseAllFigures();

            frm.Region = new Region(path);
        }

        public void UpdateStatus(string status)
        {
            if (lblProgress.InvokeRequired)
            {
                var d = new UpdateSplashScreenCallback(UpdateStatus);
                Invoke(d, new object[] {status});
            }

            else
            {
                lblProgress.Text = status;
            }

            System.Threading.Thread.Sleep(220);
        }

        private delegate void UpdateSplashScreenCallback(string text);
    }
}