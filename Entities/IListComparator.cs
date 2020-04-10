using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Lab2.Entities
{
    public interface IListComparator<T>
    {
        List<T> getNewComponents();
        List<T> getChangedComponents();
        List<ChangedObject> getChangeList(T ob, T oth);

        int GetUpdatedCount();
    }

}