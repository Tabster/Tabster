#region

using System;
using System.Reflection;
using System.Text;
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

                GlobalContext.Properties["HeaderInfo"] = GetHeaderInfo();
                GlobalContext.Properties["LogDirectory"] = _logDirectory;

                XmlConfigurator.Configure();

                _configured = true;
            }

            return _logger ?? (_logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType));
        }

        private static string GetHeaderInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Tabster {0}", TabsterEnvironment.GetVersion().ToString(TabsterVersionFormatFlags.BuildString)));

            var types = new[]
            {
                typeof (IAsciiTablature), // Tabster.Core
                typeof (Data.ITablatureFile), // Tabster.Data
                typeof (WinForms.TablatureTextEditorBase), // Tabster.WinForms
                typeof (Printing.TablaturePrintDocument), // Tabster.Printing
            };

            foreach (var type in types)
            {
                var assembly = Assembly.GetAssembly(type);
                var version = new TabsterVersion(assembly.GetName().Version);

                sb.AppendLine(string.Format("Referenced: {0} - {1}", assembly.GetName().Name, version));
            }

            return sb.ToString();
        }
    }
}