namespace Tabster.Core.Plugins
{
    public interface ITabsterPlugin
    {
        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }
        string[] PluginClasses { get; }
    }
}
