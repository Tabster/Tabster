#region

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace Tabster.Controls
{
    public class SimpleProgressBar : ProgressBar
    {
        [DllImport("uxtheme.dll")]
        internal static extern int SetWindowTheme(IntPtr hWnd, string appname, string idlist);

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetWindowTheme(Handle, "", "");
        }
    }
}