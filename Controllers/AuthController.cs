using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AngularCore.Data.ViewModels;
using AngularCore.Data.Models;
using System;
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
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] LoginForm form)
        {
            User userFound = _userRepository.GetAllUsers().Find( u => u.Email == form.Email && u.Password == form.Password );
            if( userFound == null )
            {
                return BadRequest("Incorrect credentials");
            }
            return Ok(GenerateLoginResponse(userFound));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(LoginResponse), 201)]
        [ProducesResponseType(400)]
        public IActionResult Register([FromBody] RegisterForm form)
        {
            User user = _userRepository.GetUserByEmail(form.Email);
            if(user != null || !form.Password.Equals(form.PasswordCheck))
            {
                String message = (user != null ? "This mail is already taken." : "Passwords don't match.");
                return BadRequest(message);
            }

            user = _userRepository.AddUser(new User(
                name: form.Name,
                surname: form.Surname,
                email: form.Email,
                password: form.Password
            ));

            return CreatedAtAction("Register", GenerateLoginResponse(user));
        }

        [Authorize]
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        public LoginResponse RenewSession()
        {
            var currentUser = _userRepository.GetUserById(User.Identity.Name);
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