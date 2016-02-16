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
    internal partial class SplashScreen : Form, SplashScreenController.ISplashScreenForm
    {
        private readonly bool _safeMode;
        private bool _closeRequested;
        private bool _splashFinalized;

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

        public new void Close()
        {
            _closeRequested = true;

            if (_splashFinalized)
                base.Close();
        }

        #region Implementation of ISplashScreenForm

        public void FinalizeSplash()
        {
            _splashFinalized = true;

            if (_closeRequested)
                base.Close();
        }

        private delegate void UpdateSplashScreenCallback(string text);

        public void Update(string status)
        {
            if (lblProgress.InvokeRequired)
            {
                var d = new UpdateSplashScreenCallback(Update);
                Invoke(d, new object[] { status });
            }

            else
            {
                lblProgress.Text = status;
            }
        }

        #endregion
    }
}