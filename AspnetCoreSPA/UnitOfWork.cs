using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Domain;
using AspnetCoreSPATemplate.Repositories;
using AspnetCoreSPATemplate.Repositories.Impl;

namespace AspnetCoreSPATemplate
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
        }

        public IContactRepository ContactRepository => new ContactRepository(_context);


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
