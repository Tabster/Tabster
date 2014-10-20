#region

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Tabster.Controls.Extensions;
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

            lblProgress.Text = string.Empty;

            lblVersion.Text = string.Format("v{0}", new Version(Application.ProductVersion).ToShortString());
            lblCopyright.Text = AssemblyUtilities.GetCopyrightString(Assembly.GetExecutingAssembly());
            lblVersion.ForeColor = Color.Gray;
            BringToFront();
        }

        public void SetStatus(string status)
        {
            lblProgress.InvokeIfRequired(() => { lblProgress.Text = status; });
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
    }
}