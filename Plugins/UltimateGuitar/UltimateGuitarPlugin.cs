#region

using Tabster.Core.Plugins;

#endregion

namespace UltimateGuitar
{
    public class UltimateGuitarPlugin : ITabsterPlugin
    {
        #region Implementation of ITabsterPlugin

        public string Name
        {
            get { return "Ultimate Guitar Plugin"; }
        }

        public string Description
        {
            get { return "Supports ultimate-guitar.com tab searching and downloading."; }
        }

        public string Author
        {
            get { return "Nate Shoffner"; }
        }

        public string Version
        {
            get { return "1.0"; }
        }

        public string[] PluginClasses
        {
            get { return new[] { "UltimateGuitar.UltimateGuitarSearch", "UltimateGuitar.UltimateGuitarParser" }; }
        }

        #endregion
    }
}