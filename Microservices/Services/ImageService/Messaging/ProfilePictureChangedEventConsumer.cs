using AngularCore.Microservices.Services.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ImageService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService.Messaging
{
    public class ProfilePictureChangedEventConsumer : IConsumer<ProfilePictureChangedEvent>
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<User> _users;

        public ProfilePictureChangedEventConsumer(ApplicationContext context)
        {
            _users = context.Set<User>();
            _context = context;
        }

        public async Task Consume(ConsumeContext<ProfilePictureChangedEvent> eventContext)
        {
            var user = _users.Where(u => u.Id == eventContext.Message.UserId).FirstOrDefault();
            if (user != null)
            {
                user.ProfilePictureUrl = eventContext.Message.ProfilePictureUrl;
                _users.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
