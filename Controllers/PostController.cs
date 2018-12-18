using System.Collections.Generic;
using AngularCore.Data.Models;
using AngularCore.Data.ViewModels;
using AngularCore.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AngularCore.Controllers
{
    [Authorize]
    [Route("api/v1/posts")]
    public class PostController : Controller
    {

        private IPostRepository _postRepository;
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public PostController(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PostVM>), 200)]
        public IActionResult GetAllPosts()
        {
            List<PostVM> posts = _mapper.Map<List<PostVM>>(_postRepository.GetAll());
            return Ok(posts);
        }

        [HttpGet("{postId}")]
        [ProducesResponseType(typeof(PostVM), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetPost(string postId)
        {
            Post post = _postRepository.GetById(postId);
            if( post == null )
            {
                return NotFound();
            }
            PostVM postVM = _mapper.Map<PostVM>(post);
            return Ok(postVM);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PostVM), 201)]
        [ProducesResponseType(typeof(ErrorMessage), 404)]
        public IActionResult CreatePost([FromBody] PostForm form)
        {
            var user = _userRepository.GetById(form.OwnerId);
            if(user == null)
            {
                return NotFound(new ErrorMessage("User not found"));
            }

            var post = new Post {
                User = user,
                Content = form.Content
            };

            _postRepository.Add(post);
            return Created(post.Id, post);
        }

        [HttpPut("{postId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        public IActionResult UpdatePost(string postId, [FromBody] PostForm form)
        {
            var post = _postRepository.GetById(postId);
            if( post == null )
            {
                return BadRequest(new ErrorMessage("Post was not found"));
            }

            post.Content = form.Content;
            _postRepository.Update(post);

            return Ok();
        }

        [HttpDelete("{postId}")]
        [ProducesResponseType(204)]
        public IActionResult DeletePost(string postId)
        {
            var post = _postRepository.GetById(postId);
            if( post != null )
            {
                _postRepository.Delete(post);
            }
            return NoContent();
        }

    }
}