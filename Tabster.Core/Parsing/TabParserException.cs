#region

using System;

#endregion

namespace Tabster.Core.Parsing
{
    public class TabParserException : Exception
    {
        public TabParserException()
        {
        }

        public TabParserException(string message)
            : base(message)
        {
        }
    }
}