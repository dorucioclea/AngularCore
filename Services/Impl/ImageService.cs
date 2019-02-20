using AngularCore.Data.Models;
using AngularCore.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AngularCore.Services.Impl
{
    public class ImageService : IImageService
    {
        private IImageRepository _imageRepository;
        private IUserRepository _userRepository;
        private IHostingEnvironment _hostingEnvironment;

        public ImageService(IImageRepository imageRepository, IUserRepository userRepository, IHostingEnvironment hostingEnvironment)
        {
            _imageRepository = imageRepository;
            _userRepository = userRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public Image SaveImage(string authorId, IFormFile file, string title)
        {
            User author = _userRepository.GetById(authorId);

            string webRootPath = _hostingEnvironment.WebRootPath;
            string mediaUrlDirectory = Path.Combine("upload", authorId);
            string uploadPath = Path.Combine(webRootPath, mediaUrlDirectory);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string imageGuid = Guid.NewGuid().ToString();
            string fileExtension = file.FileName.Split(".").Last();
            string fileName = imageGuid + "." + fileExtension;
            string fullPath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var mediaUrl = Path.Combine(mediaUrlDirectory, fileName);
            Image newImage = new Image
            {
                Id = imageGuid,
                Author = author,
                MediaUrl = mediaUrl,
                Title = title
            };
            _imageRepository.Add(newImage);
            return newImage;

        }
    }
}
