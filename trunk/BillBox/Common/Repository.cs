﻿using BillBox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace BillBox.Common
{
    public class Repository<TEntity> where TEntity : class
    {
        internal Entities dbContext;
        internal DbSet<TEntity> dbSet;


        public Repository(Entities context = null)
        {
            this.dbContext = context != null ? context : new Entities();
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        //public static IEnumerable<TEntity> GetAll()
        //{
        //    Entities context = new Entities();
        //    DbSet<TEntity> _dbSet = context.Set<TEntity>();
        //    return _dbSet.ToList<TEntity>();
        //}

        public IEnumerable<TEntity> GetAll()
        {
            return this.dbSet.ToList<TEntity>();
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
            }
            catch (Exception)
            {
                entity = null;
            }


            return entity;
        }
    }
}