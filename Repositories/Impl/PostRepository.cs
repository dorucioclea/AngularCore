using AngularCore.Data.Contexts;
using AngularCore.Data.Models;

namespace AngularCore.Repositories.Impl
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationContext context) : base(context) {}
    }
}