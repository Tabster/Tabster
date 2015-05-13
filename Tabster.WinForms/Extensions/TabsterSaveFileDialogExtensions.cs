#region

using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace Tabster.WinForms.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class TabsterSaveFileDialogExtensions
    {
        public static void SetTabsterFilter(this SaveFileDialog saveFileDialog, IEnumerable<ITablatureFileExporter> exporters, bool overwriteFilter = false, bool alphabeticalOrder = false)
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

            if (alphabeticalOrder)
                fileTypes.Sort((f1, f2) => f1.Name.CompareTo(f2.Name));

            for (var i = 0; i < fileTypes.Count; i++)
            {
                var fileType = fileTypes[i];

                filterStringBuilder.AppendFormat("{0} (*{1})|*{1}", fileType.Name, fileType.Extensions[0]);

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