#region

using System.Net;
using System.Text;
using System.Windows.Forms;

#endregion

namespace Tabster.Updater
{
    public class UpdateWebClient : WebClient
    {
        public UpdateWebClient(IWebProxy proxy = null)
        {
            Proxy = proxy;
            Encoding = Encoding.UTF8;
            Headers.Add("user-agent", string.Format("Tabster Updater {0}", Application.ProductVersion));
        }
    }
}