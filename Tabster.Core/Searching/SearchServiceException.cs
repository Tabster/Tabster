#region

using System;

#endregion

namespace Tabster.Core.Searching
{
    public class SearchServiceException : Exception
    {
        public SearchServiceException()
        {
        }

        public SearchServiceException(string message)
            : base(message)
        {
        }
    }
}