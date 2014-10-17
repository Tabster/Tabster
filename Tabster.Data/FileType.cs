#region

using System.Collections.Generic;

#endregion

namespace Tabster.Data
{
    public sealed class FileType
    {
        public readonly FileExtensionCollection Extensions;

        public FileType(string name, IEnumerable<string> extensions)
        {
            Name = name;
            Extensions = new FileExtensionCollection(extensions);
        }

        public FileType(string name, string extension)
            : this(name, new[] {extension})
        {
        }

        public string Name { get; set; }

        public string Extension
        {
            get { return Extensions.Count > 0 ? Extensions[0] : null; }
        }
    }
}