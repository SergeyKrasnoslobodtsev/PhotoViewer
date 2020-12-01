using System;
using System.Collections.Generic;

namespace Data.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T item);
        T FindById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Remove(T item);
        void Update(T item);
    }
}
