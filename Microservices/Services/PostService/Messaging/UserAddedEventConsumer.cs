using AngularCore.Microservices.Services.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PostService.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Messaging
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
                    Id = eventContext.Message.UserId,
                    FirstName = eventContext.Message.FirstName,
                    LastName = eventContext.Message.LastName,
                    ProfilePictureUrl = eventContext.Message.ProfilePictureUrl
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
