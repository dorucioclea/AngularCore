using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.Data;
using AngularCore.Microservices.Services.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityService.ViewModels;
using IdentityService.Services;
using Microsoft.AspNetCore.Authorization;

namespace IdentityService.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DbSet<User> _users;
        private readonly ApplicationContext _context;
        private readonly AuthService _authService;
        private readonly IBus _bus;

        public AuthController(ApplicationContext context, AuthService authService, IBus bus)
        {
            _users = context.Set<User>();
            _context = context;
            _authService = authService;
            _bus = bus;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(SessionResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] LoginForm form)
        {
            User userFound = _users.Where(u => u.Email == form.Email && u.Password == form.Password)
                                            .FirstOrDefault();
            if (userFound == null)
            {
                return BadRequest();
            }
            return Ok(GenerateLoginResponse(userFound));
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(SessionResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegisterForm form)
        {
            if(form.Password.Equals(form.PasswordCheck) == false)
            {
                return BadRequest("Passwords don't match");
            }

            User user = _users.Where(u => u.Email.Equals(form.Email))
                            .FirstOrDefault();

            if (user != null)
            {
                return BadRequest("This mail is already taken.");
            }

            User newUser = new User
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                Password = form.Password,
            };
            await _users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            await _bus.Publish(new UserAddedEvent
            {
                UserId = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName
            });

            return Ok(GenerateLoginResponse(newUser));
        }

        
        [Authorize]
        [HttpPost("promote")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PromoteToAdmin([FromBody] Guid userId)
        {
            var user = await _users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            user.IsAdmin = true;
            _users.Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpPost("degrade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DegradeFromAdmin([FromBody] Guid userId)
        {
            var user = await _users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            user.IsAdmin = false;
            _users.Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // TODO
        //[Authorize(Policy = "IsAdmin")]
        [HttpGet("isadmin/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CheckIfAdmin(Guid userId)
        {
            var user = await _users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null || !user.IsAdmin)
            {
                return BadRequest();
            }
            return Ok();
        }

        // TODO
        //[Authorize]
        [HttpGet("renew")]
        [ProducesResponseType(typeof(SessionResponse), 200)]
        public async Task<SessionResponse> RenewSession()
        {
            User currentUser = await _users.Where(u => u.Id.ToString() == User.Identity.Name).FirstOrDefaultAsync();
            SessionResponse renewalResponse = GenerateLoginResponse(currentUser);
            return renewalResponse;
        }

        private SessionResponse GenerateLoginResponse(User user)
        {
            return new SessionResponse()
            {
                User = user,
                JwtToken = _authService.GenerateJWTToken(user),
                ExpiresIn = TimeSpan.FromDays(_authService.TokenValidityPeriod).TotalSeconds.ToString()
            };
        }
    }
}
