using System;
using System.Collections.Generic;
using System.Text;

namespace Tabster
{
    public interface IDownloadProvider
    {
        void Download();
        event EventHandler OnDownloaded;
    }
}