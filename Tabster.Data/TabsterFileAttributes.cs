#region

using System;

#endregion

namespace Tabster.Data
{
    public class TabsterFileAttributes
    {
        public DateTime Created { get; private set; }

        public TabsterFileAttributes(DateTime created)
        {
            Created = created;
        }

        //todo encoding
    }
}