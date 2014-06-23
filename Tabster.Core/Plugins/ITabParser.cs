using System;

namespace Tabster.Core.Plugins
{
    public interface ITabParser
    {
        string Name { get; }
        IRemoteTab ParseTabFromSource(string sourceText);
        bool MatchesUrlPattern(Uri url);
    }
}
