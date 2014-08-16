#region

using System;

#endregion

namespace Tabster.Core.Data
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