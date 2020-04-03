using System.Collections.Generic;

namespace Lab2.DataSource
{
    public interface IDSManager<T>
    {
        void Create();
        void UpdateFromRemote();
        void RewriteDataFromList(List<T> obs);
        void Delete();
        void Repair();

        List<T> GetSourceAsList();
    }
}