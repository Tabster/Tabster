#region

using System;
using System.IO;
using System.Runtime.InteropServices;

#endregion

namespace Tabster.Utilities
{
    internal class MonoUtilities
    {
        public enum Platform
        {
            Windows,
            Unix,
            Mac,
            Other
        }

        private static Platform? _currentPlatform;

        public static Platform GetPlatform()
        {
            if (_currentPlatform.HasValue)
                return _currentPlatform.Value;

            var pid = Environment.OSVersion.Platform;

            if (pid == PlatformID.Win32NT || pid == PlatformID.Win32S || pid == PlatformID.Win32Windows || pid == PlatformID.WinCE)
                _currentPlatform = Platform.Windows;
            else if (IsRunningOnMac())
                _currentPlatform = Platform.Mac;
            else if (Environment.OSVersion.Platform == PlatformID.Unix)
                _currentPlatform = Platform.Unix;
            else
                _currentPlatform = Platform.Other;

            return _currentPlatform.Value;
        }

        [DllImport("libc")]
        private static extern int uname(IntPtr buf);

        // https://github.com/jpobst/Pinta/blob/master/Pinta.Core/Managers/SystemManager.cs#L162
        private static bool IsRunningOnMac()
        {
            var buf = IntPtr.Zero;
            try
            {
                buf = Marshal.AllocHGlobal(8192);
                // This is a hacktastic way of getting sysname from uname ()
                if (uname(buf) == 0)
                {
                    var os = Marshal.PtrToStringAnsi(buf);
                    if (os == "Darwin")
                        return true;
                }
            }
            catch
            {
            }
            finally
            {
                if (buf != IntPtr.Zero)
                    Marshal.FreeHGlobal(buf);
            }
            return false;
        }

        public static string ReadFileText(string fileName)
        {
            var text = File.ReadAllText(fileName);
            text = text.Replace(GetPlatform() == Platform.Windows ? "\n" : "\r\n", Environment.NewLine);
            return text;
        }
    }
}