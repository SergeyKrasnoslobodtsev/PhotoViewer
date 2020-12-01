using Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        DataContext _context;
        DbSet<T> _dbSet;

        public BaseRepository(DataContext context) {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public IEnumerable<T> GetAll() {
            return _dbSet.AsNoTracking();
        }
        public void Add(T item) {
            _dbSet.Add(item);
        }
        public T FindById(int id) {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> Get(Func<T, bool> predicate) {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public void Remove(T item) {
            _dbSet.Remove(item);
        }

        public void Update(T item) {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
