#region

using System;

#endregion

namespace Tabster.Data
{
    public class TabsterFileException : Exception
    {
        public TabsterFileException()
        {
        }

        public TabsterFileException(string message)
            : base(message)
        {
        }
    }
}