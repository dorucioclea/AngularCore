using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AngularCore.Data.ViewModels;
using AngularCore.Data.Models;
using System;
using System.Linq;
using AngularCore.Repositories;
using AngularCore.Services;
using Microsoft.AspNetCore.Authorization;

namespace AngularCore.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private IUserRepository _userRepository;
        private IAuthService _authService;

        public AuthController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        public IActionResult Login([FromBody] LoginForm form)
        {
            User userFound = _userRepository.GetWhere( u => u.Email == form.Email && u.Password == form.Password ).FirstOrDefault();
            if( userFound == null )
            {
                return BadRequest( new ErrorMessage("Incorrect credentials") );
            }
            return Ok(GenerateLoginResponse(userFound));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(LoginResponse), 201)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        public IActionResult Register([FromBody] RegisterForm form)
        {
            User user = _userRepository.GetWhere( u => u.Email.Equals(form.Email)).FirstOrDefault();
            if(user != null || !form.Password.Equals(form.PasswordCheck))
            {
                string message = (user != null ? "This mail is already taken." : "Passwords don't match.");
                return BadRequest(new ErrorMessage(message));
            }

            User newUser = new User {
                Id = Guid.NewGuid().ToString(),
                Name = form.Name,
                Surname = form.Surname,
                Email = form.Email,
                Password = form.Password
            };
            _userRepository.Add(newUser);

            return CreatedAtAction("Register", GenerateLoginResponse(newUser));
        }

        [Authorize]
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        public LoginResponse RenewSession()
        {
            User currentUser = _userRepository.GetById(User.Identity.Name);
            LoginResponse renewalResponse = GenerateLoginResponse(currentUser);
            return renewalResponse;
        }

        private LoginResponse GenerateLoginResponse(User user)
        {
            LoggedUser loggedUser = new LoggedUser(){
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };

            return new LoginResponse(){
                User = loggedUser,
                JwtToken = _authService.GenerateJWTToken(user),
                ExpiresIn = TimeSpan.FromDays(_authService.TokenValidityPeriod).TotalSeconds.ToString()
            };
        }

    }
}