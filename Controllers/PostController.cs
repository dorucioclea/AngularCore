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
    [Route("api/[controller]")]
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

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Post), 201)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        public IActionResult CreatePost([FromBody] PostForm form)
        {
            User owner = _userRepository.GetUserById(form.OwnerId);
            if( owner == null ) {
                return BadRequest(new ErrorMessage("User does not exist"));
            }
            Post post = _postRepository.AddPost(new Post(
                owner: owner,
                content: form.Content
            ));
            return Created("CreatePost", _mapper.Map<PostVM>(post));
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(PostVM), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetPost(string id)
        {
            Post post = _postRepository.GetPostById(id);
            if( post == null )
            {
                return NotFound();
            }
            PostVM postVM = _mapper.Map<PostVM>(post);
            return Ok(postVM);
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(List<PostVM>), 200)]
        [ProducesResponseType(204)]
        public IActionResult GetUserPosts(string id)
        {
            User user = _userRepository.GetUserById(id);
            List<Post> posts = _postRepository.GetPostsForUser(user);
            if( posts.Count == 0 )
            {
                return NoContent();
            }
            List<PostVM> postVMs = _mapper.Map<List<PostVM>>(posts);
            return Ok(postVMs);
        }


        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<PostVM>), 200)]
        public IActionResult GetAllPosts()
        {
            List<PostVM> posts = _mapper.Map<List<PostVM>>(_postRepository.GetAllPosts());
            return Ok(posts);
        }

        [HttpPut("[action]/{id}")]
        [ProducesResponseType(typeof(Post), 200)]
        [ProducesResponseType(400)]
        public IActionResult UpdatePost(string id, [FromBody] PostForm form)
        {
            Post post = _postRepository.GetPostById(id);
            User owner = _userRepository.GetUserById(form.OwnerId);
            if( post == null || owner == null )
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