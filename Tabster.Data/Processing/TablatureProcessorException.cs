#region

using System;

#endregion

namespace Tabster.Data.Processing
{
    public class TablatureProcessorException : Exception
    {
        public TablatureProcessorException()
        {
        }

        public TablatureProcessorException(string message)
            : base(message)
        {
        }
    }
}