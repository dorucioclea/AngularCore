using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientGateway.Models;
using ClientGateway.Services;
using ClientGateway.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientGateway.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersApiService _usersService;

        public UsersController(IUsersApiService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _usersService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        
        [HttpGet("{userId}/friends")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> GetUserFriends(Guid userId)
        {
            var friends = await _usersService.GetUserFriends(userId);
            return Ok(friends);
        }

        [HttpPost("{userId}/friends")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddFriend(Guid userId, [FromBody] AddFriend addFriend)
        {
            try
            {
                _usersService.AddUserFriend(userId, addFriend);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{userId}/friends/{friendId}")]
        [ProducesResponseType(204)]
        public IActionResult RemoveFriend(Guid userId, Guid friendId)
        {
            _usersService.RemoveUserFriend(userId, friendId);
            return NoContent();
        }
    }
}