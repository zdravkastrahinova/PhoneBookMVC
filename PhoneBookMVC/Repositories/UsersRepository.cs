using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository() : base() { }

        public UsersRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}