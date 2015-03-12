#region

using System;
using System.Net;

#endregion

namespace Tabster.Utilities.Net
{
    public class ManualProxyParameters
    {
        public ManualProxyParameters(string host, ushort port)
        {
            Host = host;
            Port = port;
        }

        public ManualProxyParameters(string host, ushort port, ICredentials credentials)
            : this(host, port)
        {
            Credentials = credentials;
        }

        public string Host { get; set; }
        public ushort Port { get; set; }

        public ICredentials Credentials { get; set; }
    }

    public enum ProxyConfiguration
    {
        None,
        Manual,
        System
    }

    public class ProxyConfigurationException : Exception
    {
        public ProxyConfigurationException()
        {
        }

        public ProxyConfigurationException(string message)
            : base(message)
        {
        }
    }

    public class CustomProxyController
    {
        private ProxyConfiguration _configuration;

        public CustomProxyController(ProxyConfiguration configuration)
        {
            Configuration = configuration;
        }

        public CustomProxyController(ProxyConfiguration configuration, ManualProxyParameters manualProxyParameters)
        {
            ManualProxyParameters = manualProxyParameters;
            Configuration = configuration;
        }

        public WebProxy Proxy { get; private set; }

        public ManualProxyParameters ManualProxyParameters { get; set; }

        public ProxyConfiguration Configuration
        {
            get { return _configuration; }
            set
            {
                _configuration = value;
                Refresh();
            }
        }

        public void Refresh()
        {
            if (Configuration == ProxyConfiguration.None)
                Proxy = null;

            else if (Configuration == ProxyConfiguration.System)
            {
                //quick hack since WebProxy.GetDefaultProxy is deprecated
                var address = WebRequest.DefaultWebProxy.GetProxy(new Uri("http://www.google.com"));
                Proxy = new WebProxy(address) {UseDefaultCredentials = true};
            }

            else if (Configuration == ProxyConfiguration.Manual)
            {
                if (ManualProxyParameters == null)
                    throw new ProxyConfigurationException("Manual proxy parameters are not set.");

                Proxy = new WebProxy(ManualProxyParameters.Host, ManualProxyParameters.Port)
                {
                    UseDefaultCredentials = false
                };

                if (ManualProxyParameters.Credentials != null)
                    Proxy.Credentials = ManualProxyParameters.Credentials;
            }
        }
    }
}