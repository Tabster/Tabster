#region

using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tabster.Utilities.Extensions;
using Tabster.Utilities.Reflection;

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

            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));

            lblProgress.Text = string.Empty;

            lblVersion.Text = string.Format("v{0}", new Version(Application.ProductVersion).ToShortString());
            lblCopyright.Text = AssemblyUtilities.GetCopyrightString(Assembly.GetExecutingAssembly());
            lblVersion.ForeColor = Color.Gray;
            BringToFront();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
            );

        public void SetStatus(string status)
        {
            if (lblProgress.InvokeRequired)
            {
                var d = new SetStatusCallback(SetStatus);
                Invoke(d, new object[] {status});
            }

            else
            {
                lblProgress.Text = status;
            }
        }

        private delegate void SetStatusCallback(string text);
    }
}