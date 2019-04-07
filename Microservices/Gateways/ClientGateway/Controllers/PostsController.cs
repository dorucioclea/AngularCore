using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularCore.Microservices.Gateways.Api.Models;
using AngularCore.Microservices.Gateways.Api.Services;
using AngularCore.Microservices.Gateways.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientGateway.Controllers
{
    [Route("api/posts")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IClientPostsApiService _postsService;

        public PostsController(IClientPostsApiService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Post>), 200)]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postsService.GetPosts();
            return Ok(posts);
        }

        [HttpGet("wall/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<Post>), 200)]
        public async Task<IActionResult> GetUserPosts(Guid userId)
        {
            var posts = await _postsService.GetUserWall(userId);
            return Ok(posts);
        }

        [HttpGet("{postId}")]
        [ProducesResponseType(typeof(Post), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPost(Guid postId)
        {
            var post = await _postsService.GetPost(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreatePost([FromBody] PostCreate form)
        {
            try
            {
                _postsService.AddPost(form);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{postId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult UpdatePost(Guid postId, [FromBody] PostUpdate form)
        {
            try
            {
                _postsService.UpdatePost(postId, form);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{postId}")]
        [ProducesResponseType(204)]
        public IActionResult DeletePost(Guid postId)
        {
            _postsService.DeletePost(postId);
            return NoContent();
        }

    }
}