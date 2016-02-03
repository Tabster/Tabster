#region

using System;

#endregion

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
        ///     Plugin version.
        /// </summary>
        Version Version { get; }

        /// <summary>
        ///     Plugin website.
        /// </summary>
        Uri Website { get; }

        /// <summary>
        ///     Plugin activation.
        /// </summary>
        void Activate();

        /// <summary>
        ///     Plugin deactivation.
        /// </summary>
        void Deactivate();

        /// <summary>
        ///     Plugin initialization.
        /// </summary>
        void Initialize();
    }
}