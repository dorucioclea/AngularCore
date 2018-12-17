using System.Linq;
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
            return Entity.Include( p => p.User ).AsQueryable();
        }
    }
}