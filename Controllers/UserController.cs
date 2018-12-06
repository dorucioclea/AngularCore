using System;
using AngularCore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AngularCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("[action]/{userId}")]
        public IActionResult GetUser(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if(user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet("[action]")]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }
    }
}