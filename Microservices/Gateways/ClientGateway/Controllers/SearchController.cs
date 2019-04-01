using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientGateway.Models;
using ClientGateway.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchApiService _searchService;

        public SearchController(ISearchApiService searchService)
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