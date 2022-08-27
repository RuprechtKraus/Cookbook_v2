using System;
using System.Threading.Tasks;
using Cookbook_v2.Domain.UoW.Interfaces;
using Cookbook_v2.Infrastructure.Data;

namespace Cookbook_v2.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CookbookContext _context;
        private bool _disposed = false;

        public UnitOfWork( CookbookContext context )
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( !_disposed )
            {
                if ( disposing )
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
