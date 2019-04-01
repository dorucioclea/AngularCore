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
    [Route("api/post")]
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
            return await _posts.Where(p => p.AuthorId == id).ToListAsync();
        }

        [HttpGet("wall/{id}")]
        public async Task<IEnumerable<Post>> GetForWall(Guid id)
        {
            return await _posts.Where(p => p.WallOwnerId == id).ToListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> Get()
        {
            return await _posts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Post> Get(Guid id)
        {
            return await _posts.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        [HttpPost]
        public void Post([FromBody] PostCreate postCreate)
        {
            var newPost = new Post
            {
                AuthorId = postCreate.AuthorId,
                WallOwnerId = postCreate.WallOwnerId,
                Content = postCreate.Content
            };
            _posts.AddAsync(newPost);
            _context.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async void Put(Guid id, [FromBody] PostUpdate postUpdate)
        {
            var post = await _posts.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(post != null)
            {
                post.Content = postUpdate.Content;
            }
            _posts.Update(post);
            await _context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async void Delete(Guid id)
        {
            var post = await _posts.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(post != null)
            {
                _posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
