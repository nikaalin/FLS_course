using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Lab2.DataSource
{
    public interface IDSManager<T>
    {
        void Create();
        void UpdateFromRemote();
        void RewriteDataFromList(List<T> obs);
        void Delete();
        void Repair();
        bool ExistLocal();
        void SaveLocal(string path);
        List<T> GetSourceAsList();
        List<T> GetOldSourceAsList();
    }
}