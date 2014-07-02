#region

using System;

#endregion

namespace Tabster.Core.Plugins
{
    public class RemoteTab : Tab
    {
        public RemoteTab(Uri url, string artist, string title, TabType type, string contents) : base(artist, title, type, contents)
        {
            Url = url;
        }

        public Uri Url { get; private set; }
    }
}