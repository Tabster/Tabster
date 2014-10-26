#region

using System.IO;
using System.Linq;

#endregion

namespace Tabster.Utilities.IO
{
    public static class FileHeaderChecker
    {
        public static bool Check(string fileName, byte[] header, int startIndex = 0, bool throwOnMissing = false)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open))
                {
                    var buffer = new byte[header.Length];
                    fs.Read(buffer, startIndex, header.Length);

                    return buffer.SequenceEqual(header);
                }
            }

            catch (FileNotFoundException)
            {
                if (throwOnMissing)
                    throw;

                return false;
            }
        }
    }
}