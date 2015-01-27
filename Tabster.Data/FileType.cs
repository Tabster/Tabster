#region

using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace Tabster.Data
{
    public sealed class FileType
    {
        private readonly List<string> _extensions = new List<string>();

        public FileType(string name, IEnumerable<string> extensions)
        {
            Name = name;
            _extensions.AddRange(extensions);
        }

        public FileType(string name, string extension)
            : this(name, new[] {extension})
        {
        }

        public string Name { get; set; }

        public ReadOnlyCollection<string> Extensions
        {
            get { return _extensions.AsReadOnly(); }
        }

        public string Extension
        {
            get { return Extensions.Count > 0 ? Extensions[0] : null; }
        }
    }
}