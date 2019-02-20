using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularCore.Data.ViewModels;
using AngularCore.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularCore.Controllers
{
    [Authorize]
    [Route("api/v1/search")]
    public class SearchController : Controller
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public SearchController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{phrase}")]
        [ProducesResponseType(typeof(List<UserVM>), 200)]
        public IActionResult SearchUsers(string phrase)
        {
            var users = _userRepository.GetWhere( u => $"{u.Name} {u.Surname}".ToUpper().Contains(phrase.ToUpper()));
            return Ok(_mapper.Map<List<UserVM>>(users));
        }
    }
}