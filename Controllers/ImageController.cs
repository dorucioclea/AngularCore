using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AngularCore.Data.Models;
using AngularCore.Data.ViewModels;
using AngularCore.Repositories;
using AngularCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularCore.Controllers
{
    [Route("api/v1/images")]
    public class ImageController : Controller
    {
        private IImageService _imageService;
        private IImageRepository _imageRepository;
        private IMapper _mapper;

        public ImageController(IImageService imageService, IImageRepository imageRepository, IMapper mapper)
        {
            _imageService = imageService;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }


        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var images = _imageRepository.GetAll();
            var mapped = _mapper.Map<List<ImageVM>>(images);
            return Ok(mapped);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Image image = _imageRepository.GetById(id);
            if(image == null)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<ImageVM>(image));
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromForm(Name = "image")] IFormFile file, [FromForm(Name = "title")] string title)
        {
            try
            {
                _imageService.SaveImage(User.Identity.Name, file, title);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest( ex );
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] string title)
        {
            Image image = _imageRepository.GetById(id);
            if(image == null)
            {
                return BadRequest();
            }

            image.Title = title;
            _imageRepository.Update(image);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Image image = _imageRepository.GetById(id);
            if(image == null)
            {
                return BadRequest();
            }

            _imageRepository.Delete(image);
            return NoContent();
        }
    }
}
