﻿#region

using System;
using System.IO;

#endregion

namespace Tabster.Core.Data
{
    public class TabsterDocumentProcessor<T> where T : class, ITabsterDocument, new()
    {
        private readonly Version _latestVersion;
        private readonly bool _updateFormat;
        private readonly bool _throwIfMissing;

        public TabsterDocumentProcessor(Version latestVersion, bool updateFormat, bool throwIfMissing)
        {
            _latestVersion = latestVersion;
            _updateFormat = updateFormat;
            _throwIfMissing = throwIfMissing;
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

            if (!_throwIfMissing && !File.Exists(fileName))
            {
                error = new FileNotFoundException();
                return null;
            }

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