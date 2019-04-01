using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularCore.Microservices.Services.Events;
using IdentityService.Data;
using IdentityService.ViewModels;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<User> _users;
        private readonly IBus _bus;

        public UsersController(ApplicationContext context, IBus bus)
        {
            _context = context;
            _users = context.Set<User>();
            _bus = bus;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _users.ToListAsync(); ;
        }

        [HttpGet("{id}")]
        public async Task<User> Get(Guid id)
        {
            return await _users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        [HttpGet("{id}/friends")]
        public async Task<IEnumerable<User>> GetUserFriends(Guid userId)
        {
            var user = await _users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if(user != null)
            {
                var userFriendIds = user.Friends.Select(uf => uf.FriendId).ToList();
                return await _users.Where(u => userFriendIds.Contains(u.Id)).ToListAsync();
            }
            return new List<User>();
        }

        [HttpPost("{id}/friends")]
        public async void AddUserFriend(Guid userId, [FromBody] AddFriend addFriend)
        {
            var user = await _users.Include(u => u.Friends).Where(u => u.Id == userId).FirstOrDefaultAsync();
            var friend = await _users.Include(f => f.Friends).Where(f => f.Id == addFriend.UserId).FirstOrDefaultAsync();
            if(user != null && friend != null 
                && !user.Friends.Exists(uf => uf.FriendId == friend.Id)
                && !friend.Friends.Exists(uf => uf.FriendId == userId))
            {
                user.Friends.Add(new UserFriend
                {
                    UserId = user.Id,
                    FriendId = friend.Id
                });
                friend.Friends.Add(new UserFriend
                {
                    UserId = friend.Id,
                    FriendId = user.Id
                });
                _users.Update(user);
                _users.Update(friend);
                await _context.SaveChangesAsync();
            }
        }

        [HttpDelete("{userId}/friends/{friendId}")]
        public async void RemoveUserFriend(Guid userId, Guid friendId)
        {
            var user = await _users.Include(u => u.Friends).Where(u => u.Id == userId).FirstOrDefaultAsync();
            var friend = await _users.Include(f => f.Friends).Where(f => f.Id == friendId).FirstOrDefaultAsync();
            if (user != null && friend != null 
                && user.Friends.Exists(uf => uf.FriendId == friendId)
                && friend.Friends.Exists(uf => uf.FriendId == userId))
            {
                var userFriend = user.Friends.Where(uf => uf.FriendId == friendId).FirstOrDefault();
                user.Friends.Remove(userFriend);

                userFriend = friend.Friends.Where(uf => uf.FriendId == userId).FirstOrDefault();
                friend.Friends.Remove(userFriend);
                _users.Update(user);
                _users.Update(friend);
                await _context.SaveChangesAsync();
            }
        }

        // TODO
        //[Authorize]
        [HttpPut("{id}")]
        public async void Put(Guid id, [FromBody] UserUpdate userUpdate)
        {
            var user = await _users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                user.FirstName = userUpdate.FirstName;
                user.LastName = userUpdate.LastName;
                _users.Update(user);
                await _context.SaveChangesAsync();
                await _bus.Publish(new UserUpdatedEvent
                {
                    UserId = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }
        }
        
        [HttpDelete("{id}")]
        public async void Delete(Guid id)
        {
            var user = await _users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if(user != null)
            {
                _users.Remove(user);
                await _context.SaveChangesAsync();
                await _bus.Publish(new UserDeletedEvent
                {
                    UserId = id,
                });
            }
        }
    }
}
