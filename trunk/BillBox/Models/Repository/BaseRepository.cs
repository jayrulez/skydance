using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillBox.Models.Repository
{
    public abstract class BaseRepository : IDisposable
    {
        protected Entities dbContext;

        protected bool disposed = false;

        public BaseRepository(Entities context = null)
        {
            this.dbContext = context != null ? context : new Entities();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}