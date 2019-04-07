using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularCore.Microservices.Gateways.Api.Models;
using AngularCore.Microservices.Gateways.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly IClientSearchApiService _searchService;

        public SearchController(IClientSearchApiService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("{phrase}")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> SearchUsers(string phrase)
        {
            var users = await _searchService.SearchUsers(phrase);
            return Ok(users);
        }
    }
}