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
        public UserRepository(ApplicationContext context) : base(context) { }

        public override User GetById(string id)
        {
            return Entity
                .Include( u => u.FriendUsers )
                    .ThenInclude( uf => uf.User )
                .Include( u => u.UserFriends )
                    .ThenInclude( fu => fu.Friend )
                .Include( u => u.Posts )
                .SingleOrDefault( u => u.Id.Equals(id) );
        }
    }
}