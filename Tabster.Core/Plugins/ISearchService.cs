namespace Tabster.Core.Plugins
{
    public interface ISearchService
    {
        string Name { get; }
        IRemoteTab[] Search(string artist, string title, TabType? type);
        bool SupportsTabType(TabType type);
    }
}
