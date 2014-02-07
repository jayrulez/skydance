using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BillBox.Models.Repository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal Entities dbContext;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(Entities context)
        {
            this.dbContext = context != null ? context : new Entities();
            this.dbSet = this.dbContext.Set<TEntity>();

        }

        public TEntity GetByPk(object Pk)
        {
            TEntity entity;
            try
            {
                using (this.dbContext)
                {
                    entity = this.dbSet.Find(Pk);
                }
            }catch(Exception ex)
            {
                entity = null;
            }

            return entity;
        }
    }
}