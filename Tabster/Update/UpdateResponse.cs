﻿#region

using System.Collections.Generic;
using Newtonsoft.Json;
using Tabster.Core.Types;

#endregion

namespace Tabster.Update
{
    public class Release
    {
        [JsonProperty("platform")]
        public string Platform { get; internal set; }

        [JsonProperty("portable")]
        public bool Portable { get; internal set; }

        [JsonProperty("download_url")]
        public string DownloadUrl { get; internal set; }

        [JsonProperty("release_page")]
        public string ReleasePage { get; internal set; }
    }

    public class ReleasesChanges
    {
        [JsonProperty("version")]
        [JsonConverter(typeof (TabsterVersionConverter))]
        public TabsterVersion Version { get; internal set; }

        [JsonProperty("changes")]
        public List<string> Changes { get; internal set; }
    }

    public class UpdateResponse
    {
        [JsonProperty("latest_version")]
        [JsonConverter(typeof (TabsterVersionConverter))]
        public TabsterVersion LatestVersion { get; internal set; }

        [JsonProperty("releases")]
        public List<Release> Releases { get; internal set; }

        [JsonProperty("changelog")]
        public List<ReleasesChanges> Changelog { get; internal set; }
    }
}