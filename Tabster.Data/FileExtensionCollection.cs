#region

using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace Tabster.Data
{
    public sealed class FileExtensionCollection : Collection<string>
    {
        public FileExtensionCollection(IList<string> extensions) : base(extensions)
        {
        }

        public FileExtensionCollection(string extension)
            : this(new[] {extension})
        {
        }
    }
}