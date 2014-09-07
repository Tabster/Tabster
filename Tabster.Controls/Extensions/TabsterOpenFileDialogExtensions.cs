#region

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Tabster.Core.Data;
using Tabster.Core.Data.Processing;

#endregion

namespace Tabster.Controls.Extensions
{
    public static class TabsterOpenFileDialogExtensions
    {
        public static void SetTabsterFilter(this OpenFileDialog openFileDialog, IEnumerable<ITablatureFileImporter> importers)
        {
            var filterStringBuilder = new StringBuilder();

            var fileTypes = new List<FileType>();

            foreach (var importer in importers)
            {
                if (importer.FileType != null && importer.FileType.Extension != null)
                {
                    fileTypes.Add(importer.FileType);
                }
            }

            for (var i = 0; i < fileTypes.Count; i++)
            {
                var fileType = fileTypes[i];

                var extensionString = string.Join(";", fileType.Extensions.ToArray());

                filterStringBuilder.AppendFormat(string.Format("{0} (*{1})|*{1}", fileType.Name, extensionString));

                if (i + 1 < fileTypes.Count)
                    filterStringBuilder.Append("|");
            }

            if (filterStringBuilder.Length > 0)
                openFileDialog.Filter = filterStringBuilder.ToString();
        }
    }
}