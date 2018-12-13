using System;
using System.Collections.Generic;
using AngularCore.Data.ViewModels;
using AngularCore.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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
            var user = _userRepository.GetUserById(userId);
            if(user != null)
            {
                return Ok(_mapper.Map<UserVM>(user));
            }
            return NotFound();
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<UserVM>), 200)]
        [ProducesResponseType(204)]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
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
            var user = _userRepository.GetUserById(User.Identity.Name);
            if( user == null )
            {
                return BadRequest(new ErrorMessage("Logged user is incorrect"));
            }

            var friend = _userRepository.GetUserById(friendVM.Id);
            if( friend == null )
            {
                return NotFound(new ErrorMessage("User was not found"));
            }

            user.Friends.Add(friend);
            _userRepository.UpdateUser(user.Id, user);
            return Ok();
        }
    }
}