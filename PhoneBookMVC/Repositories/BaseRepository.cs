using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Repositories
{
    public class BaseRepository<T> where T : BaseModel
    {
        private readonly AppContext context;
        private readonly DbSet<T> dbSet;
        protected UnitOfWork unitOfWork;

        public BaseRepository()
        {
            this.context = new AppContext();
            this.dbSet = context.Set<T>();
        }

        public BaseRepository(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.context = this.unitOfWork.Context;
            this.dbSet = this.context.Set<T>();
        }

        public T GetByID(int id)
        {
            return this.dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return this.dbSet.ToList();
        }

        private void Insert(T item)
        {
            this.dbSet.Add(item);
        }

        private void Update(T item)
        {
            this.context.Entry(item).State = EntityState.Modified;
        }

        public void Save(T item)
        {
            if (item.ID == 0)
            {
                Insert(item);
            }
            else
            {
                Update(item);
            }
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            this.dbSet.Remove(GetByID(id));
            this.context.SaveChanges();
        }
    }
}