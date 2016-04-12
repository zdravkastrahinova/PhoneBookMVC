using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using PhoneBookMVC.ViewModels.AccountVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Services.ModelServices
{
    public class UsersService : BaseService<User>
    {
        public UsersService() : base() { }

        public UsersService(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool IsUserExsits(AccountRegisterVM model)
        {
            User verifyUser = new UsersRepository().GetAll().FirstOrDefault((u => u.Username.ToLower() == model.Username.ToLower() || u.Email == model.Email));
            if (verifyUser == null)
            {
                return false;
            }
            return true;
        }
    }
}