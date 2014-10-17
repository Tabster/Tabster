#region

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace Tabster.Controls.Extensions
{
    public static class TabsterSaveFileDialogExtensions
    {
        public static void SetTabsterFilter(this SaveFileDialog saveFileDialog, IEnumerable<ITablatureFileExporter> exporters, bool overwriteFilter = false)
        {
            var filterStringBuilder = new StringBuilder();

            var fileTypes = new List<FileType>();

            foreach (var exporter in exporters)
            {
                if (exporter.FileType != null && exporter.FileType.Extension != null)
                {
                    fileTypes.Add(exporter.FileType);
                }
            }
            for (var i = 0; i < fileTypes.Count; i++)
            {
                var fileType = fileTypes[i];

                var extensionString = string.Join(";*", fileType.Extensions.ToArray());

                filterStringBuilder.AppendFormat(string.Format("{0} (*{1})|*{1}", fileType.Name, extensionString));

                if (i + 1 < fileTypes.Count)
                    filterStringBuilder.Append("|");
            }

            if (filterStringBuilder.Length > 0)
            {
                if (overwriteFilter)
                {
                    saveFileDialog.Filter = filterStringBuilder.ToString();
                }

                else
                {
                    if (saveFileDialog.Filter.Length > 0)
                        filterStringBuilder.Insert(0, "|");

                    saveFileDialog.Filter += filterStringBuilder.ToString();
                }
            }
        }
    }
}