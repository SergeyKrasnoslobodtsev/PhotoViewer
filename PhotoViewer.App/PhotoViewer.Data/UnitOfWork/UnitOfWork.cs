using Data.Model;
using Data.Repository;
using System;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        DataContext _context;

        public UnitOfWork() {
            _context = new DataContext();
        }
        private PhotoRepository photoRepository;
        public PhotoRepository Photos {
            get {
                if (photoRepository == null)
                    photoRepository = new PhotoRepository(_context);
                return photoRepository;
            }
        }

        public void Commit() {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
