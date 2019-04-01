using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClientGateway.Models;
using ClientGateway.Services;
using ClientGateway.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientGateway.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesApiService _imagesService;

        public ImagesController(IImagesApiService imagesService)
        {
            _imagesService = imagesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Image>), 200)]
        public async Task<IActionResult> Get()
        {
            var images = await _imagesService.GetImages();
            return Ok(images);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<Image>), 200)]
        public async Task<IActionResult> GetUserImages(Guid userId)
        {
            var images = await _imagesService.GetUserImages(userId);
            return Ok(images);
        }

        [HttpGet("profile/{userId}")]
        [ProducesResponseType(typeof(Image), 200)]
        public async Task<IActionResult> GetProfilePicture(Guid userId)
        {
            var images = await _imagesService.GetProfilePicture(userId);
            return Ok(images);
        }

        [HttpPost("profile/{imageId}")]
        [ProducesResponseType(200)]
        public IActionResult SetProfilePicture(Guid imageId)
        {
            _imagesService.ChangeProfilePicture(new ProfilePictureUpdate { ImageId = imageId });
            return Ok();
        }

        [HttpGet("{imageId}")]
        [ProducesResponseType(typeof(Image), 200)]
        [ProducesResponseType(204)]
        public IActionResult Get(Guid imageId)
        {
            var image = _imagesService.GetImage(imageId);
            if(image == null)
            {
                return NoContent();
            }
            return Ok(image);
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromForm(Name = "image")] IFormFile file, [FromForm(Name = "title")] string title, [FromForm(Name = "authorId")] Guid authorId)
        {
            bool save = false;
            var imageCreate = new ImageCreate();
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string base64Image = Convert.ToBase64String(fileBytes);
                if (!String.IsNullOrEmpty(base64Image))
                {
                    imageCreate.AuthorId = authorId;
                    imageCreate.Title = title;
                    imageCreate.ImageBase64 = base64Image;
                    save = true;
                }
            }

            if (save)
            {
                try
                {
                    _imagesService.AddImage(imageCreate);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }
        
        [HttpPut("{imageId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(Guid imageId, [FromBody] ImageUpdate imageUpdate)
        {
            try
            {
                _imagesService.UpdateImage(imageId, imageUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        
        [HttpDelete("{imageId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Delete(Guid imageId)
        {
            try
            {
                _imagesService.DeleteImage(imageId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}