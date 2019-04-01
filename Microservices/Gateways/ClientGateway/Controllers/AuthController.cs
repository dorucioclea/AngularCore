using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientGateway.Services;
using ClientGateway.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientGateway.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityApiService _identityService;

        public AuthController(IIdentityApiService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(SessionResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] LoginForm form)
        {
            var response = await _identityService.Login(form);
            return Ok(response);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(SessionResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegisterForm form)
        {
            var response = await _identityService.Register(form);
            return Ok(response);
        }

        // TODO
        //[Authorize]
        [HttpGet("renew")]
        [ProducesResponseType(typeof(SessionResponse), 200)]
        public async Task<IActionResult> RenewSession()
        {
            var response = await _identityService.RenewSession();
            return Ok(response);
        }
    }
}