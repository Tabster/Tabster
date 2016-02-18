#region

using System;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

#endregion

namespace Tabster.Utilities
{
    internal class SplashScreenController
    {
        private readonly Form _form;

        public SplashScreenController(Form form)
        {
            _form = form;

            var splashScreenForm = form as ISplashScreenForm;
            if (splashScreenForm == null)
                throw new ArgumentException("Argument needs to implement ISplashScreenForm interface", "form");
        }

        public void Start(int minDisplayTime)
        {
            var splashThread = new Thread(StartSplash) {IsBackground = true};
            splashThread.Start();

            var splashTimer = new Timer {Interval = 3500};
            splashTimer.Elapsed += (sender, eventArgs) =>
            {
                _form.Invoke(new MethodInvoker(_form.Close));
                splashThread.Abort();
                splashTimer.Stop();
            };
            splashTimer.Start();
        }

        public void Stop()
        {
            ((ISplashScreenForm) _form).FinalizeSplash();
        }

        public void Update(string str)
        {
            if (_form == null)
                return;

            try
            {
                ((ISplashScreenForm) _form).Update(str);
            }

            catch (InvalidOperationException)
            {
                //sometimes happens "randomly"
            }

            ((ISplashScreenForm) _form).Update(str);
        }

        private void StartSplash()
        {
            _form.Show();
            Application.Run(_form);
        }

        public interface ISplashScreenForm
        {
            void FinalizeSplash();
            void Update(string str);
        }
    }
}