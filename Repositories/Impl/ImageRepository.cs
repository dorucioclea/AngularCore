using AngularCore.Data.Contexts;
using AngularCore.Data.Models;

namespace AngularCore.Repositories.Impl
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationContext context) : base(context) {}
    }
}