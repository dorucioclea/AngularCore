using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageService.Data;
using ImageService.Services;
using ImageService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private DbSet<Image> _images;
        private ApplicationContext _context;
        private ImagesService _imagesService;

        public ImagesController(ApplicationContext context, ImagesService imagesService)
        {
            _images = context.Set<Image>();
            _context = context;
            _imagesService = imagesService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Image>> Get()
        {
            return await _images.ToListAsync();
        }

        [HttpGet("author/{id}")]
        public async Task<IEnumerable<Image>> GetForAuthor(Guid id)
        {
            return await _images.Where(i => i.AuthorId == id).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Image> Get(Guid id)
        {
            return await _images.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        [HttpGet("profile/{id}")]
        public async Task<Image> GetProfilePicture(Guid userId)
        {
            return await _images.Where(i => i.AuthorId == userId && i.IsProfilePicture == true).FirstOrDefaultAsync();
        }

        [HttpPost("profile")]
        public async void ChangeProfilePicture([FromBody] ProfilePictureUpdate profilePicture)
        {
            var newProfilePicture = await _images.Where(i => i.Id == profilePicture.ImageId).FirstOrDefaultAsync();
            if(newProfilePicture != null)
            {
                var currentProfilePicture = await _images.Where(i => i.AuthorId == newProfilePicture.AuthorId).FirstOrDefaultAsync();
                if(currentProfilePicture != null)
                {
                    currentProfilePicture.IsProfilePicture = false;
                    _images.Update(currentProfilePicture);
                }
                newProfilePicture.IsProfilePicture = true;
                _images.Update(newProfilePicture);
                await _context.SaveChangesAsync();
            }
        }
        
        [HttpPost]
        public void Post([FromBody] ImageCreate imageCreate)
        {
            var authorId = imageCreate.AuthorId;
            var imageBase64 = imageCreate.ImageBase64;
            var fileName = imageCreate.FileName;
            var title = imageCreate.Title;

            _imagesService.SaveImage(authorId, imageBase64, fileName, title);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(Guid id, [FromBody] ImageUpdate imageUpdate)
        {
            var image = await _images.Where(i => i.Id == id).FirstOrDefaultAsync();
            if(image != null)
            {
                image.Title = imageUpdate.Title;
                _images.Update(image);
                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(Guid id)
        {
            var image = await _images.Where(i => i.Id == id).FirstOrDefaultAsync();
            if(image != null)
            {
                _images.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
