#region

using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace Tabster.Data
{
    public sealed class FileType
    {
        public readonly Collection<string> Extensions;

        public FileType(string name, IList<string> extensions)
        {
            Name = name;
            Extensions = new Collection<string>(extensions);
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