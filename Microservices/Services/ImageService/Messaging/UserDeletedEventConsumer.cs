using AngularCore.Microservices.Services.Events;
using ImageService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService.Messaging
{
    public class UserDeletedEventConsumer : IConsumer<UserDeletedEvent>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Image> _images;

        public UserDeletedEventConsumer(ApplicationContext context)
        {
            _images = context.Set<Image>();
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserDeletedEvent> eventContext)
        {
            var userId = eventContext.Message.UserId;
            var imagesToDelete = await _images.Where(i => i.AuthorId == userId).ToListAsync();
            if (imagesToDelete != null && imagesToDelete.Count > 0)
            {
                _images.RemoveRange(imagesToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
