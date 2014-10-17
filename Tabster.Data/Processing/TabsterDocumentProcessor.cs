#region

using System;

#endregion

namespace Tabster.Data.Processing
{
    public class TabsterDocumentProcessor<T> where T : class, ITabsterDocument, new()
    {
        private readonly Version _latestVersion;
        private readonly bool _updateFormat;

        public TabsterDocumentProcessor(Version latestVersion, bool updateFormat)
        {
            _latestVersion = latestVersion;
            _updateFormat = updateFormat;
        }

        public Exception Error { get; private set; }

        public T Load(string fileName)
        {
            Exception error;
            return Load(fileName, out error);
        }

        public T Load(string fileName, out Exception error)
        {
            Error = null;

            var doc = new T();

            try
            {
                doc.Load(fileName);

                if (_latestVersion > doc.FileVersion && _updateFormat)
                {
                    doc.Update();
                }

                error = null;
                return doc;
            }

            catch (Exception ex)
            {
                error = ex;
                Error = ex;
                return null;
            }
        }
    }
}