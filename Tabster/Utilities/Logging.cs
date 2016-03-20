#region

using System;
using System.Reflection;
using log4net;
using log4net.Config;
using Tabster.Core.Types;

#endregion

namespace Tabster.Utilities
{
    internal static class Logging
    {
        private static ILog _logger;
        private static String _logDirectory;
        private static bool _configured;

        public static void SetLogDirectory(String directory)
        {
            _logDirectory = directory;
        }

        public static ILog GetLogger()
        {
            if (!_configured)
            {
                if (_logDirectory == null)
                    throw new InvalidOperationException("Log directory needs to be set.");

                GlobalContext.Properties["HeaderInfo"] = string.Format("Tabster {0}",
                    TabsterEnvironment.GetVersion().ToString(TabsterVersionFormatFlags.BuildString));
                GlobalContext.Properties["LogDirectory"] = _logDirectory;

                XmlConfigurator.Configure();

                _configured = true;
            }

            return _logger ?? (_logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType));
        }
    }
}