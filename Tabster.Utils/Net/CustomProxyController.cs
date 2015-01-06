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
        private WebProxy _proxy;

        public CustomProxyController(ProxyConfiguration configuration)
        {
            Configuration = configuration;
        }

        public CustomProxyController(ProxyConfiguration configuration, ManualProxyParameters manualProxyParameters)
        {
            ManualProxyParameters = manualProxyParameters;
            Configuration = configuration;
        }

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

        public WebProxy GetProxy(bool refresh = false)
        {
            if (refresh)
                Refresh();

            return _proxy;
        }

        public void Refresh()
        {
            if (Configuration == ProxyConfiguration.None)
                _proxy = null;

            else if (Configuration == ProxyConfiguration.System)
            {
                //quick hack since WebProxy.GetDefaultProxy is deprecated
                var address = WebRequest.DefaultWebProxy.GetProxy(new Uri("http://www.google.com"));
                _proxy = new WebProxy(address) {UseDefaultCredentials = true};
            }

            else if (Configuration == ProxyConfiguration.Manual)
            {
                if (ManualProxyParameters == null)
                    throw new ProxyConfigurationException("Manual proxy parameters are not set.");

                _proxy = new WebProxy(ManualProxyParameters.Host, ManualProxyParameters.Port)
                {
                    UseDefaultCredentials = false
                };

                if (ManualProxyParameters.Credentials != null)
                    _proxy.Credentials = ManualProxyParameters.Credentials;
            }
        }
    }
}