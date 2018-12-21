using System;
using System.Linq;
using System.Collections.Generic;
using AngularCore.Data.ViewModels;
using AngularCore.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AngularCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularCore.Controllers
{
    [Authorize]
    [Route("api/v1/users")]
    public class UserController : Controller
    {

        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<UserVM>), 200)]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return Ok(_mapper.Map<List<UserVM>>(users));
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(DetailedUserVM), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetUser(string userId)
        {
            var user = _userRepository.GetById(userId);
            if(user == null)
            {
                return NotFound();
            }
            var mapped = _mapper.Map<DetailedUserVM>(user);
            return Ok(mapped);
        }

        [HttpGet("{userId}/friends")]
        [ProducesResponseType(typeof(FriendUserVM), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 404)]
        public IActionResult GetUserFriends(string userId)
        {
            var user = _userRepository.GetById(userId);
            if( user == null )
            {
                return NotFound(new ErrorMessage("User was not found"));
            }
            var friends = user.Friends.ToList();
            return Ok(_mapper.Map<List<UserVM>>(friends));
        }

        [HttpPost("{userId}/friends")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorMessage), 404)]
        public IActionResult AddFriend(string userId, [FromBody] string friendId)
        {
            if( userId == friendId )
            {
                return Ok();
            }

            var user = _userRepository.GetById(userId);
            if( user == null )
            {
                return NotFound(new ErrorMessage("User was not found"));
            }
            var friend = _userRepository.GetById(friendId);
            if( friend == null )
            {
                return NotFound(new ErrorMessage("Friend user was not found"));
            }

            if( !user.Friends.Contains(friend) )
            {
                user.AddFriend(friend);
                _userRepository.Update(user);
            }

            return Ok();
        }

        [HttpDelete("{userId}/friends/{friendId}")]
        [ProducesResponseType(204)]
        public IActionResult RemoveFriend(string userId, string friendId)
        {
            var user = _userRepository.GetById(userId);
            var friend = user.Friends.Find( f => f.Id == friendId);
            if( friend == null )
            {
                return NoContent();
            }

            user.RemoveFriend(friend);
            _userRepository.Update(user);
            return NoContent();
        }

        [HttpGet("{userId}/posts")]
        [ProducesResponseType(typeof(List<PostVM>), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 404)]
        public IActionResult GetUserPosts(string userId)
        {
            var user = _userRepository.GetById(userId);
            if(user == null)
            {
                return NotFound(new ErrorMessage("User not found"));
            }
            return Ok(_mapper.Map<List<PostVM>>(user.Posts));
        }

        [HttpPost("{userId}/posts")]
        [ProducesResponseType(typeof(Post), 201)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        public IActionResult AddUserPost(string userId, [FromBody] PostForm form)
        {
            var owner = _userRepository.GetById(userId);
            if( owner == null ) {
                return BadRequest(new ErrorMessage("User does not exist"));
            }

            Post post = new Post {
                Content = form.Content
            };
            owner.Posts.Add(post);
            _userRepository.Update(owner);
            return Created($"/users/{userId}/posts/{post.Id}", _mapper.Map<PostVM>(post));
        }
    }
}