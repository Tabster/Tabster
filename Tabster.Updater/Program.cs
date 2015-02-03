#region

using System;
using System.Windows.Forms;

#endregion

namespace Tabster.Updater
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //todo implement updating for portable mode
#if PORTABLE
            MessageBox.Show("The updater does not work for portable installations.", "Portable Mode", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
            Application.Run(new UpdateCheckDialog());
#endif
        }
    }
}