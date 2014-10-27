#region

using System;

#endregion

namespace Tabster.Data
{
    public class TablatureFileException : Exception
    {
        public TablatureFileException()
        {
        }

        public TablatureFileException(string message)
            : base(message)
        {
        }
    }
}