using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularCore.Microservices.Services.Events;
using ImageService.Data;
using ImageService.Services;
using ImageService.ViewModels;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly DbSet<Image> _images;
        private readonly ApplicationContext _context;
        private readonly ImagesService _imagesService;
        private readonly IBus _bus;

        public ImagesController(ApplicationContext context, ImagesService imagesService, IBus bus)
        {
            _images = context.Set<Image>();
            _context = context;
            _imagesService = imagesService;
            _bus = bus;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Image>> Get()
        {
            return await _images.Include("Author").ToListAsync();
        }

        [HttpGet("author/{id}")]
        public async Task<IEnumerable<Image>> GetForAuthor(Guid id)
        {
            return await _images.Include("Author").Where(i => i.Author.Id == id).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Image> Get(Guid id)
        {
            return await _images.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        [HttpGet("profile/{userId}")]
        public async Task<Image> GetProfilePicture(Guid userId)
        {
            var image = await _images.Include("Author")
                                     .Where(i => i.Author.Id == userId)
                                     .Where(i => i.IsProfilePicture == true)
                                     .FirstOrDefaultAsync();
            if (image == null) {
                image = new Image
                {
                    MediaUrl = "http://localhost:15003/upload/default-profile-pic.png"
                };
            }
            return image;
        }

        [HttpPost("profile")]
        public async Task<IActionResult> ChangeProfilePicture([FromBody] ProfilePictureUpdate profilePicture)
        {
            var newProfilePicture = await _images.Include("Author").Where(i => i.Id == profilePicture.ImageId).FirstOrDefaultAsync();
            var currentProfilePicture = await _images.Include("Author")
                                                     .Where(i => i.Author.Id == newProfilePicture.Author.Id)
                                                     .Where(i => i.IsProfilePicture == true)
                                                     .FirstOrDefaultAsync();
            if (newProfilePicture != null) 
            {
                newProfilePicture.IsProfilePicture = true;
                _images.Update(newProfilePicture);
            } else {
                return NotFound();
            }

            if (currentProfilePicture != null) 
            {
                currentProfilePicture.IsProfilePicture = false;
                _images.Update(currentProfilePicture);
            }

            await _context.SaveChangesAsync();
            await _bus.Publish(new ProfilePictureChangedEvent
            {
                UserId = newProfilePicture.Author.Id,
                ProfilePictureUrl = newProfilePicture.MediaUrl
            });

            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImageCreate imageCreate)
        {
            var authorId = imageCreate.AuthorId;
            var imageBase64 = imageCreate.ImageBase64;
            var fileName = imageCreate.FileName;
            var title = imageCreate.Title;

            await _imagesService.SaveImage(authorId, imageBase64, fileName, title);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ImageUpdate imageUpdate)
        {
            var image = await _images.Where(i => i.Id == id).FirstOrDefaultAsync();
            if(image != null)
            {
                image.Title = imageUpdate.Title;
                _images.Update(image);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var image = await _images.Where(i => i.Id == id).FirstOrDefaultAsync();
            if(image != null)
            {
                _images.Remove(image);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
