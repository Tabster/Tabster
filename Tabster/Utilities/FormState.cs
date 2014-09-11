#region

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace Tabster.Utilities
{
    public class FormState
    {
        private const int SWP_SHOWWINDOW = 64; // 0x0040

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private bool IsMaximized;
        private Rectangle bounds;
        private FormBorderStyle brdStyle;
        private bool topMost;
        private FormWindowState winState;

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        internal static extern int GetSystemMetrics(int which);

        [DllImport("user32.dll")]
        internal static extern void SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int X, int Y, int width, int height, uint flags);

        public void Maximize(Form targetForm)
        {
            if (!IsMaximized)
            {
                IsMaximized = true;
                Save(targetForm);
                targetForm.WindowState = FormWindowState.Maximized;
                targetForm.FormBorderStyle = FormBorderStyle.None;
                targetForm.TopMost = true;

                var screenX = GetSystemMetrics(SM_CXSCREEN);
                var screenY = GetSystemMetrics(SM_CYSCREEN);

                SetWindowPos(targetForm.Handle, IntPtr.Zero, 0, 0, screenX, screenY, SWP_SHOWWINDOW);
            }
        }

        public void Save(Form targetForm)
        {
            winState = targetForm.WindowState;
            brdStyle = targetForm.FormBorderStyle;
            topMost = targetForm.TopMost;
            bounds = targetForm.Bounds;
        }

        public void Restore(Form targetForm)
        {
            targetForm.WindowState = winState;
            targetForm.FormBorderStyle = brdStyle;
            targetForm.TopMost = topMost;
            targetForm.Bounds = bounds;
            IsMaximized = false;
        }
    }
}