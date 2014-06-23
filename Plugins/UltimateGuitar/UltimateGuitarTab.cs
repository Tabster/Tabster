#region

using System;
using Tabster.Core;
using Tabster.Core.Plugins;

#endregion

namespace UltimateGuitar
{
    public class UltimateGuitarTab : IRemoteTab
    {
        public UltimateGuitarTab(Uri url, string artist, string title, TabType type, string contents)
        {
            Url = url;
            Artist = artist;
            Title = title;
            Type = type;
            Contents = contents;
        }

        #region Implementation of IRemoteTab

        public Uri Url { get; set; }

        public string Artist { get; set; }

        public string Title { get; set; }

        public TabType Type { get; set; }

        public string Contents { get; set; }

        public Tab ToTab()
        {
            return new Tab(Artist, Title, Type, Contents) {Source = Url, SourceType = TabSource.Download};
        }

        #endregion
    }
}