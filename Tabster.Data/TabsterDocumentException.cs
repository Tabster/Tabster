#region

using System;

#endregion

namespace Tabster.Data
{
    public class TabsterDocumentException : Exception
    {
        public TabsterDocumentException()
        {
        }

        public TabsterDocumentException(string message)
            : base(message)
        {
        }
    }
}