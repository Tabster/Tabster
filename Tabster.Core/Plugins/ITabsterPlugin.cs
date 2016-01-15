using System;

namespace Tabster.Core.Plugins
{
    public interface ITabsterPlugin
    {
        /// <summary>
        ///     Plugin author.
        /// </summary>
        string Author { get; }

        /// <summary>
        ///     Plugin copyright.
        /// </summary>
        string Copyright { get; }

        /// <summary>
        ///     Plugin description.
        /// </summary>
        string Description { get; }

        /// <summary>
        ///     Plugin display name.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        ///     Publicly exposed plugin types.
        /// </summary>
        Type[] Types { get; }

        /// <summary>
        ///     Plugin version.
        /// </summary>
        Version Version { get; }

        /// <summary>
        ///     Plugin website.
        /// </summary>
        Uri Website { get; }

        void Activate();
        void Deactivate();

    }
}