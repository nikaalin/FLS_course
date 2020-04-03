using System.Collections.Generic;
using System.Windows.Documents;
using Lab2.Entities;

namespace Lab2.IDAO
{
    public interface IDao<T>
    {
        void Update(T t);
        void Remove(T t);
        void Remove(int id);
        void Create(T t);
        Danger Get(int id);
        List<T> GetAll();
        void Commit();
    }
}