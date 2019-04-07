using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularCore.Microservices.Gateways.Api.Services;
using AngularCore.Microservices.Gateways.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminGateway.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAdminIdentityApiService _identityService;

        public AuthController(IAdminIdentityApiService identityService)
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

        [Authorize]
        [HttpGet("renew")]
        [ProducesResponseType(typeof(SessionResponse), 200)]
        public async Task<IActionResult> RenewSession()
        {
            var response = await _identityService.RenewSession();
            return Ok(response);
        }

        [Authorize]
        [HttpPost("promote")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PromoteToAdmin(ChangeUserPrivileges userChange)
        {
            await Task.Run(() => _identityService.PromoteToAdmin(userChange));
            return Ok();
        }

        [Authorize]
        [HttpPost("degrade")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DegradeFromAdmin(ChangeUserPrivileges userChange)
        {
            await Task.Run(() => _identityService.DegradeFromAdmin(userChange));
            return Ok();
        }
    }
}