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
            var postsToDelete = await _posts.Include("Author").Include("WallOwner")
                                            .Where(p => p.Author.Id == userId || p.WallOwner.Id == userId).ToListAsync();
            if (postsToDelete != null && postsToDelete.Count > 0)
            {
                _posts.RemoveRange(postsToDelete);
            }
            var userToDelete = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
            }
            await _context.SaveChangesAsync();
        }
    }
}
