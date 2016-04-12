using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Repositories
{
    public class UnitOfWork : IDisposable
    {
        public AppContext Context { get; set; }
        private DbContextTransaction transaction = null;

        public UnitOfWork()
        {
            this.Context = new AppContext();
            this.transaction = this.Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (this.transaction != null)
            {
                this.transaction.Commit();
                this.transaction = null;
            }
        }

        public void RollBack()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
                this.transaction = null;
            }
        }

        public void Dispose()
        {
            Commit();
            Context.Dispose();
        }
    }
}