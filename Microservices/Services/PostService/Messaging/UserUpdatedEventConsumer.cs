using MassTransit;
using AngularCore.Microservices.Services.Events;
using System.Linq;
using System.Threading.Tasks;
using PostService.Data;
using Microsoft.EntityFrameworkCore;

namespace PostService.Messaging
{
    public class UserUpdatedEventConsumer : IConsumer<UserUpdatedEvent>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<User> _users;

        public UserUpdatedEventConsumer(ApplicationContext context)
        {
            _users = context.Set<User>();
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserUpdatedEvent> eventContext)
        {
            var user = _users.Where(u => u.Id == eventContext.Message.UserId).FirstOrDefault();
            if (user != null)
            {
                user.FirstName = eventContext.Message.FirstName;
                user.LastName = eventContext.Message.LastName;

                _users.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
