#region

using System;
using System.Net;
using Newtonsoft.Json;

#endregion

namespace Tabster.Plugins
{
    internal class FeaturedPluginChecker
    {
        private const string JsonSource = "http://tabster.org/featured_plugins.json";

        public static event EventHandler<FeaturedPluginsResponseEventArgs> Completed;

        public static void Check(object userToken = null)
        {
            using (var client = new WebClient {Proxy = null})
            {
                client.DownloadStringCompleted += client_DownloadStringCompleted;
                client.DownloadStringAsync(new Uri(JsonSource), userToken);
            }
        }

        private static void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var error = e.Error;
            FeaturedPluginsResponse response = null;

            if (e.Error == null && !e.Cancelled && !string.IsNullOrEmpty(e.Result))
            {
                try
                {
                    response = JsonConvert.DeserializeObject<FeaturedPluginsResponse>(e.Result);
                }
                catch (Exception ex)
                {
                    Utilities.Logging.GetLogger().Error("An error occured while fetching featured plugins JSON.", ex);
                    error = ex;
                }
            }

            if (Completed != null)
                Completed(null, new FeaturedPluginsResponseEventArgs(response, error, e.UserState));
        }

        internal class FeaturedPluginsResponseEventArgs : EventArgs
        {
            public FeaturedPluginsResponseEventArgs(FeaturedPluginsResponse response, Exception exception, object userState)
            {
                Response = response;
                Error = exception;
                UserState = userState;
            }

            public FeaturedPluginsResponse Response { get; private set; }
            public object UserState { get; private set; }
            public Exception Error { get; private set; }
        }
    }
}