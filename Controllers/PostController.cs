using System.Collections.Generic;
using AngularCore.Data.Models;
using AngularCore.Data.ViewModels;
using AngularCore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AngularCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PostController : Controller
    {

        private IPostRepository _postRepository;
        private IUserRepository _userRepository;

        public PostController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Post), 201)]
        public IActionResult CreatePost([FromBody] PostForm form)
        {
            User owner = _userRepository.GetUserById(form.OwnerId);
            Post post = _postRepository.AddPost(new Post(
                owner: owner,
                content: form.Content
            ));
            return Created("CreatePost", post);
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(Post), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetPost(string id)
        {
            Post post = _postRepository.GetPostById(id);
            if( post == null )
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<Post>), 200)]
        public IActionResult GetAllPosts()
        {
            return Ok(_postRepository.GetAllPosts());
        }

        [HttpPut("[action]/{id}")]
        [ProducesResponseType(typeof(Post), 200)]
        [ProducesResponseType(400)]
        public IActionResult UpdatePost(string id, [FromBody] PostForm form)
        {
            Post post = _postRepository.GetPostById(id);
            if( post == null )
            {
                return BadRequest();
            }

            post.Content = form.Content;
            if(!_postRepository.UpdatePost(id, post))
            {
                return BadRequest();
            }

            return Ok(post);
        }

        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePost(string id)
        {
            Post post = _postRepository.GetPostById(id);
            if( post == null )
            {
                return NotFound();
            }
            _postRepository.DeletePost(post);
            return NoContent();
        }

    }
}