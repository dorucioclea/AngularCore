using AngularCore.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCore.Services
{
    public interface IImageService
    {
        Image SaveImage(string authorId, IFormFile file, string title);
    }
}
