using AngularCore.Microservices.Services.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SearchService.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SearchService.Messaging
{
    public class UserAddedEventConsumer : IConsumer<UserAddedEvent>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<User> _users;

        public UserAddedEventConsumer(ApplicationContext context)
        {
            _users = context.Set<User>();
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserAddedEvent> eventContext)
        {
            var userCheck = _users.Where(u => u.Id == eventContext.Message.UserId).FirstOrDefault();
            if(userCheck == null)
            {
                await _users.AddAsync(new User
                {
                    Name = eventContext.Message.FirstName,
                    Surname = eventContext.Message.LastName
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
