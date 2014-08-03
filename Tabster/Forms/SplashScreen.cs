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
        public SplashScreen()
        {
            InitializeComponent();
            pictureBox1.Image = Resources.guitar128;
            lblversion.Text = string.Format("v{0}", new Version(Application.ProductVersion).ToShortString());

            lblversion.ForeColor = Color.Gray;
            BringToFront();
        }

        public void SetStatus(string status)
        {
            lblloading.InvokeIfRequired(() => { lblloading.Text = status; });
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