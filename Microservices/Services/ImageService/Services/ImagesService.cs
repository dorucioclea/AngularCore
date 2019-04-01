using ImageService.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService.Services
{
    public class ImagesService
    {
        private IHostingEnvironment _hostingEnvironment;
        private ApplicationContext _context;
        private DbSet<Image> _images;

        public ImagesService(IHostingEnvironment hostingEnvironment, ApplicationContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _images = context.Set<Image>();
        }

        public Image SaveImage(Guid authorId, string imageBase64, string fileName, string title)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string mediaUrlDirectory = Path.Combine("upload", authorId.ToString());
            string uploadPath = Path.Combine(webRootPath, mediaUrlDirectory);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            Guid imageGuid = Guid.NewGuid();
            string fileExtension = fileName.Split(".").Last();
            string newFileName = imageGuid.ToString() + "." + fileExtension;
            string fullPath = Path.Combine(uploadPath, newFileName);

            var imageBytes = Convert.FromBase64String(imageBase64);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                using (var fileStream = new MemoryStream(imageBytes))
                {
                    fileStream.CopyTo(stream);
                }
            }

            var mediaUrl = Path.Combine(mediaUrlDirectory, fileName);
            Image newImage = new Image
            {
                Id = imageGuid,
                AuthorId = authorId,
                MediaUrl = mediaUrl,
                Title = title
            };
            _images.Add(newImage);
            _context.SaveChanges();
            return newImage;
        }
    }
}
