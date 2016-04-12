using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Repositories
{
    public class GroupsRepository : BaseRepository<Group>
    {
        public GroupsRepository() : base() { }

        public GroupsRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}