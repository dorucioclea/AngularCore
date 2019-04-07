using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostService.Data;
using PostService.ViewModels;

namespace PostService.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private DbSet<Post> _posts;
        private ApplicationContext _context;

        public PostsController(ApplicationContext context)
        {
            _posts = context.Set<Post>();
            _context = context;
        }

        [HttpGet("author/{id}")]
        public async Task<IEnumerable<Post>> GetForAuthor(Guid id)
        {
            List<Post> posts =  await _posts.Include("Author").Include("WallOwner")
                                            .Where(p => p.Author.Id == id).ToListAsync();
            return posts;
        }

        [HttpGet("wall/{id}")]
        public async Task<IEnumerable<Post>> GetForWall(Guid id)
        {
            List<Post> posts =  await _posts.Include("Author").Include("WallOwner")
                                            .Where(p => p.WallOwner.Id == id).ToListAsync();
            return posts;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> Get()
        {
            List<Post> posts = await _posts.Include("Author").Include("WallOwner").ToListAsync();
            return posts;
        }

        [HttpGet("{id}")]
        public async Task<Post> Get(Guid id)
        {
            return  await _posts.Include("Author").Include("WallOwner")
                                .Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostCreate postCreate)
        {
            var user = _context.Users.Where(u => u.Id == postCreate.AuthorId).FirstOrDefault();
            var wallOwner = _context.Users.Where(u => u.Id == postCreate.WallOwnerId).FirstOrDefault();
            var newPost = new Post
            {
                Author = user,
                WallOwner = wallOwner,
                Content = postCreate.Content
            };
            await _posts.AddAsync(newPost);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PostUpdate postUpdate)
        {
            var post = await _posts.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(post != null)
            {
                post.Content = postUpdate.Content;
            }
            _posts.Update(post);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var post = await _posts.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(post != null)
            {
                _posts.Remove(post);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
