#region

using System;

#endregion

namespace Tabster.Core.Searching
{
    public class TablatureSearchEngineException : Exception
    {
        public TablatureSearchEngineException()
        {
        }

        public TablatureSearchEngineException(string message)
            : base(message)
        {
        }
    }
}