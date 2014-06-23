#region

using System;

#endregion

namespace Tabster.Core.Plugins
{
    public interface IRemoteTab
    {
        Uri Url { get; set; }
        string Artist { get; set; }
        string Title { get; set; }
        TabType Type { get; set; }
        string Contents { get; set; }

        Tab ToTab();
    }
}