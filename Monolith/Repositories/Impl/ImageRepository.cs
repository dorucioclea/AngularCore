using System.Linq;
using AngularCore.Data.Contexts;
using AngularCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularCore.Repositories.Impl
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationContext context) : base(context) {}

        public override IQueryable<Image> GetAll()
        {
            return Entity
                    .Include(i => i.Author)
                        .ThenInclude(a => a.ProfilePicture).AsQueryable();
        }
    }
}