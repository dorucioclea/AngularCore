using AngularCore.Microservices.Services.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SearchService.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.Messaging
{
    public class UserDeletedEventConsumer : IConsumer<UserDeletedEvent>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<User> _users;

        public UserDeletedEventConsumer(ApplicationContext context)
        {
            _users = context.Set<User>();
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserDeletedEvent> eventContext)
        {
            var userToDelete = _users.Where(u => u.Id == eventContext.Message.UserId).FirstOrDefault();
            if (userToDelete == null)
            {
                _users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
