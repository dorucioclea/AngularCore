using AngularCore.Microservices.Services.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PostService.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Messaging
{
    public class UserDeletedEventConsumer : IConsumer<UserDeletedEvent>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Post> _posts;

        public UserDeletedEventConsumer(ApplicationContext context)
        {
            _posts = context.Set<Post>();
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserDeletedEvent> eventContext)
        {
            var userId = eventContext.Message.UserId;
            var postsToDelete = await _posts.Where(p => p.AuthorId == userId || p.WallOwnerId == userId).ToListAsync();
            if (postsToDelete != null && postsToDelete.Count > 0)
            {
                _posts.RemoveRange(postsToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
