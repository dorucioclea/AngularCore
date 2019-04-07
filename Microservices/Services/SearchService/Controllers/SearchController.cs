using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchService.Data;

namespace SearchService.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private DbSet<User> _users;

        public SearchController(ApplicationContext context)
        {
            _users = context.Users;
        }

        // GET api/values
        [HttpGet("{phrase}")]
        [ProducesResponseType(typeof(List<Guid>), 200)]
        public async Task<List<User>> SearchUsers(string phrase)
        {
            var users = await _users.Where(u => u.FullName.ToUpper().Contains(phrase.ToUpper())).ToListAsync();
            return users;
        }
    }
}
