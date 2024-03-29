﻿using ContextConnection.ContextDB;
using Infrastructure.DBFactory;
using System.Data.Entity;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private Context _dbContext;
        private DbContextTransaction _tranContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public Context DbContext
        {
            get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        }

        public virtual void CommitChange()
        {
            DbContext.SaveChanges();
        }

        public virtual void BeginTran()
        {
            _tranContext = DbContext.Database.BeginTransaction();
        }

        public virtual void CommitTran()
        {
            _tranContext.Commit();
            _tranContext.Dispose();
        }

        public virtual void RollbackTran()
        {
            _tranContext.Rollback();
            _tranContext.Dispose();
        }
    }
}