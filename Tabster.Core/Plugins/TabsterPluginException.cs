#region

using System;

#endregion

namespace Tabster.Core.Plugins
{
    public class TabsterPluginException : Exception
    {
        public TabsterPluginException()
        {
        }

        public TabsterPluginException(string message)
            : base(message)
        {
        }
    }
}