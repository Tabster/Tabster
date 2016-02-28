#region

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using Tabster.Core.Types;
using Tabster.Utilities;

#endregion

namespace Tabster.Forms
{
    internal partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();

#if PORTABLE
            lblPortable.Visible = true;
#endif

            if (TabsterEnvironment.SafeMode)
                lblSafeMode.Visible = true;

            RoundBorderForm(this);

            lblProgress.Text = string.Empty;

            lblVersion.Text = string.Format("v{0}", TabsterEnvironment.GetVersion().ToString(TabsterVersionFormatFlags.Truncated));
            lblBuild.Text = string.Format("Build {0}", TabsterEnvironment.GetVersion().Build);
            lblCopyright.Text = BrandingUtilities.GetCopyrightString(Assembly.GetExecutingAssembly());
            BringToFront();
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