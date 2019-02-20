using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                .Include(u => u.FriendUsers)
                    .ThenInclude(uf => uf.User)
                        .ThenInclude(u => u.ProfilePicture)
                .Include(u => u.UserFriends)
                    .ThenInclude(fu => fu.Friend)
                        .ThenInclude(u => u.ProfilePicture)
                .Include( u => u.Posts )
                .Include( u => u.WallPosts )
                .Include( u => u.Images )
                .SingleOrDefault( u => u.Id.Equals(id) );
        }

        public override IQueryable<User> GetWhere(Expression<Func<User, bool>> predicate)
        {
            return Entity.Include(u => u.ProfilePicture).Where(predicate);
        }

        public override IQueryable<User> GetAll()
        {
            return Entity.Include(u => u.ProfilePicture).AsQueryable();
        }
    }
}