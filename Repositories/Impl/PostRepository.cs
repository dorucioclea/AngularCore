using System;
using System.Linq;
using System.Linq.Expressions;
using AngularCore.Data.Contexts;
using AngularCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularCore.Repositories.Impl
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationContext context) : base(context) {}

        public override IQueryable<Post> GetAll()
        {
            return Entity.Include( p => p.Author )
                            .ThenInclude( a => a.ProfilePicture)
                         .Include( p => p.WallOwner )
                            .ThenInclude( wo => wo.ProfilePicture)
                            .AsQueryable();
        }

        public override IQueryable<Post> GetWhere(Expression<Func<Post, bool>> predicate)
        {
            return Entity.Include(p => p.Author)
                            .ThenInclude(a => a.ProfilePicture)
                         .Include(p => p.WallOwner)
                            .ThenInclude(wo => wo.ProfilePicture)
                            .Where(predicate)
                            .AsQueryable();
        }
    }
}