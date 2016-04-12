using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Services.ModelServices
{
    public class BaseService<T> where T : BaseModel, new()
    {
        private readonly BaseRepository<T> baseRepo;
        protected UnitOfWork unitOfWork;

        public BaseService()
        {
            this.unitOfWork = new UnitOfWork();
            this.baseRepo = new BaseRepository<T>(this.unitOfWork);
        }

        public BaseService(UnitOfWork unitOfWork) : this()
        {
            this.unitOfWork = unitOfWork;
            this.baseRepo = new BaseRepository<T>(this.unitOfWork);
        }

        public List<T> GetAll()
        {
            return this.baseRepo.GetAll();
        }

        public T GetByID(int id)
        {
            return this.baseRepo.GetByID(id);
        }

        public void Save(T item)
        {
            try
            {
                this.baseRepo.Save(item);
                this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.unitOfWork.RollBack();
            }
        }

        public void Delete(int id)
        {
            try
            {
                this.baseRepo.Delete(id);
                this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.unitOfWork.RollBack();
            }
        }
    }
}