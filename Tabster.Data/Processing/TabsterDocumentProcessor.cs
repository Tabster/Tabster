#region

using System;

#endregion

namespace Tabster.Data.Processing
{
    public class TabsterFileProcessor<T> where T : class, ITabsterFile, new()
    {
        private readonly Version _latestVersion;

        public TabsterFileProcessor(Version latestVersion)
        {
            _latestVersion = latestVersion;
        }

        public T Load(string fileName, bool update = false)
        {
            Exception error;
            return Load(fileName, out error, update);
        }

        private T Load(string fileName, out Exception error, bool update = false)
        {
            var file = new T();

            try
            {
                var header = file.Load(fileName);

                if (update && header.Version < _latestVersion)
                {
                    //todo implement file updating
                }

                error = null;
                return file;
            }

            catch (Exception ex)
            {
                error = ex;
                return null;
            }
        }
    }
}