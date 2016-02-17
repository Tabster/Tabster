#region

using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Tabster.Data.Processing;

#endregion

namespace Tabster.WinForms.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class TabsterSaveFileDialogExtensions
    {
        public static List<TabsterSaveFileDialogFilter> SetTabsterFilter(this SaveFileDialog saveFileDialog, IEnumerable<ITablatureFileExporter> exporters, bool overwriteFilter = false, bool alphabeticalOrder = false)
        {
            var filters = new List<TabsterSaveFileDialogFilter>();
            var filterStringBuilder = new StringBuilder();

            foreach (var exporter in exporters)
            {
                if (exporter.FileType != null && exporter.FileType.Extension != null)
                {
                    filters.Add(new TabsterSaveFileDialogFilter(exporter));
                }
            }

            if (alphabeticalOrder)
                filters.Sort((f1, f2) => f1.Exporter.FileType.Name.CompareTo(f2.Exporter.FileType.Name));

            for (var i = 0; i < filters.Count; i++)
            {
                var filter = filters[i];

                filterStringBuilder.Append(filter.GetFilterString());

                if (i + 1 < filters.Count)
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

            return filters;
        }

        public class TabsterSaveFileDialogFilter
        {
            private string _filterString;

            public TabsterSaveFileDialogFilter(ITablatureFileExporter exporter)
            {
                Exporter = exporter;
            }

            public ITablatureFileExporter Exporter { get; private set; }

            public string GetFilterString()
            {
                return _filterString ?? (_filterString = string.Format("{0} (*{1})|*{1}", Exporter.FileType.Name, Exporter.FileType.Extensions[0]));
            }
        }
    }
}