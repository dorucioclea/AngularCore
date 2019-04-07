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
            var imagesToDelete = await _images.Include("Author").Where(i => i.Author.Id == userId).ToListAsync();
            if (imagesToDelete != null && imagesToDelete.Count > 0)
            {
                _images.RemoveRange(imagesToDelete);
            }
            var userToDelete = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if(userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
            }
            await _context.SaveChangesAsync();
        }
    }
}
