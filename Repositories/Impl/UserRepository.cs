using System;
using System.Collections.Generic;
using System.Linq;
using AngularCore.Data.Contexts;
using AngularCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularCore.Repositories.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) {}

        public override User GetById(string id)
        {
            return Entity
                .Include( u => u.FriendUsers )
                .Include( u => u.UserFriends )
                .SingleOrDefault( u => u.Id.Equals(id) );
        }
    }
}