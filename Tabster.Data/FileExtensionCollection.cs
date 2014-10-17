#region

using System.Collections.Generic;
using Tabster.Core.Utilities;

#endregion

namespace Tabster.Data
{
    public sealed class FileExtensionCollection : CustomTypeCollection<string>
    {
        public FileExtensionCollection(IEnumerable<string> extensions) : base(extensions)
        {
        }

        public FileExtensionCollection(string extension)
            : this(new[] {extension})
        {
        }
    }
}