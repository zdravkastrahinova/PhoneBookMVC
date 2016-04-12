using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Services.ModelServices
{
    public class GroupsService : BaseService<Group>
    {
        public GroupsService() : base() { }

        public GroupsService(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}