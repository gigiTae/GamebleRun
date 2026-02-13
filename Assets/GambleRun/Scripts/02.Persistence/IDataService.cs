
using System.Collections.Generic;

namespace GambleRun.Persistence
{
    public interface IDataService
    {
        void Save<TData>(string name, TData data, bool overwrite = true);

        TData Load<TData>(string name);

        void Delete(string name);

        void DeleteAll();
    }
}