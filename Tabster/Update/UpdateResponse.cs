#region

using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Tabster.Utilities;

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
        [JsonConverter(typeof (VersionConverter))]
        public Version Version { get; internal set; }

        [JsonProperty("changes")]
        public List<string> Changes { get; internal set; }
    }

    public class UpdateResponse
    {
        [JsonProperty("latest_version")]
        [JsonConverter(typeof (VersionConverter))]
        public Version LatestVersion { get; internal set; }

        [JsonProperty("releases")]
        public List<Release> Releases { get; internal set; }

        [JsonProperty("changelog")]
        public List<ReleasesChanges> Changelog { get; internal set; }
    }

    public class UpdateResponseEventArgs : EventArgs
    {
        public UpdateResponseEventArgs(UpdateResponse updateResponse, Exception exception, object userState)
        {
            Response = updateResponse;
            Error = exception;
            UserState = userState;
        }

        public UpdateResponse Response { get; private set; }
        public object UserState { get; private set; }
        public Exception Error { get; private set; }
    }

    public class UpdateQuery
    {
        private const string JSON_SOURCE = "http://tabster.org/update.json";

        public event EventHandler<UpdateResponseEventArgs> Completed;

        public void Check(object userToken = null)
        {
            using (var client = new WebClient {Proxy = null})
            {
                client.DownloadStringCompleted += client_DownloadStringCompleted;

                try
                {
                    client.DownloadStringAsync(new Uri(JSON_SOURCE), userToken);
                }
                catch (Exception ex)
                {
                    Logging.GetLogger().Error("An error occured while checking for updates.", ex);

                    if (Completed != null)
                        Completed(this, new UpdateResponseEventArgs(null, ex, userToken));
                }
            }
        }

        private void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var error = e.Error;
            UpdateResponse response = null;

            if (e.Error == null && !e.Cancelled && !string.IsNullOrEmpty(e.Result))
            {
                try
                {
                    response = JsonConvert.DeserializeObject<UpdateResponse>(e.Result);
                }
                catch (Exception ex)
                {
                    error = ex;
                }
            }

            if (Completed != null)
                Completed(this, new UpdateResponseEventArgs(response, error, e.UserState));
        }
    }
}