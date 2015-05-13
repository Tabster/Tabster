#region

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace Tabster.WinForms.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class TabsterOpenFileDialogExtensions
    {
        public static void SetTabsterFilter(this OpenFileDialog openFileDialog, IEnumerable<ITablatureFileImporter> importers, bool overwriteFilter = false, bool allSupportedTypesOption = true, bool alphabeticalOrder = false)
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

            if (alphabeticalOrder)
                fileTypes.Sort((f1, f2) => f1.Name.CompareTo(f2.Name));

            var last = fileTypes.Last();

            foreach (var fileType in fileTypes)
            {
                var filters = fileType.Extensions.Select(ext => "*" + ext).ToList();

                filterStringBuilder.AppendFormat("{0} ", fileType.Name);
                filterStringBuilder.AppendFormat("({0})", string.Join(",", filters.ToArray()));
                filterStringBuilder.Append("|");
                filterStringBuilder.AppendFormat(string.Join(";", filters.ToArray()));

                if (fileType != last)
                    filterStringBuilder.Append("|");
            }

            //insert pre-existing filter(s)
            if (!overwriteFilter)
            {
                filterStringBuilder.Insert(0, string.Format("{0}|", openFileDialog.Filter));
            }

            //add 'all supported files' option
            if (allSupportedTypesOption)
            {
                var filters = new List<string>();

                foreach (var fileType in fileTypes)
                {
                    filters.AddRange(fileType.Extensions.Select(ext => "*" + ext));
                }

                var allSupportedStringBuilder = new StringBuilder("All Supported Files ");

                allSupportedStringBuilder.AppendFormat("({0})", string.Join(",", filters.ToArray()));
                allSupportedStringBuilder.Append("|");
                allSupportedStringBuilder.AppendFormat(string.Join(";", filters.ToArray()));


                filterStringBuilder.Insert(0, allSupportedStringBuilder.ToString() + "|");
            }

            openFileDialog.Filter = filterStringBuilder.ToString();
        }
    }
}