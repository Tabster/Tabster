#region

using System;
using System.Net;
using Newtonsoft.Json;

#endregion

namespace Tabster.Update
{
    internal class UpdateCheck
    {
        private const string JsonSource = "http://tabster.org/update.json";

        public static event EventHandler<UpdateResponseEventArgs> Completed;

        public static void Check(object userToken = null)
        {
            using (var client = new WebClient {Proxy = null})
            {
                client.DownloadStringCompleted += client_DownloadStringCompleted;

                try
                {
                    client.DownloadStringAsync(new Uri(JsonSource), userToken);
                }
                catch (Exception ex)
                {
                    Utilities.Logging.GetLogger().Error("An error occured while checking for updates.", ex);

                    if (Completed != null)
                        Completed(null, new UpdateResponseEventArgs(null, ex, userToken));
                }
            }
        }

        private static void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
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
                Completed(null, new UpdateResponseEventArgs(response, error, e.UserState));
        }
    }
}