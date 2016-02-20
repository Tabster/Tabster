#region

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#endregion

namespace Tabster.Update
{
    internal class Release
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

    internal class ReleasesChanges
    {
        [JsonProperty("version")]
        [JsonConverter(typeof (VersionConverter))]
        public Version Version { get; internal set; }

        [JsonProperty("changes")]
        public List<string> Changes { get; internal set; }
    }

    internal class UpdateResponse
    {
        [JsonProperty("latest_version")]
        [JsonConverter(typeof (VersionConverter))]
        public Version LatestVersion { get; internal set; }

        [JsonProperty("releases")]
        public List<Release> Releases { get; internal set; }

        [JsonProperty("changelog")]
        public List<ReleasesChanges> Changelog { get; internal set; }
    }
}