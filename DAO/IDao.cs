using System.Collections.Generic;
using System.Windows.Documents;

namespace Lab2.DAO
{
    public interface IDao<T>
    {
        void Update(T t);
        void Remove(T t);
        void Remove(int id);
        void Create(T t);
        T Get(int id);
        List<T> GetAll();
        void Commit();
    }
}