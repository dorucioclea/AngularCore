using System;
using System.Linq;
using System.Collections.Generic;
using AngularCore.Data.ViewModels;
using AngularCore.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AngularCore.Data.Models;

namespace AngularCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("[action]/{userId}")]
        [ProducesResponseType(typeof(UserVM), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetUser(string userId)
        {
            var user = _userRepository.GetById(userId);
            var friends = user.Friends;
            if(user != null)
            {
                var mapped = _mapper.Map<UserVM>(user);
                return Ok(mapped);
            }
            return NotFound();
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<UserVM>), 200)]
        [ProducesResponseType(204)]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAll().ToList();
            if( users == null || users.Count == 0) {
                return NoContent();
            }
            return Ok(_mapper.Map<List<UserVM>>(users));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        [ProducesResponseType(typeof(ErrorMessage), 404)]
        public IActionResult AddFriend([FromBody] UserVM friendVM)
        {
            if( friendVM.Id == User.Identity.Name )
            {
                return BadRequest(new ErrorMessage("Cannot be friends with yourself :("));
            }

            var user = _userRepository.GetById(User.Identity.Name);
            if( user == null )
            {
                return BadRequest(new ErrorMessage("Logged user is incorrect"));
            }

            var friend = _userRepository.GetById(friendVM.Id);
            if( friend == null )
            {
                return NotFound(new ErrorMessage("User was not found"));
            }
            if( user.Friends.Contains(friend))
            {
                return BadRequest(new ErrorMessage("User is already in the friendlist"));
            }

            user.AddFriend(friend);
            _userRepository.Update(user);
            return Ok();
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        [ProducesResponseType(200)]
        public IActionResult RemoveFriend([FromBody] FriendUserVM friendVM)
        {
            var user = _userRepository.GetById(User.Identity.Name);
            var friend = user.Friends.Find( f => f.Id == friendVM.Id);
            if( friend == null )
            {
                return BadRequest(new ErrorMessage("User not in the friendlist"));
            }

            user.UserFriends.RemoveAll( uf => uf.FriendId == friend.Id);
            user.FriendUsers.RemoveAll( uf => uf.FriendId == friend.Id);
            _userRepository.Update(user);
            return Ok();
        }

        [HttpGet("[action]/{userId}")]
        [ProducesResponseType(typeof(FriendUserVM), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 404)]
        [ProducesResponseType(204)]
        public IActionResult GetUserFriends(string userId)
        {
            var user = _userRepository.GetById(userId);
            if( user == null )
            {
                return NotFound(new ErrorMessage("User was not found"));
            }
            var friends = user.Friends;
            if( !friends.Any() )
            {
                return NoContent();
            }
            return Ok(_mapper.Map<List<FriendUserVM>>(friends));
        }
    }
}