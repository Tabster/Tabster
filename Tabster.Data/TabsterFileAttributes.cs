#region

using System;
using System.Text;

#endregion

namespace Tabster.Data
{
    public class TabsterFileAttributes
    {
        public DateTime Created { get; private set; }
        public Encoding Encoding { get; set; }

        public TabsterFileAttributes(DateTime created, Encoding encoding)
        {
            Created = created;
            Encoding = encoding;
        }
    }
}